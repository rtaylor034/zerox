﻿namespace SixShaded.FourZeroOne.Token;

public interface IHasNoArgs<out RVal> : IToken<RVal>
    where RVal : class, Res
{ }