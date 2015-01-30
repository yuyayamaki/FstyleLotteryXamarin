using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FstyleLotteryXamarin
{
    public interface ISoundPlayer
    {
        void Play(string soundName);
        void LoopPlay(string soundName);
        void Stop(string soundName);
        void Initialize();
        void Dispose();
    }
}
