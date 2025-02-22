﻿namespace SixShaded.FourZeroOne.Core.Tokens.IO.Select;

public sealed record One<R> : Token.Defined.Function<IMulti<R>, R> where R : class, Res
{
    public One(IToken<IMulti<R>> from) : base(from) { }

    protected override async ITask<IOption<R>> Evaluate(ITokenContext runtime, IOption<IMulti<R>> fromOpt) =>
        fromOpt.Check(out var from)
            ? (await runtime.Input.ReadSelection(from, 1))[0]
            .ExprAs(i => from.At(i).Expect($"Got invalid index '{i}', expected 0..{from.Count - 1}"))
            .AsSome()
            : new None<R>();

    protected override IOption<string> CustomToString() => $"Select({Arg1})".AsSome();
}