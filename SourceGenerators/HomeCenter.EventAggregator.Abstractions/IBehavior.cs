namespace HomeCenter.EventAggregator
{
    public interface IBehavior : IAsyncCommandHandler
    {
        void SetNextNode(IAsyncCommandHandler asyncCommandHandler);

        int Priority { get; }
    }
}