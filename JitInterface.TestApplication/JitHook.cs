using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JitInterface;

namespace JitInterface.TestApplication
{
    public static unsafe class JitHook
    {
        private static ClrJit.ICorJitCompiler.CompileMethodDelegate OriginalCompileMethod;
        private static ClrJit.ICorJitCompiler.CompileMethodDelegate CustomCompileMethod;
        
        static JitHook()
        {
            foreach (var method in typeof(JitHook).GetMethods(
                BindingFlags.Public 
                | BindingFlags.Static 
                | BindingFlags.NonPublic))
            {
                RuntimeHelpers.PrepareMethod(method.MethodHandle);
            }

            CustomCompileMethod = new ClrJit.ICorJitCompiler.CompileMethodDelegate(CompileMethod);
            RuntimeHelpers.PrepareDelegate(CustomCompileMethod);
        }
        
        public static void Hook()
        {
            var jitCompiler = *ClrJit.GetJit();
            OriginalCompileMethod = jitCompiler.CompileMethod;
            jitCompiler.CompileMethod = CustomCompileMethod;
        }

        public static void Unhook()
        {
            var jitCompiler = *ClrJit.GetJit();
            jitCompiler.CompileMethod = OriginalCompileMethod;
        }

        private static RuntimeMethodHandle IntPtrToMethodHandle(IntPtr intPtr)
        {
            var handle = new RuntimeMethodHandle();
            handle.GetType().GetFields(BindingFlags.NonPublic).First().SetValue(handle, intPtr);
            return handle;
        }

        public static ClrJit.CorJitResult CompileMethod(
            ClrJit.ICorJitCompiler* thisPtr,
            void* jitInfo,
            ClrJit.CORINFO_METHOD_INFO* info,
            ClrJit.CorJitFlag flags,
            byte** nativeEntry,
            ulong* nativeSizeOfCode)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Compiling method {0} (il code size: {1}, flags: {2})", 
                info->methodHandle,
                info->ILCodeSize, 
                flags);

            var result = OriginalCompileMethod(thisPtr, jitInfo, info, flags, nativeEntry, nativeSizeOfCode);

            Console.WriteLine("compileMethod returned " + result);
            Console.WriteLine();
            Console.ForegroundColor = oldColor;

            return result;
        }
    }

    
}
