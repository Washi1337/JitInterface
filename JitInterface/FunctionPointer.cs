using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JitInterface
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct FunctionPointer
    {
        private IntPtr _ptr;

        public FunctionPointer(IntPtr ptr)
        {
            _ptr = ptr;
        }

        public FunctionPointer(Delegate @delegate)
        {
            _ptr = Marshal.GetFunctionPointerForDelegate(@delegate);
        }

        public TDelegate GetDelegate<TDelegate>()
        {
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(_ptr);
        }
    }
}
