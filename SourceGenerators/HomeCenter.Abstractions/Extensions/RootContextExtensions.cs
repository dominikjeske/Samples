using Proto;

namespace HomeCenter.Model.Extensions
{
    public static class RootContextExtensions
    {
        public static void Send(this RootContext context, string address, string actorId, object command)
        {
            var pid = new PID(address, actorId);
            context.Send(pid, command);
        }
    }
}