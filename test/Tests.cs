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

            public void Update([MyValueConverter] IDictionary<string, object> attr)
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
        public void Test_PR_221()
        {
            // https://github.com/nilproject/NiL.JS/pull/221
            var se = new NilJsProcessEngine();

            var calls = 0;
            se.Context.DefineVariable("loaded").Assign(new Action(() => calls++));

            se.Eval("main.js");

            Assert.AreEqual(1, calls);
        }

        [Test]
        public void Test_Issue_220()
        {
            // https://github.com/nilproject/NiL.JS/issues/220
            var se = new NilJsProcessEngine();
            se.Eval("main2.js");
        }
    }
}
