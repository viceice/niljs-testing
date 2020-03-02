using System.IO;
using NiL.JS;

namespace lib
{
    public class NotFoundModuleResolver : IModuleResolver
    {
        public bool TryGetModule(ModuleRequest moduleRequest, out Module result) => throw new FileNotFoundException($"Module '{moduleRequest.CmdArgument}' not found", moduleRequest.AbsolutePath);
    }
}
