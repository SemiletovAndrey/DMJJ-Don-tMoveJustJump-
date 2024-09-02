public interface IStateFactory
{
    public T CreateTState<T>() where T : IExitableState;
}