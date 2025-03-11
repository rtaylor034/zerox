﻿namespace SixShaded.FourZeroOne.Core.Korvessas;

using Roggis;
using Korvessa.Defined;
using Syntax;
using SixShaded.FourZeroOne.Roveggi;

public static class Compose<C>
    where C : IRovetu
{
    public static Korvessa<IRoveggi<C>> Construct() => new() { Du = Axoi.Korvedu("compose"), Definition = new Korssas.Fixed<IRoveggi<C>>(new Roveggi<C>()).kMetaBoxed([]) };
}