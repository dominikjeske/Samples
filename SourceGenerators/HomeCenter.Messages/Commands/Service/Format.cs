using System;

namespace HomeCenter.Messages.Commands.Service
{
    public class Format
    {
        public int Lp;
        public string ValueName;
        public Type ValueType;

        public Format(int lp, Type valueType, string valueName)
        {
            Lp = lp;
            ValueType = valueType;
            ValueName = valueName;
        }
    }
}