using FstyleLotteryXamarin.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundPlayer))]

namespace FstyleLotteryXamarin.WinPhone
{
    public class SoundPlayer : ISoundPlayer
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

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
