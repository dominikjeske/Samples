using Proto;
using System;

namespace HomeCenter.Abstractions
{
    public interface ITypeMapper<TConfig> : ITypeMapper
    {
        IActor Map(TConfig config, Type destinationType);
    }

    public interface ITypeMapper
    {
    }
}