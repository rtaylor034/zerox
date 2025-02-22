﻿namespace SixShaded.FourZeroOne.Token.Defined;

public abstract record StandardToken<R> : TokenBehavior<R> where R : class, Res
{
    public StandardToken(params Tok[] args) : base(args) { }
    public StandardToken(IEnumerable<Tok> args) : base(args) { }
    protected abstract ITask<IOption<R>> StandardResolve(ITokenContext runtime, IOption<Res>[] args);
    protected override IResult<ITask<IOption<R>>, FZOSpec.EStateImplemented> Resolve(ITokenContext runtime, IOption<Res>[] args) => new Ok<ITask<IOption<R>>, FZOSpec.EStateImplemented>(StandardResolve(runtime, args));
}