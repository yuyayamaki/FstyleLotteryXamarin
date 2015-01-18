using FstyleLotteryXamarin.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(WavePlayer))]

namespace FstyleLotteryXamarin.WinPhone
{
    public class WavePlayer : IWavePlayer
    {
        public void LoopPlay(string soundName)
        {
            throw new NotImplementedException();
        }

        public void Play(string soundName)
        {
            throw new NotImplementedException();
        }

        public void Stop(string soundName)
        {
            throw new NotImplementedException();
        }
    }
}
