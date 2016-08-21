using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JitInterface
{
    public static unsafe partial class ClrJit
    {
        [Flags]
        public enum CorJitFlag : uint
        {
            SPEED_OPT = 0x00000001,
            SIZE_OPT = 0x00000002,
            DEBUG_CODE = 0x00000004, 
            DEBUG_EnC = 0x00000008, 
            DEBUG_INFO = 0x00000010, 
            MIN_OPT = 0x00000020, 
            GCPOLL_CALLS = 0x00000040, 
            MCJIT_BACKGROUND = 0x00000080, 

            UNUSED1 = 0x00000100,

            UNUSED2 = 0x00000200,
            UNUSED3 = 0x00000400,
            UNUSED4 = 0x00000800,
            UNUSED5 = 0x00001000,
            UNUSED6 = 0x00002000,

            MAKEFINALCODE = 0x00008000, 
            READYTORUN = 0x00010000, 

            PROF_ENTERLEAVE = 0x00020000, 
            PROF_REJIT_NOPS = 0x00040000, 
            PROF_NO_PINVOKE_INLINE
                                = 0x00080000, 
            SKIP_VERIFICATION = 0x00100000, 
            PREJIT = 0x00200000, 
            RELOC = 0x00400000, 
            IMPORT_ONLY = 0x00800000, 
            IL_STUB = 0x01000000, 
            PROCSPLIT = 0x02000000, 
            BBINSTR = 0x04000000, 
            BBOPT = 0x08000000, 
            FRAMED = 0x10000000, 
            ALIGN_LOOPS = 0x20000000, 
            PUBLISH_SECRET_PARAM = 0x40000000, 
            GCPOLL_INLINE = 0x80000000, 

        };

        public enum CorJitResult : uint
        {
            OK = 0,
            BADCODE = 1,
            OUTOFMEM = 2,
            INTERNALERROR = 3,
            SKIPPED = 4,
            RECOVERABLEERROR = 5,
        };

        [Flags]
        public enum CorInfoOptions
        {
            OPT_INIT_LOCALS = 0x00000010,
            GENERICS_CTXT_FROM_THIS = 0x00000020,
            GENERICS_CTXT_FROM_METHODDESC = 0x00000040,
            GENERICS_CTXT_FROM_METHODTABLE = 0x00000080,
            GENERICS_CTXT_MASK = (GENERICS_CTXT_FROM_THIS |
                                  GENERICS_CTXT_FROM_METHODDESC |
                                  GENERICS_CTXT_FROM_METHODTABLE),
            GENERICS_CTXT_KEEP_ALIVE = 0x00000100,
        }

        [Flags]
        public enum CorInfoRegionKind
        {
            NONE,
            HOT,
            COLD,
            JIT,
        }

        [Flags]
        public enum CorInfoCallConv
        {
            DEFAULT = 0x0,
            C = 0x1,
            STDCALL = 0x2,
            THISCALL = 0x3,
            FASTCALL = 0x4,
            VARARG = 0x5,
            FIELD = 0x6,
            LOCAL_SIG = 0x7,
            PROPERTY = 0x8,
            NATIVEVARARG = 0xb,

            MASK = 0x0f,
            GENERIC = 0x10,
            HASTHIS = 0x20,
            EXPLICITTHIS = 0x40,
            PARAMTYPE = 0x80,
        };

        public enum CorInfoType
        {
            UNDEF = 0x0,
            VOID = 0x1,
            BOOL = 0x2,
            CHAR = 0x3,
            BYTE = 0x4,
            UBYTE = 0x5,
            SHORT = 0x6,
            USHORT = 0x7,
            INT = 0x8,
            UINT = 0x9,
            LONG = 0xa,
            ULONG = 0xb,
            NATIVEINT = 0xc,
            NATIVEUINT = 0xd,
            FLOAT = 0xe,
            DOUBLE = 0xf,
            STRING = 0x10,
            PTR = 0x11,
            BYREF = 0x12,
            VALUECLASS = 0x13,
            CLASS = 0x14,
            REFANY = 0x15,
            VAR = 0x16,
            COUNT,
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CORINFO_METHOD_INFO
        {
            public IntPtr methodHandle; 
            public IntPtr moduleHandle; 
            public byte* ILCode;
            public uint ILCodeSize;
            public ushort maxStack;
            public ushort EHcount;
            public CorInfoOptions options;
            public CorInfoRegionKind regionKind;

            // TODO: add support for 32-bit and 64-bit apps.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CORINFO_SIG_INFO
        {
            public CorInfoCallConv callConv;
            public IntPtr retTypeClass;
            public IntPtr retTypeSigClass;
            public CorInfoType retType;
            public uint flags;
            public uint numArgs;
            public CORINFO_SIG_INST sigInst;
            public IntPtr args;
            public IntPtr pSig;
            public uint cbSig;
            public IntPtr scope;
            public uint token;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CORINFO_SIG_INST
        {
            uint classInstCount;
            IntPtr* classInst;
            uint methInstCount;
            IntPtr* methInst;
        };
        
    }
}
