using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeCenter.Abstractions;
using HomeCenter.Abstractions.Defaults;
using HomeCenter.EventAggregator;
using HomeCenter.Messages.Events.Service;
using HomeCenter.Messages.Queries;
using Microsoft.Extensions.Logging;
using Proto;
using Proto.Mailbox;
using Behavior = Proto.Behavior;

namespace HomeCenter.Actors.Core
{
    public abstract class DeviceActor : BaseObject, IActor
    {
        [DI] protected IMessageBroker MessageBroker { get; set; }
        [DI] protected ILogger Logger { get; set; }
        public bool IsEnabled
        {
            get => this.AsBool(MessageProperties.IsEnabled, true);
            set => this.SetProperty(MessageProperties.IsEnabled, value);
        }

        protected IList<string> Tags
        {
            get => this.AsList(MessageProperties.Tags);
            set => this.SetPropertyList(MessageProperties.Tags, value.ToArray());
        }

        protected readonly Behavior Behavior = new Behavior();

        protected DisposeContainer _disposables = new DisposeContainer();
        protected PID Self { get; private set; }

        //Add resource to container that will be released when actor is about to be released
        protected void ProtectResource(IDisposable resource) => _disposables.Add(resource);

        protected CancellationToken Token => _disposables.Token;

        protected DeviceActor()
        {
            Behavior.Become(StandardMode);
        }

        public Task ReceiveAsync(IContext context)
        {
            try
            {
                //MessageBroker.SendAfterDelay

                return Behavior.ReceiveAsync(context);
            }
            catch (Exception rr)
            {
                throw;
            }
        }

        protected void Log(EventId eventId, string template = "", params object[] arguments)
        {
            if (string.IsNullOrEmpty(template))
            {
                template = eventId.Name;
            }

            template = "[{Uid}] " + template;
            var args = new List<object>(arguments);
            args.Insert(0, Uid);

            Logger.LogInformation(eventId, template, args.ToArray());
        }

        protected Task StandardMode(IContext context) => ReceiveAsyncInternal(context);

        protected void Become(Receive receive)
        {
            Log(ActorEventType.Behavior, "changed behavior to '{behavior}'", receive.Method.Name);

            Behavior.Become(receive);
        }

        protected virtual Task ReceiveAsyncInternal(IContext context) => Task.CompletedTask;

        protected virtual Task UnhandledMessage(object message)
        {
            if (message is ActorMessage actorMessage)
            {
                throw new ArgumentException($"Component [{Uid}] cannot process message because there is no registered handler for [{actorMessage.Type ?? actorMessage.GetType().Name}]");
            }
            else
            {
                throw new ArgumentException($"Component [{Uid}] cannot process message because type {message.GetType().Name} is not ActorMessage");
            }
        }

        protected object FormatMessage(object rawMessage)
        {
            if (rawMessage is ActorMessage message)
            {
                Log(ActorEventType.Messagee, "Incoming message '{message}'", message);
            }

            return rawMessage;
        }

        protected virtual async Task<bool> HandleSystemMessages(IContext context)
        {
            var msg = context.Message;

            if (msg is Started)
            {
                if (!IsEnabled)
                {
                    Log(ActorEventType.Messagee, "Device disabled and all messages will be ignored");
                    return true;
                }

                await OnStarted(context);
                return true;
            }
            else if (msg is Restarting)
            {
                await OnRestarting(context);
                return true;
            }
            else if (msg is Restart)
            {
                await OnRestart(context);
                return true;
            }
            else if (msg is Stop)
            {
                await OnStop(context);
                return true;
            }
            else if (msg is Stopped)
            {
                await OnStopped(context);
                return true;
            }
            else if (msg is Stopping)
            {
                await Stopping(context);
                return true;
            }
            else if (msg is SystemMessage)
            {
                await OtherSystemMessage(context);
                return true;
            }

            if (msg is SystemStartedEvent started)
            {
                await OnSystemStarted(started);
                return true;
            }

            if (msg is ActorContextQuery)
            {
                context.Respond(context);
                return true;
            }

            // If actor is disabled we are ignoring all non system messages
            if (!IsEnabled)
            {
                return true;
            }

            return false;
        }

        protected virtual Task OnStarted(IContext context)
        {
            Log(ActorEventType.ActorStart, "Started with id '{deviceId}'", context.Self.Id);

            Self = context.Self;

            Subscribe<SystemStartedEvent>();

            return Task.CompletedTask;
        }

        protected virtual Task OnRestarting(IContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnRestart(IContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnStop(IContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnStopped(IContext context)
        {
            _disposables.Dispose();

            return Task.CompletedTask;
        }

        protected virtual Task Stopping(IContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OtherSystemMessage(IContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnSystemStarted(SystemStartedEvent systemStartedEvent)
        {
            return Task.CompletedTask;
        }

        protected void Subscribe<T>(bool subscribeOnParent = false, RoutingFilter filter = null) where T : ActorMessage
        {
            _disposables.Add(MessageBroker.SubscribeForMessage<T>(Self, subscribeOnParent, filter));
        }

        protected void Subscribe<T, R>(bool subscribeOnParent = false, RoutingFilter filter = null) where T : Query
        {
            _disposables.Add(MessageBroker.SubscribeForQuery<T, R>(Self, subscribeOnParent, filter));
        }
    }
}