namespace SixShaded.FourZeroOne.FZOSpec;

public interface IProcessorFZO
{
    public Task<IResult<EProcessorStep, EProcessorHalt>> GetNextStep(IStateFZO state, IInputFZO input);

    public interface IKorssaContext
    {
        public IMemoryFZO CurrentMemory { get; }
        public IInputFZO Input { get; }
    }
}