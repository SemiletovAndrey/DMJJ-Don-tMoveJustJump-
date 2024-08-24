public interface IPayloadState<TPayload> : IExitableState
{
    public void Enter(TPayload payload);
}
