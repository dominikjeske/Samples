namespace HomeCenter.Abstractions
{
    public static class MessageProperties
    {
        //Common
        public const string MessageSource = nameof(MessageSource);

        public const string MessageDestination = nameof(MessageDestination);
        public const string LogLevel = nameof(LogLevel);
        public const string Type = nameof(Type);
        public const string Uid = nameof(Uid);
        public const string IsEnabled = nameof(IsEnabled);
        public const string Tags = nameof(Tags);
        public const string TimeOut = nameof(TimeOut);
        public const string Context = nameof(Context);

        //Command
        public const string IsFinishComand = nameof(IsFinishComand);

        public const string ExecutionDelay = nameof(ExecutionDelay);
        public const string CancelPrevious = nameof(CancelPrevious);
        public const string StateName = nameof(StateName);
        public const string ChangeFactor = nameof(ChangeFactor);
        public const string Value = nameof(Value);
        public const string InputSource = nameof(InputSource);
        public const string SurroundMode = nameof(SurroundMode);
        public const string Repeat = nameof(Repeat);
        public const string Code = nameof(Code);
        public const string StateTime = nameof(StateTime);
        public const string PowerLevel = nameof(PowerLevel);
        public const string Delta = nameof(Delta);

        //Events
        public const string OldValue = nameof(OldValue);

        public const string NewValue = nameof(NewValue);
        public const string EventTime = nameof(EventTime);
        public const string CommandCode = nameof(CommandCode);
        public const string EventTriggerSource = nameof(EventTriggerSource);

        //Adapter
        public const string Address = nameof(Address);

        public const string PinNumber = nameof(PinNumber);
        public const string PinMode = nameof(PinMode);
        public const string ReversePinLevel = nameof(ReversePinLevel);
        public const string PoolInterval = nameof(PoolInterval);
        public const string PollDurationWarningThreshold = nameof(PollDurationWarningThreshold);
        public const string Hostname = nameof(Hostname);
        public const string Zone = nameof(Zone);
        public const string UserName = nameof(UserName);
        public const string Password = nameof(Password);
        public const string Port = nameof(Port);
        public const string MAC = nameof(MAC);
        public const string AuthKey = nameof(AuthKey);
        public const string AppKey = nameof(AppKey);
        public const string System = nameof(System);
        public const string Bits = nameof(Bits);
        public const string Unit = nameof(Unit);
        public const string AdapterName = nameof(AdapterName);
        public const string AdapterAuthor = nameof(AdapterAuthor);
        public const string AdapterDescription = nameof(AdapterDescription);
        public const string ClientID = nameof(ClientID);
        public const string PinChangeWithPullUp = nameof(PinChangeWithPullUp);
        public const string PinChangeWithPullDown = nameof(PinChangeWithPullDown);
        public const string IsRising = nameof(IsRising);
        public const string InterruptPin = nameof(InterruptPin);
        public const string InterruptSource = nameof(InterruptSource);
        public const string InfraredAdapter = nameof(InfraredAdapter);
        public const string FirstPortWriteMode = nameof(FirstPortWriteMode);
        public const string SecondPortWriteMode = nameof(SecondPortWriteMode);
        public const string Minimum = nameof(Minimum);
        public const string Maximum = nameof(Maximum);

        //Services
        public const string Body = nameof(Body);

        public const string ContentType = nameof(ContentType);
        public const string RequestType = nameof(RequestType);
        public const string Size = nameof(Size);
        public const string IgnoreReturnStatus = nameof(IgnoreReturnStatus);
        public const string AuthorisationHeader = nameof(AuthorisationHeader);
        public const string Headers = nameof(Headers);
        public const string Creditionals = nameof(Creditionals);
        public const string Initialize = nameof(Initialize);
        public const string ComponentsAttachedProperties = nameof(ComponentsAttachedProperties);
        public const string AreasAttachedProperties = nameof(AreasAttachedProperties);

        //Components
        public const string RelayNotTranslatedEvents = nameof(RelayNotTranslatedEvents);

        public const string ComponentId = nameof(ComponentId);
    }
}