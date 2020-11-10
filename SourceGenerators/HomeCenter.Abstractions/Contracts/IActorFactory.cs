using HomeCenter.EventAggregator;
using Proto;

namespace HomeCenter.Abstractions
{
    public interface IActorFactory
    {
        RootContext Context { get; }

        PID GetExistingActor(string id, string address = default, IContext parent = default);

        PID CreateActor<T>(string id = default, IContext parent = default) where T : class, IActor;

        PID CreateActor<C>(C actorConfig, IContext parent = null) where C : IBaseObject, IPropertySource;

        PID GetParentActor(PID actor);
    }
}