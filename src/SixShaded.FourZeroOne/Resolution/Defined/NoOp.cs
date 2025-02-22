#nullable enable
using FourZeroOne;

namespace SixShaded.FourZeroOne.Resolution.Defined
{
    using FourZeroOne.FZOSpec;
    using Handles;
    using SixShaded.FourZeroOne;
    using SixShaded.FourZeroOne.Resolution;
    using SixShaded.NotRust;
    using SixShaded.SixLib.GFunc;

    public abstract record NoOp : Construct
    {
        public override IEnumerable<IInstruction> Instructions => [];
    }
}
