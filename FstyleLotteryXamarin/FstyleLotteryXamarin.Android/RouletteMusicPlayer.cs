using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Media;
using Xamarin.Forms;
using FstyleLotteryXamarin.Droid;

[assembly: Dependency(typeof(RouletteMusicPlayer))]

namespace FstyleLotteryXamarin.Droid
{
    class RouletteMusicPlayer : IRouletteMusicPlayer
    {
        MediaPlayer mediaPlayer;

        public RouletteMusicPlayer()
        {
            Initialize();
        }

        private void Initialize()
        {
            var c = Forms.Context;
            mediaPlayer = MediaPlayer.Create(c, Resource.Raw.lo_040);
            mediaPlayer.Looping = true; 
        }

        public void LoopPlay()
        {
            mediaPlayer.Start();
        }

        public void Stop()
        {
            mediaPlayer.Stop();
            mediaPlayer.Prepare();
        }

        public void Dispose()
        {
            if (mediaPlayer != null)
                mediaPlayer.Release();
        }

    }
}