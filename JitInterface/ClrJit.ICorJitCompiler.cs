using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JitInterface
{
    public static unsafe partial class ClrJit
    {
        [DllImport("clrjit.dll", EntryPoint = "getJit", CallingConvention = CallingConvention.StdCall)]
        private static extern ICorJitCompiler* __getJit();

        public static ICorJitCompiler* GetJit()
        {
            ICorJitCompiler* jit = __getJit();
            if (jit == null)
                throw new Win32Exception();
            return jit;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ICorJitCompiler
        {
            private FunctionPointer* _vtable;

            [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
            public delegate CorJitResult CompileMethodDelegate(
                ICorJitCompiler* thisPtr,
                void* jitInfo,
                CORINFO_METHOD_INFO* info,
                CorJitFlag flags,
                byte** nativeEntry,
                ulong* nativeSizeOfCode);

            public CompileMethodDelegate CompileMethod
            {
                get
                {
                    return _vtable[0].GetDelegate<CompileMethodDelegate>();
                }
                set
                {
                    Kernel32.Protection oldProtection;
                    Kernel32.VirtualProtect(
                        (IntPtr) _vtable, 
                        (uint) sizeof(FunctionPointer),
                        Kernel32.Protection.PAGE_EXECUTE_READWRITE, 
                        out oldProtection);

                    _vtable[0] = new FunctionPointer(value);

                    Kernel32.VirtualProtect(
                        (IntPtr) _vtable, 
                        (uint) sizeof(FunctionPointer),
                        oldProtection, 
                        out oldProtection);
                }
            }
        }
        
    }
}
