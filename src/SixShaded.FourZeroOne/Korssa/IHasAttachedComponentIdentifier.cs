﻿namespace SixShaded.FourZeroOne.Korssa;

public interface IHasAttachedComponentIdentifier<in C, out R> : IKorssa<R>
    where C : IRoveggitu
    where R : class, Rog
{
    public Roggi.Unsafe.IRovu<C> _attachedRovu { get; }
}