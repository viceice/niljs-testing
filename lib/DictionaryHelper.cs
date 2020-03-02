using System;
using System.Collections.Generic;
using System.Linq;
using NiL.JS.Core;
using NiL.JS.Core.Interop;
using NiL.JS.Extensions;
using JSArray = NiL.JS.BaseLibrary.Array;

namespace lib
{
    public static class DictionaryHelper
    {
        internal static object FromInternal(JSValue source)
        {
            if (source?.Exists == false)
                return null;

            switch (source.ValueType)
            {
                case JSValueType.Object:
                    if (source.Is<JSArray>())
                        return source.Select(kp => FromInternal(kp.Value)).ToArray();
                    return From(source.As<JSObject>());
                case JSValueType.NotExists:
                case JSValueType.Symbol:
                case JSValueType.Undefined:
                case JSValueType.Function:
                    return null;
                default:
                    return source.Value;
            }
        }

        [DoNotDelete]
        [DoNotEnumerate]
        [NotConfigurable]
        [JavaScriptName("from")]
        public static object From(JSObject source)
        {
            var res = new Dictionary<string, object>();

            if (source == null)
                return res;

            foreach (var kvp in source)
            {
                switch (kvp.Value.ValueType)
                {
                    case JSValueType.NotExists:
                    case JSValueType.Symbol:
                    case JSValueType.Undefined:
                    case JSValueType.Function:
                        break;
                    default:
                        res[kvp.Key] = FromInternal(kvp.Value);
                        break;
                }
            }

            return res;
        }

        [DoNotDelete]
        [DoNotEnumerate]
        [NotConfigurable]
        [JavaScriptName("set")]
        public static JSValue Set(Arguments args)
        {
            if (args.Length != 3)
                throw new ArgumentException(nameof(args), " Wrong argument count.");

            var source = args[0];
            var key = args[1];

            if (source?.Exists != true || source.ValueType != JSValueType.Object)
                throw new ArgumentException(nameof(args), "Missing source argument");

            if (key?.Exists != true || key.ValueType != JSValueType.String)
                throw new ArgumentException(nameof(args), "Missing key argument");

            switch (source.Value)
            {
                case IDictionary<string, object> d:
                    d[key.As<string>()] = FromInternal(args[2]);
                    break;
            }

            return source;
        }
    }
}
