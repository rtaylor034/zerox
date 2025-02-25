﻿namespace SixShaded.FourZeroOne.Core.Korssas.Component;

public sealed record Without<C> : Korssa.Defined.RegularKorssa<IRoveggi<C>>, IHasAttachedComponentIdentifier<C, IRoveggi<C>> where C : IRoveggitu
{
    public Without(IKorssa<IRoveggi<C>> holder) : base(holder) { }
    public required Roggi.Unsafe.IRovu<C> Rovu { get; init; }
    Roggi.Unsafe.IRovu<C> IHasAttachedComponentIdentifier<C, IRoveggi<C>>._attachedRovu => Rovu;
    protected override ITask<IOption<IRoveggi<C>>> StandardResolve(IKorssaContext _, RogOpt[] args) => args[0].RemapAs(x => ((IRoveggi<C>)x).WithoutComponents([Rovu])).ToCompletedITask();
    protected override IOption<string> CustomToString() => $"{ArgKorssas[0]}:{{{Rovu} X}}".AsSome();
}