using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FstyleLotteryXamarin
{
    public interface IWavePlayer
    {
        void Play(string soundName);
        void LoopPlay(string soundName);
        void Stop(string soundName);
    }
}
