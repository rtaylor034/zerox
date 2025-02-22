#nullable enable
namespace FourZeroOne.Resolution
{
    using FourZeroOne.FZOSpec;
    using Handles;
    using SixShaded.FourZeroOne;
    using SixShaded.NotRust;
    using SixShaded.SixLib.GFunc;

    // the 'new()' constraint is mega stupid.
    // this is mega stupid.
    public record CompositionOf<C> : NoOp, ICompositionOf<C> where C : ICompositionType, new()
    {
        private PMap<Unsafe.IComponentIdentifier<C>, IResolution> _componentMap { get; init; }
        public IEnumerable<ITiple<Unsafe.IComponentIdentifier, IResolution>> ComponentsUnsafe => _componentMap.Elements;
        public CompositionOf()
        {
            _componentMap = new();
        }
        // UNBELIEVABLY stupid
        public ICompositionOf<C> WithComponent<R>(IComponentIdentifier<C, R> identifier, R data) where R : IResolution
        {
            return this with { _componentMap = _componentMap.WithEntries((identifier.IsA<Unsafe.IComponentIdentifier<C>>(), data.IsA<IResolution>()).Tiple()) };
        }
        public ICompositionOf<C> WithComponentsUnsafe(IEnumerable<ITiple<Unsafe.IComponentIdentifier<C>, IResolution>> components)
        {
            return this with { _componentMap = _componentMap.WithEntries(components) };
        }
        public ICompositionOf<C> WithoutComponents(IEnumerable<Unsafe.IComponentIdentifier<C>> addresses)
        {
            return this with { _componentMap = _componentMap.WithoutEntries(addresses) };
        }

        public IOption<R> GetComponent<R>(IComponentIdentifier<C, R> address) where R : IResolution
        {
            return _componentMap.At(address).RemapAs(x => (R)x);
        }
        public override string ToString()
        {
            return $"{typeof(C).Namespace!.Split(".")[^1]}.{typeof(C).Name}:{{{string.Join(" ", ComponentsUnsafe.OrderBy(x => x.A.ToString()).Map(x => $"{x.A}={x.B}"))}}}";
        }
        public virtual bool Equals(CompositionOf<C>? other)
        {
            return (other is not null) && ComponentsUnsafe.SequenceEqual(other.ComponentsUnsafe);
        }
        public override int GetHashCode()
        {
            return ComponentsUnsafe.GetHashCode();
        }
    }
}
