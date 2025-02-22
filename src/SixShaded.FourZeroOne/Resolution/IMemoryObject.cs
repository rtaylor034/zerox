#nullable enable
namespace SixShaded.FourZeroOne.Resolution
{
    public interface IMemoryObject<out R> : IMemoryAddress<R>, IResolution where R : class, IResolution { }
}