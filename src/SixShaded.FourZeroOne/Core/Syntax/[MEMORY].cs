﻿namespace SixShaded.FourZeroOne.Core.Syntax;

using Roggis;
using Korssas.Memory;
using SixShaded.FourZeroOne.Core.Korssas.Memory.Object;
using Korvessa.Defined;
using Roveggi;

public static partial class KorssaSyntax
{
    public static DynamicAssign<R> kAsVariable<R>(this IKorssa<R> korssa, out DynamicAddress<R> ident)
        where R : class, Rog
    {
        ident = new();
        return new(ident, korssa);
    }

    public static DynamicReference<R> kRef<R>(this IMemoryAddress<R> ident)
        where R : class, Rog =>
        new(ident);

    public static Insert<R> kWrite<R>(this IKorssa<IRoveggi<IMemoryRovetu<R>>> address, IKorssa<R> data)
        where R : class, Rog =>
        new(address, data);

    public static Get<R> kGet<R>(this IKorssa<IRoveggi<IMemoryRovetu<R>>> address)
        where R : class, Rog =>
        new(address);

    public static Remove kRedact(this IKorssa<IRoveggi<IMemoryRovetu<Rog>>> address) => new(address);

    public static Korvessa<IRoveggi<IMemoryRovetu<R>>, MetaFunction<R, R>, Roggis.Instructions.Assign<R>> kUpdate<R>(this IKorssa<IRoveggi<IMemoryRovetu<R>>> address, IKorssa<MetaFunction<R, R>> updateFunction)
        where R : class, Rog =>
        Korvessas.UpdateMemoryObject<R>.Construct(address, updateFunction);

    public static Korvessa<IRoveggi<IMemoryRovetu<R>>, MetaFunction<R, R>, Roggis.Instructions.Assign<R>> kUpdate<R>(this IKorssa<IRoveggi<IMemoryRovetu<R>>> address, IEnumerable<Addr> captures, Func<DynamicAddress<R>, IKorssa<R>> updateFunction)
        where R : class, Rog =>
        Korvessas.UpdateMemoryObject<R>.Construct(address, Core.kMetaFunction(captures, updateFunction));
}