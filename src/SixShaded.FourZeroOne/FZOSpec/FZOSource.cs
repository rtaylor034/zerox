using MorseCode.ITask;
using SixShaded.FourZeroOne;
using SixShaded.NotRust;
using SixShaded.SixLib.GFunc;
#nullable enable
namespace SixShaded.FourZeroOne.FZOSpec
{
    using Token;
    using Res = Resolution.IResolution;
    using Rule = Rule.Unsafe.IRule<Resolution.IResolution>;
    using ResOpt = IOption<Resolution.IResolution>;
    using Token = IToken<Resolution.IResolution>;
    using Addr = Resolution.IMemoryAddress<Resolution.IResolution>;
    using Resolution;
    using Resolution.Unsafe;
    using Rule;
    using System.Diagnostics.CodeAnalysis;
    using SixShaded.NotRust;
    public record FZOSource
    {
        public required Token Program { get; init; }
        public required IMemoryFZO InitialMemory { get; init; }
    }
}