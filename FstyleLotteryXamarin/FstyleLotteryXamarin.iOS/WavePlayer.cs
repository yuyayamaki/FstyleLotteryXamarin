using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using FstyleLotteryXamarin.iOS;

[assembly: Dependency(typeof(WavePlayer))]

namespace FstyleLotteryXamarin.iOS
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
