using GeometryDashAPI.Memory;
using GeometryDashAPI.Realtime.Structures;
using System;
using System.Collections.Generic;

namespace GeometryDashAPI.Realtime
{
    public class PlayingColors
    {
        private static readonly int[] ColorArray = new int[] { 0x003222D0, 0x164, 0x124, 0xB0, 0x20 };
        private static readonly int MaxCountOffset = 0x4;
        private static readonly int DataOffset = 0x8;
        private static readonly int ColorIDOffset = 0xF8;
        private static readonly int ColorRedOffset = 0xF3;
        private static readonly int ColorGreenOffset = 0xF1;
        private static readonly int ColorBlueOffset = 0xF2;

        private GameProcess process;
        private IntPtr Array;
        private IntPtr Data;
        private Dictionary<int, IntPtr> ColorPointers;

        public int Count
        {
            get
            {
                return process.Read<int>(Array.ToInt64());
            }
        }

        public RealtimeColor this[int id]
        {
            get
            {
                IntPtr ptr = ColorPointers[id];
                RealtimeColor color = new RealtimeColor();
                if (Exists)
                {
                    color.ID = id;
                    color.Red = process.Read<byte>(IntPtr.Add(ptr, ColorRedOffset).ToInt64());
                    color.Green = process.Read<byte>(IntPtr.Add(ptr, ColorGreenOffset).ToInt64());
                    color.Blue = process.Read<byte>(IntPtr.Add(ptr, ColorBlueOffset).ToInt64());
                }
                return color;
            }
        }

        /// <summary>
        /// May be dangerous
        /// </summary>
        public bool Exists
        {
            get
            {
                int count = Count;
                if (process.Read<int>(IntPtr.Add(Array, MaxCountOffset).ToInt64()) >= count && count != 0)
                    return true;
                return false;
            }
        }

        public PlayingColors()
        {
            process = new GameProcess();
            bool success = process.Initialize(Access.PROCESS_VM_OPERATION | Access.PROCESS_VM_READ | Access.PROCESS_VM_WRITE);
            if (!success)
                throw new Exception("Game not found");
            ColorPointers = new Dictionary<int, IntPtr>();
            Update();
        }

        public void UpdateColorPointers()
        {
            ColorPointers.Clear();
            int count = Count;
            for (int i = 0; i < count * 4; i += 4)
            {
                IntPtr color = process.Read<IntPtr>((long)IntPtr.Add(Data, i));
                int id = process.Read<int>((long)IntPtr.Add(color, ColorIDOffset));
                ColorPointers.Add(id, color);
            }
        }

        public bool ContainsID(int id)
        {
            return ColorPointers.ContainsKey(id);
        }

        public void Update()
        {
            Array = process.Read<IntPtr>(process.GetModule("GeometryDash.exe"), ColorArray);
            Data = process.Read<IntPtr>(IntPtr.Add(Array, DataOffset).ToInt64());
            UpdateColorPointers();
        }
    }
}
