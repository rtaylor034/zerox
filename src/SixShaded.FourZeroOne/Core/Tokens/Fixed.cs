﻿#nullable enable
using FourZeroOne;

namespace SixShaded.FourZeroOne.Core.Tokens
{
    public sealed record Fixed<R> : PureValue<R> where R : Res
    {
        public readonly R Resolution;
        public Fixed(R resolution)
        {
            Resolution = resolution;
        }
        protected override R EvaluatePure()
        {
            return Resolution;
        }
        protected override IOption<string> CustomToString() => $"|{Resolution}|".AsSome();
    }
}