using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Service
{
    public class StartSystemCommand : Command
    {
        public string AdapterMode
        {
            get => this.AsString(nameof(AdapterMode), "Embedded");
            set => this.SetProperty(nameof(AdapterMode), value);
        }

        public string Configuration
        {
            get => this.AsString(nameof(Configuration));
            set => this.SetProperty(nameof(Configuration), value);
        }

        public static StartSystemCommand Create(string configuration, string mode = "Embedded") => new StartSystemCommand { AdapterMode = mode, Configuration = configuration };
    }
}