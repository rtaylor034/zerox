﻿#nullable enable
namespace FourZeroOne.Core.Resolutions.Instructions
{
    public sealed record RuleAdd : Instruction
    {
        public required Rule.Unsafe.IRule<ResObj> Rule { get; init; }
        public override IMemory TransformMemory(IMemory state)
        {
            return state.WithRules([Rule]);
        }
        public override string ToString()
        {
            return $"<?>+{Rule}";
        }
    }
}