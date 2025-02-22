﻿#nullable enable
namespace SixShaded.FourZeroOne.Core.Tokens.Component
{
    public sealed record Get<C, R> : Token.Defined.StandardToken<R>, IHasAttachedComponentIdentifier<C, R> where R : class, Res where C : ICompositionType
    {
        public required IComponentIdentifier<C, R> ComponentIdentifier { get; init; }
        Resolution.Unsafe.IComponentIdentifier<C> IHasAttachedComponentIdentifier<C, R>.AttachedComponentIdentifier => ComponentIdentifier;
        public Get(IToken<ICompositionOf<C>> holder) : base(holder) { }
        protected override ITask<IOption<R>> StandardResolve(ITokenContext _, IOption<Res>[] args)
        {
            return args[0].RemapAs(x => ((ICompositionOf<C>)x).GetComponent(ComponentIdentifier)).Press().ToCompletedITask();
        }
        protected override IOption<string> CustomToString() => $"{ArgTokens[0]}->{ComponentIdentifier}".AsSome();
    }
}