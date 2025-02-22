﻿namespace SixShaded.FourZeroOne.Core.Tokens.Component;

public sealed record Without<C> : Token.Defined.StandardToken<ICompositionOf<C>>, IHasAttachedComponentIdentifier<C, ICompositionOf<C>> where C : ICompositionType
{
    public Without(IToken<ICompositionOf<C>> holder) : base(holder) { }
    public required Resolution.Unsafe.IComponentIdentifier<C> ComponentIdentifier { get; init; }
    Resolution.Unsafe.IComponentIdentifier<C> IHasAttachedComponentIdentifier<C, ICompositionOf<C>>.AttachedComponentIdentifier => ComponentIdentifier;
    protected override ITask<IOption<ICompositionOf<C>>> StandardResolve(ITokenContext _, IOption<Res>[] args) => args[0].RemapAs(x => ((ICompositionOf<C>)x).WithoutComponents([ComponentIdentifier])).ToCompletedITask();
    protected override IOption<string> CustomToString() => $"{ArgTokens[0]}:{{{ComponentIdentifier} X}}".AsSome();
}