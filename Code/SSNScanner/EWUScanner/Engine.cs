using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EWUScanner
{

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    struct SSNPatternCount
    {
        public Int32 d9, d324;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    struct CCNPatternCount
    {
        public Int32 Visa,
            MC,
            Amex,
            Discover,
            Diners,
            JCB;
    };

    public static class Engine
    {

        private static SSNPatternCount ssnPattern;
        private static CCNPatternCount ccnPattern;

        public const String dllName = "Scanner.dll";

        internal static class UnsafeNativeMethod
        {
            [DllImport(dllName, CharSet = CharSet.Ansi, 
            CallingConvention = CallingConvention.Cdecl)]
            public static extern int ScanSSN(Char[] file_stream, 
                int length, ref int count, ref SSNPatternCount pattern);

            [DllImport(dllName, CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
            public static extern int ScanCCN(Char[] file_stream,
                int length, ref int count, ref CCNPatternCount pattern);
        }

        public static ScanData ScanForSocialSecurity(String text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = System.Environment.NewLine + text + System.Environment.NewLine;
                int count = 0;
                int retCode = UnsafeNativeMethod.ScanSSN(text.ToCharArray(), text.Length, ref count, ref ssnPattern);
                return new ScanData(count, retCode, ssnPattern.d9, ssnPattern.d324);
            }
            return new ScanData(0, 0, 0, 0);
        }

        public static CreditData ScanForCreditCard(String text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = System.Environment.NewLine + text + System.Environment.NewLine;
                int count = 0;
                int retCode = UnsafeNativeMethod.ScanCCN(text.ToCharArray(), text.Length, ref count, ref ccnPattern);
                return new CreditData(count, retCode, ccnPattern.Visa, ccnPattern.MC, ccnPattern.Amex, ccnPattern.Discover, ccnPattern.Diners, ccnPattern.JCB);
       
            } 
            return new CreditData(0,0,0,0, 0, 0, 0, 0);
        }
    }
}
