using System;

namespace HomeCenter.Abstractions
{
    public interface ISoundPlayer
    {
        Action SoundEnd { get; set; }

        void Pause();

        void Play(string sound);
    }
}