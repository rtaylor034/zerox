﻿#nullable enable
namespace SixShaded.FourZeroOne.Rule
{
    public interface IProxy<out R> : Res
            where R : Res
    {
        public IToken<R> Token { get; }
        public RuleID FromRule { get; }
        public bool ReallowsRule { get; }
    }
}
