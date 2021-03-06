﻿using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class ModeSetCommand : Command
    {
        public string SurroundMode
        {
            get => this.AsString(MessageProperties.SurroundMode);
            set => this.SetProperty(MessageProperties.SurroundMode, value);
        }

        public static ModeSetCommand Create(string mode)
        {
            var command = new ModeSetCommand
            {
                SurroundMode = mode
            };
            return command;
        }
    }
}