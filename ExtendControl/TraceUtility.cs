using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace ExtendControl
{
    static public class TraceUtility
    {
        static public void Trace([CallerFilePath] string filePath = ""
            , [CallerLineNumber] int lineNum = 0
            , [CallerMemberName] string funcName = "")
        {
            Debug.WriteLine($"{Path.GetFileName(filePath)} L{lineNum} {funcName}()");
        }
    }
}
