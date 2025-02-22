﻿namespace SixShaded.FourZeroOne.Core.Resolutions;

using Resolution.Unsafe;
public sealed record MetaFunction<R> : Resolution.Defined.NoOp where R : class, Res
{
    public required DynamicAddress<MetaFunction<R>> SelfIdentifier { get; init; }
    public required IToken<R> Token { get; init; }
    public override string ToString() => $"{SelfIdentifier}(){{{Token}}}";

    public FZOSpec.EStateImplemented.MetaExecute GenerateMetaExecute() =>
        new()
        {
            Token = new Tokens.MetaExecuted<R>(Token),
            ObjectWrites = Iter.Over<(IMemoryAddress<Res>, IOption<Res>)>
                    ((SelfIdentifier, this.AsSome()))
                .Tipled(),
        };
}
public sealed record MetaFunction<RArg1, ROut> : Resolution.Defined.NoOp, IBoxedMetaFunction<ROut>
    where RArg1 : class, Res
    where ROut : class, Res
{
    public required DynamicAddress<MetaFunction<RArg1, ROut>> SelfIdentifier { get; init; }
    public required DynamicAddress<RArg1> IdentifierA { get; init; }
    public required IToken<ROut> Token { get; init; }
    IMemoryAddress<IBoxedMetaFunction<ROut>> IBoxedMetaFunction<ROut>.SelfIdentifier => SelfIdentifier;
    IEnumerable<IMemoryAddress<Res>> IBoxedMetaFunction<ROut>.ArgAddresses => [IdentifierA];
    public override string ToString() => $"{SelfIdentifier}({IdentifierA})::{{{Token}}}";

    public FZOSpec.EStateImplemented.MetaExecute GenerateMetaExecute(IOption<RArg1> arg1) =>
        new()
        {
            Token = new Tokens.MetaExecuted<ROut>(Token),
            ObjectWrites = Iter.Over<(IMemoryAddress<Res>, IOption<Res>)>
                    ((SelfIdentifier, this.AsSome()), (IdentifierA, arg1))
                .Tipled(),
        };
}
public sealed record MetaFunction<RArg1, RArg2, ROut> : Resolution.Defined.NoOp, IBoxedMetaFunction<ROut>
    where RArg1 : class, Res
    where RArg2 : class, Res
    where ROut : class, Res
{
    public required DynamicAddress<MetaFunction<RArg1, RArg2, ROut>> SelfIdentifier { get; init; }
    public required DynamicAddress<RArg1> IdentifierA { get; init; }
    public required DynamicAddress<RArg2> IdentifierB { get; init; }
    public required IToken<ROut> Token { get; init; }
    IMemoryAddress<IBoxedMetaFunction<ROut>> IBoxedMetaFunction<ROut>.SelfIdentifier => SelfIdentifier;
    IEnumerable<IMemoryAddress<Res>> IBoxedMetaFunction<ROut>.ArgAddresses => [IdentifierA, IdentifierB];

    public override string ToString() => $"{SelfIdentifier}({IdentifierA}, {IdentifierB})::{{{Token}}}";

    public FZOSpec.EStateImplemented.MetaExecute GenerateMetaExecute(IOption<RArg1> arg1, IOption<RArg2> arg2) =>
        new()
        {
            Token = new Tokens.MetaExecuted<ROut>(Token),
            ObjectWrites = Iter.Over<(IMemoryAddress<Res>, IOption<Res>)>
                    ((SelfIdentifier, this.AsSome()), (IdentifierA, arg1), (IdentifierB, arg2))
                .Tipled(),
        };
}
public sealed record MetaFunction<RArg1, RArg2, RArg3, ROut> : NoOp, IBoxedMetaFunction<ROut>
    where RArg1 : class, Res
    where RArg2 : class, Res
    where RArg3 : class, Res
    where ROut : class, Res
{
    public required DynamicAddress<MetaFunction<RArg1, RArg2, RArg3, ROut>> SelfIdentifier { get; init; }
    public required DynamicAddress<RArg1> IdentifierA { get; init; }
    public required DynamicAddress<RArg2> IdentifierB { get; init; }
    public required DynamicAddress<RArg3> IdentifierC { get; init; }
    public required IToken<ROut> Token { get; init; }

    IMemoryAddress<IBoxedMetaFunction<ROut>> IBoxedMetaFunction<ROut>.SelfIdentifier => SelfIdentifier;

    IEnumerable<IMemoryAddress<Res>> IBoxedMetaFunction<ROut>.ArgAddresses => [IdentifierA, IdentifierB, IdentifierC];

    public override string ToString() => $"{SelfIdentifier}({IdentifierA}, {IdentifierB}, {IdentifierC})::{{{Token}}}";

    public FZOSpec.EStateImplemented.MetaExecute GenerateMetaExecute(IOption<RArg1> arg1, IOption<RArg2> arg2, IOption<RArg3> arg3) =>
        new()
        {
            Token = new Tokens.MetaExecuted<ROut>(Token),
            ObjectWrites = Iter.Over<(IMemoryAddress<Res>, IOption<Res>)>
                    ((SelfIdentifier, this.AsSome()), (IdentifierA, arg1), (IdentifierB, arg2), (IdentifierC, arg3))
                .Tipled(),
        };
}
/// <summary>
/// <b>Strictly for internal workings (e.g. Rule definitions).</b><br></br> 
/// Not for normal use.
/// </summary>
public sealed record OverflowingMetaFunction<RArg1, RArg2, RArg3, RArg4, ROut> : NoOp, IBoxedMetaFunction<ROut>
    where RArg1 : class, Res
    where RArg2 : class, Res
    where RArg3 : class, Res
    where RArg4 : class, Res
    where ROut : class, Res
{
    public required DynamicAddress<OverflowingMetaFunction<RArg1, RArg2, RArg3, RArg4, ROut>> SelfIdentifier { get; init; }
    public required DynamicAddress<RArg1> IdentifierA { get; init; }
    public required DynamicAddress<RArg2> IdentifierB { get; init; }
    public required DynamicAddress<RArg3> IdentifierC { get; init; }
    public required DynamicAddress<RArg4> IdentifierD { get; init; }
    public required IToken<ROut> Token { get; init; }

    IMemoryAddress<IBoxedMetaFunction<ROut>> IBoxedMetaFunction<ROut>.SelfIdentifier => SelfIdentifier;

    IEnumerable<IMemoryAddress<Res>> IBoxedMetaFunction<ROut>.ArgAddresses => [IdentifierA, IdentifierB, IdentifierC, IdentifierD];

    public override string ToString() => $"{SelfIdentifier}({IdentifierA}, {IdentifierB}, {IdentifierC}, {IdentifierD})::{{{Token}}}";

    public FZOSpec.EStateImplemented.MetaExecute GenerateMetaExecute(IOption<RArg1> arg1, IOption<RArg2> arg2, IOption<RArg3> arg3, IOption<RArg4> arg4) =>
        new()
        {
            Token = new Tokens.MetaExecuted<ROut>(Token),
            ObjectWrites = Iter.Over<(IMemoryAddress<Res>, IOption<Res>)>
                    ((SelfIdentifier, this.AsSome()), (IdentifierA, arg1), (IdentifierB, arg2), (IdentifierC, arg3), (IdentifierD, arg4))
                .Tipled(),
        };
}