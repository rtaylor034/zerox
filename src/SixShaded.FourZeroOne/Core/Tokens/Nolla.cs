﻿#nullable enable
using FourZeroOne;

namespace SixShaded.FourZeroOne.Core.Tokens
{
    public sealed record Nolla<R> : Value<R> where R : class, ResObj
    {
        public Nolla() { }
        protected override ITask<IOption<R>> Evaluate(ITokenContext _) { return new None<R>().ToCompletedITask(); }
        protected override IOption<string> CustomToString() => "nolla".AsSome();
    }
}