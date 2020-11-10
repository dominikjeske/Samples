using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HomeCenter.Abstractions
{
    public class ActorMessageContext
    {
        public static ActorMessageContext Create(PID actor, Command command)
        {
            var context = new ActorMessageContext { Actor = actor };
            context.Commands.Add(command);
            return context;
        }

        public static ActorMessageContext Create(PID actor, IValidable condition, params Command[] commands)
        {
            var context = new ActorMessageContext { Actor = actor, Condition = condition };
            context.Commands.AddRange(commands);
            return context;
        }

        public string GetMessageUid()
        {
            return $"{Actor.Id}-{string.Join("-", Commands.Select(c => c.Type))}";
        }

        public IValidable Condition { get; set; } = EmptyCondition.Default;
        public PID Actor { get; set; }
        public List<Command> Commands { get; set; } = new List<Command>();
        public List<Command> FinishCommands { get; set; } = new List<Command>();
        public TimeSpan? FinishCommandTime { get; set; }
        public CancellationToken Token { get; set; }
    }
}