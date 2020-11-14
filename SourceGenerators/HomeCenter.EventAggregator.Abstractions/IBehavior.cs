namespace HomeCenter.EventAggregator
{
    public interface IBehavior : IAsyncCommandHandler
    {
        int Priority { get; }
        void SetNextNode(IAsyncCommandHandler asyncCommandHandler);
    }
}