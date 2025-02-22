﻿#nullable enable
namespace FourZeroOne.Core.Tokens
{
    public sealed record DynamicAssign<R> : StandardToken<r.Instructions.Assign<R>> where R : class, ResObj
    {
        public readonly IMemoryAddress<R> AssigningAddress;
        public DynamicAssign(IMemoryAddress<R> address, IToken<R> obj) : base(obj)
        {
            AssigningAddress = address;
        }
        protected override ITask<IOption<r.Instructions.Assign<R>>> StandardResolve(ITokenContext runtime, IOption<ResObj>[] args)
        {
            return args[0].RemapAs(x => new r.Instructions.Assign<R>() { Address = AssigningAddress, Subject = (R)x }).ToCompletedITask();
        }
        protected override IOption<string> CustomToString() => $"{AssigningAddress}<- {ArgTokens[0]}".AsSome();
    }
}