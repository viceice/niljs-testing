using System;
using System.Collections.Concurrent;
using System.IO;
using NiL.JS;
using NiL.JS.Core;
using JsScript = NiL.JS.Script;

namespace lib
{
    internal static class NilJsUtil
    {
        private static readonly ConcurrentDictionary<string, Lazy<JsScript>> _scriptCache = new ConcurrentDictionary<string, Lazy<JsScript>>();


        private static JsScript CompileInternal(string code, string file)
        {
            try
            {
                return JsScript.Parse(code, (MessageLevel level, CodeCoordinates coords, string message) => CompilerLogger(level, coords, message, file));
            }
            catch (JSException ex)
            {
                Console.Error.WriteLine("FILE: {0}\n{1}", file, ex);
                throw;
            }
        }

        private static void CoreProvider_OnFlush(object sender, EventArgs e) => _scriptCache.Clear();

        internal static void CompilerLogger(MessageLevel level, CodeCoordinates coords, string message, string file)
        {
            switch (level)
            {
                case MessageLevel.Regular:
                    Console.WriteLine($"DEBUG: {message} at {file}{coords}");
                    break;
                case MessageLevel.Recomendation:
                    Console.WriteLine($"REC: {message} at {file}{coords}");
                    break;
                case MessageLevel.Warning:
                    Console.WriteLine($"WARN: {message} at {file}{coords}");
                    break;
                case MessageLevel.CriticalWarning:
                    Console.WriteLine($"CRIT: {message} at {file}{coords}");
                    break;
                case MessageLevel.Error:
                    Console.WriteLine($"ERROR: {message} at {file}{coords}");
                    break;
            }
        }

        public static JsScript Compile(string code, string file = null) => _scriptCache.GetOrAdd(code, new Lazy<JsScript>(() => CompileInternal(code, file))).Value;


        public static Module Create(string name, string path, string code, GlobalContext ctx = null)
        {
            var s = Compile(code, path);
            try
            {
                var m = new Module(name, s, ctx);

                return m;
            }
            catch (JSException ex)
            {
                Console.Error.WriteLine(ex);
                throw;
            }
        }

        public static string ReadAll(this string file) => File.ReadAllText(Resolve(file));

        public static bool Exists(this string file) => File.Exists(Resolve(file));

        private static string Resolve(string file) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
    }
}
