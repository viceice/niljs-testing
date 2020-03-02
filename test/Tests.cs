using NiL.JS.Core;
using NiL.JS.Core.Interop;
using NiL.JS.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace lib.test
{
    public class Tests
    {
        private class Test : Dictionary<string, object>
        {
            private sealed class MyValueConverter : ConvertValueAttribute
            {
                public override object From(object source) => null;

                public override object To(JSValue source) => DictionaryHelper.From(source.As<JSObject>());
            }

            public string Title { get; set; } = "Test Title";

            public void Create(IDictionary<string, object> attr)
            {
                Assert.IsNotNull(attr);
                Console.WriteLine($"Create:\n{Newtonsoft.Json.Linq.JToken.FromObject(attr)}");
            }

            public void Update([MyValueConverter]IDictionary<string, object> attr)
            {
                try
                {
                    Console.WriteLine($"Update:\n{Newtonsoft.Json.Linq.JToken.FromObject(attr)}");
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }

        [Test]
        public void Test1()
        {
            var se = new NilJsProcessEngine();

            var item = new Test { { "Test", 5 } };
            se.Context.DefineVariable("res").Assign("test");

            se.Eval("main.js");

            se.Context.DefineVariable("assert").Assign(new Action<bool, string>((v, s) => Assert.True(v, s)));
            se.Context.DefineVariable("res").Assign("test");
            se.Context.DefineVariable("item").Assign(JSValue.Marshal(item));

            var res = se.Run();

            Assert.NotNull(res);
        }
    }
}