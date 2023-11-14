using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace AutoBrowser
{
    public class ScriptCore
    {
        private string m_code;
        public string Source { get { return m_code; } }

        private System.Reflection.Assembly m_assembly;

        private bool m_isCompiled;
        public bool IsCompiled { get { return m_isCompiled; } }

        private string m_compile_msg;
        public string CompileMsg { get { return m_compile_msg; } }

        public ScriptCore(string code)
        {
            m_code = @"using System;
using "+ typeof(OpenCvSharp.Cv2).Namespace + @";
using AutoBrowser;

namespace IMKCode.ABS
{
    public class Coder
    {
" +
    string.Join("\r\n", GetUtilsInject())
+@"

        public static void RunCode()
        {
" +
code
+ @"
        }
    }
}";
        }

        System.Threading.Thread thRuning;

        public void Run()
        {
            if (thRuning == null)
            {
                thRuning = new System.Threading.Thread(new System.Threading.ThreadStart(run_loop_proc));
                thRuning.IsBackground = true;
                thRuning.Start();
            }

        }

        public void RunOnce()
        {
            if (m_assembly != null)
            {
                Type _t = m_assembly.GetType("IMKCode.ABS.Coder");
                if (_t != null)
                {
                    System.Reflection.MethodInfo _m = _t.GetMethod("RunCode");
                    if (_m != null)
                    {
                        _m.Invoke(null, null);
                    }
                }
            }
        }

        public void Abort()
        {
            if (thRuning != null)
            {
                try
                {
                    thRuning.Abort();
                }
                finally
                {
                    thRuning = null;
                }
            }
        }

        private void run_loop_proc()
        {
            if (m_assembly != null)
            {
                Type _t = m_assembly.GetType("IMKCode.ABS.Coder");
                if (_t != null)
                {
                    System.Reflection.MethodInfo _m = _t.GetMethod("RunCode");
                    if (_m != null)
                    {
                        while (thRuning != null)
                        {
                            _m.Invoke(null, null);
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public bool compiler()
        {
            CompilerResults cres = _compiler(m_code);
            if (cres != null)
            {
                if (cres.Errors.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (CompilerError ce in cres.Errors)
                    {
                        sb.AppendLine(ce.ToString());
                    }
                    m_compile_msg = "编译异常：\r\n" + sb.ToString();
                    m_assembly = null;
                    return false;
                }
                else
                {
                    m_compile_msg = "编译完成：\r\n"+ string.Join("\r\n", cres.Output);
                    m_assembly = cres.CompiledAssembly;
                    m_isCompiled = true;
                    return true;
                }
            }
            else
            {
                m_assembly = null;
                m_compile_msg = "IMK:当前环境不支持动态编译！";
                return false;
            }
        }

        private CompilerResults _compiler(params string[] source)
        {
            CodeDomProvider cdp = new CSharpCodeProvider();
            if (cdp != null)
            {
                CompilerParameters cpars = new CompilerParameters();
                cpars.GenerateExecutable = false;
                cpars.GenerateInMemory = true;
                cpars.TreatWarningsAsErrors = false;

                string[] _asses = this.GetAssemblies();
                foreach (string ReferencedAssembly in _asses)
                {
                    cpars.ReferencedAssemblies.Add(ReferencedAssembly);
                }

                CompilerResults cres = cdp.CompileAssemblyFromSource(cpars, source);
                return cres;
            }
            return null;
        }

        public decimal calcScriptTimes()
        {
            return calcScriptTimes(Source);
        }

        public static decimal calcScriptTimes(string source)
        {
            var mts = System.Text.RegularExpressions.Regex.Matches(source, @"sleep\s*\(\s*(?<sl>\d+)\s*\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            long sum = 0;
            foreach (System.Text.RegularExpressions.Match mt in mts)
            {
                if (mt.Success)
                {
                    sum += long.Parse(mt.Groups["sl"].Value);
                }
            }
            return sum / 1000M;
        }

        public virtual string[] GetUtilsInject()
        {
            List<string> mlist = new List<string>();
            List<string> plist = new List<string>();
            List<string> clist = new List<string>();
            var t = typeof(ScriptUtils);
            var ms = t.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (var m in ms)
            {
                var ps = m.GetParameters();
                plist.Clear();
                clist.Clear();
                for (int px = 0; px < ps.Length; px ++)
                {
                    plist.Add(ps[px].ParameterType.FullName + " par" + px);
                    clist.Add("par" + px);
                }

                mlist.Add(string.Format("\tpublic static {0} {1}({2}){{" +
                    "{3} {4}({5});" +
                    "}}"
                    , m.ReturnType.FullName == "System.Void" ? "void" : m.ReturnType.FullName
                    , m.Name
                    , string.Join(", ", plist)
                    , m.ReturnType.FullName == "System.Void" ? "" : "return"
                    , t.FullName + "." + m.Name
                    , string.Join(", ", clist)
                    ));
            }
            return mlist.ToArray();
        }

        public virtual string[] GetAssemblies()
        {
            List<KeyValuePair<string, string>> gac_temps = new List<KeyValuePair<string, string>>(AppDomain.CurrentDomain.GetAssemblies().Where(p => p.GlobalAssemblyCache).Select(p => new KeyValuePair<string, string>(p.GetName().Name, p.Location)));
            List<KeyValuePair<string, string>> dyn_temps = new List<KeyValuePair<string, string>>();
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies().Where(p => p.GetName().Name != "Anonymously Hosted DynamicMethods Assembly"))
            {
                if (!gac_temps.Exists(p => p.Key == item.GetName().Name))
                {
                    string location = string.Empty;
                    try
                    {
                        location = item.Location;
                    }
                    catch
                    {
                        continue;
                    }
                    dyn_temps.Add(new KeyValuePair<string, string>(item.GetName().Name, location));
                }
            }
            List<KeyValuePair<string, string>> assemblies = new List<KeyValuePair<string, string>>();
            assemblies.AddRange(gac_temps);
            assemblies.AddRange(dyn_temps);
            return assemblies.Select(p => p.Value).ToArray();
        }
    }

    /*
    static CompilerResults GetAssemblyFromSourceByCodeDom(params string[] source)
        {
            var provider = CodeDomProvider.CreateProvider(
                                                        "CSharp",
                                                        new Dictionary<string, string> { { "CompilerVersion", "v4.0" }});
            
            var compileResult = provider.CompileAssemblyFromSource(
                                                        new CompilerParameters()
                                                        {
                                                            IncludeDebugInformation = false,
                                                            TreatWarningsAsErrors = true,
                                                            WarningLevel = 4,
                                                            GenerateExecutable = false,
                                                            GenerateInMemory = true
                                                        },
                                                        source);


            return compileResult;
        }

    }*/
}
