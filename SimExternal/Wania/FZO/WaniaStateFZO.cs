using MorseCode.ITask;
using Perfection;
using FourZeroOne.FZOSpec;
using FourZeroOne.Resolution;
using FourZeroOne.Resolution.Unsafe;
using FourZeroOne.Rule;
using FourZeroOne.Macro.Unsafe;
using ResOpt = Perfection.IOption<FourZeroOne.Resolution.IResolution>;
using FourZeroOne.Token.Unsafe;
#nullable enable
namespace Wania.FZO
{
    public record WaniaStateFZO : IStateFZO
    {
        private bool _isInitialized;
        private PStack<OperationNode> _opStack;
        private PStack<IMemoryFZO> _memStack;
        private PStack<ETokenPrep> _prepStack;

        public WaniaStateFZO()
        {
            _opStack = new();
            _memStack = new();
            _prepStack = new();
            _isInitialized = false;
        }
        IEnumerable<IStateFZO.IOperationNode> IStateFZO.OperationStack => _opStack.Elements;
        IEnumerable<ETokenPrep> IStateFZO.TokenPrepStack => _prepStack.Elements;
        IStateFZO IStateFZO.Initialize(FZOSource source)
        {
            return new WaniaStateFZO()
            {
                _isInitialized = true,
                _opStack = new PStack<OperationNode>().WithEntries(new OperationNode()
                {
                    Operation = source.Program,
                    MemoryStack = new PStack<IMemoryFZO>().WithEntries(source.InitialMemory),
                    MemCount = 1,
                    ResolvedArgs = new()
                })
            };
        }
        IStateFZO IStateFZO.WithStep(EDelta step)
        {
            return step switch
            {
                EDelta.TokenPrep v => this with
                {
                    _prepStack = _prepStack.WithEntries(v.Value)
                },
                EDelta.PushOperation v => this with
                {
                    _opStack = _opStack.WithEntries(new OperationNode()
                    {
                        Operation = v.OperationToken,
                        MemoryStack = _opStack.TopValue.Expect("No parent operation node?").MemoryStack,
                        MemCount = 1,
                        ResolvedArgs = new(),
                    })
                },
                EDelta.Resolve v => v.Resolution.CheckOk(out var resolution, out var stateImplemented)
                    ? this with
                    {
                        _opStack = _opStack.At(1).Expect("No parent operation node?")
                            .MapTopValue(
                                opNode => opNode with
                                {
                                    ResolvedArgs = opNode.ResolvedArgs.WithEntries(resolution),
                                    MemCount = opNode.MemCount.ExprAs(x => resolution.IsSome() ? x + 1 : x),
                                    MemoryStack =
                                        resolution.Check(out var r)
                                        ? opNode.MemoryStack.WithEntries(
                                            opNode.MemoryStack.TopValue.Expect("no initial memory value?")
                                                .ExprAs(lastMemory => r.Instructions.AccumulateInto(
                                                    lastMemory, (mem, instruction) => instruction.TransformMemoryUnsafe(mem))))
                                        : opNode.MemoryStack
                                })
                            .IsA<PStack<OperationNode>>()
                    }
                    : stateImplemented switch
                    {
                        EStateImplemented.MetaExecute metaExecute => this with
                        {
                            _opStack = _opStack.WithEntries(new OperationNode()
                            {
                                Operation = metaExecute.FunctionToken,
                                MemoryStack = _opStack.TopValue.Expect("No parent operation node?").MemoryStack,
                                MemCount = 1,
                                ResolvedArgs = new(),
                            })
                        },
                        _ => throw new NotSupportedException()
                    },
                _ => throw new NotSupportedException(),
            };
        }
        private record OperationNode : IStateFZO.IOperationNode
        {
            public required int MemCount { get; init; }
            public required IToken Operation { get; init; }
            public required PStack<ResOpt> ResolvedArgs { get; init; }
            public required PStack<IMemoryFZO> MemoryStack { get; init; }
            IEnumerable<ResOpt> IStateFZO.IOperationNode.ArgResolutionStack => ResolvedArgs.Elements;
            IEnumerable<IMemoryFZO> IStateFZO.IOperationNode.MemoryStack => MemoryStack.Elements.Take(MemCount);
        }
    }

}