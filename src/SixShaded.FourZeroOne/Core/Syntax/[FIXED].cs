﻿namespace SixShaded.FourZeroOne.Core.Syntax;

using Resolutions;

public static partial class TokenSyntax
{
    public static Tokens.Fixed<Bool> tFixed(this bool value) => new(value);

    public static Tokens.Fixed<Number> tFixed(this int value) => new(value);

    public static Tokens.Fixed<NumRange> tFixed(this Range value) => new(value);

    public static Tokens.Fixed<R> tFixed<R>(this R value) where R : class, Res => new(value);

    public static Tokens.Fixed<Multi<R>> tFixed<R>(this IEnumerable<R> values) where R : class, Res => new(new() { Values = values.ToPSequence() });
}