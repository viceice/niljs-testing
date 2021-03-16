using NiL.JS.Core;
using NiL.JS.Core.Interop;
using NiL.JS.Extensions;
using NUnit.Framework;
using System;
using System.Collections;
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


        class IndexerDummy
        {

            public object this[string key] { get => key; }
            public object this[Guid key] { get => key; }

            public string Title => "title";

            public bool Update() => true;
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
        public void Test_Issue_234()
        {
            // https://github.com/nilproject/NiL.JS/issues/220
            // https://github.com/nilproject/NiL.JS/issues/234
            var se = new NilJsProcessEngine();

            // Should not throw
            Assert.Throws<JSException>(() => se.Eval("main2.js"));
        }

        [Test]
        public void Test_Issue_224_console()
        {
            // https://github.com/nilproject/NiL.JS/issues/224
            var se = new Context();
            se.DefineVariable("it").Assign(JSValue.Marshal(new IndexerDummy()));
            se.Eval("console.log('start')");
            Assert.IsTrue(se.Eval("it.Update()").As<bool>());
            Assert.AreEqual("title", se.Eval("it.Title").ToString());
            //se.Eval("console.log('item:', it)")
            Assert.Throws<JSException>(() => se.Eval("console.log('item:', it)"));
            se.Eval("console.log('end')");
        }

        [Test]
        public void Test_Issue_224_json()
        {
            // https://github.com/nilproject/NiL.JS/issues/224
            var se = new Context();
            se.DefineVariable("it").Assign(JSValue.Marshal(new IndexerDummy()));
            se.Eval("console.log('start')");
            Assert.IsTrue(se.Eval("it.Update()").As<bool>());
            Assert.AreEqual("title", se.Eval("it.Title").ToString());
            //Assert.IsNotNull(se.Eval("JSON.stringify(it)").Value);
            Assert.Throws<JSException>(() => se.Eval("JSON.stringify(it)"));
            se.Eval("console.log('end')");
        }
    }
}
