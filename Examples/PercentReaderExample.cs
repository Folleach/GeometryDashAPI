using GeometryDashAPI.Memory;
using System;
using System.Diagnostics;
using System.Threading;

namespace Examples
{
    class PercentReader
    {
        public delegate void PercentDelegate(int percent);
        public event PercentDelegate PercentChanged;

        Thread Checker;

        GameProcess Game = new GameProcess();
        ProcessModule Module;

        public int RefreshDelay { get; set; }

        public PercentReader(int delay, bool backgroud)
        {
            RefreshDelay = delay;
            Game.Initialize(Access.PROCESS_VM_READ);
            Module = Game.GetModule("GeometryDash.exe");
            Checker = new Thread(Check) { IsBackground = backgroud };
        }

        public void Start() => Checker.Start();

        void Check()
        {
            int oldPercent = 0;
            while (true)
            {
                IntPtr ptr = IntPtr.Add(Game.Read<IntPtr>(Module, new int[] { 0x003222D0, 0x164, 0x124, 0xB4, 0x3C0 }), 0x12C);
                string readed = Game.ReadString((int)ptr, 4);
                if (!readed.Contains("%"))
                {
                    oldPercent = -1;
                    Thread.Sleep(150);
                    continue;
                }
                int curPercent = int.Parse(readed.Split('%')[0]);
                if (curPercent != oldPercent)
                {
                    PercentChanged?.Invoke(curPercent);
                    oldPercent = curPercent;
                }
                Thread.Sleep(RefreshDelay);
            }
        }
    }

    class PercentReaderExample
    {
        public static void Invoke()
        {
            PercentReader reader = new PercentReader(35, false);
            reader.PercentChanged += Reader_PercentChanged;
            reader.Start();
        }

        private static void Reader_PercentChanged(int percent)
        {
            Console.WriteLine($"Percent changed: {percent}");
        }
    }
}
