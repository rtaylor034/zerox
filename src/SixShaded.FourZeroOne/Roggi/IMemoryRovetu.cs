﻿namespace SixShaded.FourZeroOne.Roggi;

/// <summary>
/// Allows 'Get' 'Insert' and 'Redact' Korssas.
/// </summary>
public interface IMemoryRovetu<out R> : IRovetu
    where R : class, Rog
{ }
