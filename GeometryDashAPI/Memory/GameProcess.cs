using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GeometryDashAPI.Memory
{
    public class GameProcess
    {
        [DllImport("kernel32.dll")]
        protected static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        protected static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        protected static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

        public int BytesRead;
        public int BytesWrite;

        public Process Game;
        public IntPtr GameHandle;

        public bool Initialize(int access)
        {
            return this.Initialize(access, "GeometryDash");
        }

        public bool Initialize(int access, string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
                Game = Process.GetProcessesByName(processName)[0];
            else
                return false;
            GameHandle = OpenProcess(access, false, Game.Id);
            return true;
        }

        public IntPtr GetModuleAddress(string moduleFullName)
        {
            foreach (ProcessModule modules in Game.Modules)
            {
                if (moduleFullName == modules.ModuleName)
                    return modules.BaseAddress;
            }
            return (IntPtr)(-1);
        }

        public T Read<T>(int address) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize];
            ReadProcessMemory((int)GameHandle, address, buffer, buffer.Length, ref this.BytesRead);

            return this.BytesToStructure<T>(buffer);
        }

        //TODO: Not the best option
        public T Read<T>(ProcessModule module, int[] offsets) where T : struct
        {
            IntPtr[] pointers = new IntPtr[offsets.Length - 1];
            pointers[0] = this.Read<IntPtr>((int)IntPtr.Add(module.BaseAddress, offsets[0]));
            for (int i = 1; i < offsets.Length - 1; i++)
                pointers[i] = this.Read<IntPtr>((int)IntPtr.Add(pointers[i - 1], offsets[i]));

            return this.Read<T>((int)IntPtr.Add(pointers[offsets.Length - 2], offsets[offsets.Length - 1]));
        }

        public void Write<T>(int address, T value)
        {
            byte[] buffer = this.StructureToBytes(value);

            WriteProcessMemory((int)GameHandle, address, buffer, buffer.Length, out this.BytesWrite);
        }

        private T BytesToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private byte[] StructureToBytes(object value)
        {
            int size = Marshal.SizeOf(value);
            byte[] array = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }        
    }
}
