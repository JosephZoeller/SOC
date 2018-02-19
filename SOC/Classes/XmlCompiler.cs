﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes
{
    static class XmlCompiler
    {
        
        public static string FoxToolPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ToolAssets//FoxTool.exe");

        public static void CompileFile(string toolArg, string ToolPath)
        {
            Process compileProcess = new Process();
            compileProcess.StartInfo.FileName = ToolPath;
            compileProcess.StartInfo.Arguments = toolArg;
            compileProcess.StartInfo.UseShellExecute = false;
            compileProcess.StartInfo.CreateNoWindow = true;
            compileProcess.Start();
            compileProcess.WaitForExit();
        }
    }
}
