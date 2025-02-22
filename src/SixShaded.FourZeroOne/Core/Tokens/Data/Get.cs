﻿#nullable enable
using FourZeroOne;

namespace SixShaded.FourZeroOne.Core.Tokens.Data
{
    public sealed record Get<R> : Function<IMemoryObject<R>, R>
        where R : Res
    {
        public Get(IToken<IMemoryObject<R>> address) : base(address) { }
        protected override ITask<IOption<R>> Evaluate(ITokenContext runtime, IOption<IMemoryObject<R>> in1)
        {
            return in1.RemapAs(x => runtime.CurrentMemory.GetObject(x)).Press().ToCompletedITask();
        }
        protected override IOption<string> CustomToString() => $"*{Arg1}".AsSome();
    }
}