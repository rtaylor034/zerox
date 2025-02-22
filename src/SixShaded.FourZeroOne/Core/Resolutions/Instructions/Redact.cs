﻿#nullable enable
namespace FourZeroOne.Core.Resolutions.Instructions
{
    public sealed record Redact : Instruction
    {
        public required IMemoryAddress Address { get; init; }
        public override IMemory TransformMemory(IMemory context)
        {
            return context.WithClearedAddresses([Address]);
        }
    }
}