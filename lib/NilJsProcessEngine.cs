using System;
using NiL.JS;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;


namespace lib
{
    public sealed class NilJsProcessEngine
    {
        private readonly GlobalContext _ctx;
        private Module _module;

        public Context Context => _ctx;

        public NilJsProcessEngine()
        {
            _ctx = new GlobalContext
            {
                { "log", new Action<string>(s => Console.WriteLine($"LOG: {s}")) },
                { "assert", new Action<bool, string>((v, s) => { if (!v) throw new InvalidOperationException(s); }) },
            };

            _ctx.DefineConstructor(typeof(DictionaryHelper), "Dict");
            _ctx.DeleteVariable("Promise");
        }

        public void Eval(string file)
        {
            _module = NilJsUtil.Create("main.js", file, NilJsUtil.ReadAll($"process/{file}"), _ctx);
            _module.ModuleResolversChain.Add(new ScriptModuleResolver());
            _module.ModuleResolversChain.Add(new NotFoundModuleResolver());
            _module.Run();
        }

        public object Run()
        {
            var f = _module.Exports.Default.As<ICallable>();

            if (f == null)
            {
                Console.Error.WriteLine("Mising default export");
                throw new Exception("Missing process default export");
            }

            var res = f.Call(JSValue.Undefined, new Arguments());

            string val = JSON.stringify(res);
            Console.WriteLine("RES: {0}", res);
            return  val != null && val != "undefined" ? Newtonsoft.Json.Linq.JObject.Parse(val): null;
        }
    }
}
