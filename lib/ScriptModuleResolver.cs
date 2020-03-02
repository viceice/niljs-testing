using NiL.JS;

namespace lib
{
    public class ScriptModuleResolver : CachedModuleResolverBase
    {
        public override bool TryGetModule(ModuleRequest moduleRequest, out Module result)
        {
            result = null;
            var path = moduleRequest.AbsolutePath;
            var file = $"scripts/{path}";
            if (NilJsUtil.Exists(file))
            {
                result = NilJsUtil.Create(path, file, NilJsUtil.ReadAll(file));
                return true;
            }

            if (!moduleRequest.CmdArgument.EndsWith(".js"))
            {
                var path2 = $"{path.Substring(0, path.Length - 3)}/index.js";
                file = ($"scripts/{path2}");
                if (NilJsUtil.Exists(file))
                {
                    result = NilJsUtil.Create(path, file, $"export * from '{path2}'");
                    return true;
                }
            }

            return false;
        }
    }
}
