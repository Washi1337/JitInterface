using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JitInterface
{
    public static class Kernel32
    {
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "VirtualProtect")]
        private static extern bool __VirtualProtect(
            IntPtr lpAddress,
            uint dwSize,
            Protection flNewProtect,
            out Protection lpflOldProtect);

        public static void VirtualProtect(
            IntPtr lpAddress,
            uint dwSize,
            Protection flNewProtect,
            out Protection lpflOldProtect)
        {
            if (!__VirtualProtect(lpAddress, dwSize, flNewProtect, out lpflOldProtect))
                throw new Win32Exception();
        }

        public enum Protection : uint
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }
    }
}
