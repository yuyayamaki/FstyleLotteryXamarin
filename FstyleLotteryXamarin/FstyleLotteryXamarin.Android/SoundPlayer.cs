using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FstyleLotteryXamarin.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundPlayer))]

namespace FstyleLotteryXamarin.Droid
{
    public class SoundPlayer : ISoundPlayer
    {
        public SoundPlayer()
        {
            if (_soundPool == null)
                Initialize();
        }

        SoundPool _soundPool;
        IDictionary<string, int> rawIdToSoundId;

        public void Play(string audioName)
        {
            int soundId = rawIdToSoundId[audioName];
            _soundPool.Play(soundId, 1.0f, 1.0f, 1, 0, 1.0f);
        }

        public void LoopPlay(string audioName)
        {
            int soundId = rawIdToSoundId[audioName];
            _soundPool.Play(soundId, 1.0f, 1.0f, 1, 1, 1.0f);
        }

        public void Stop(string audioName)
        {
            int soundId = rawIdToSoundId[audioName];
            _soundPool.Stop(soundId);
        }

        public void Initialize()
        {
            rawIdToSoundId = new Dictionary<string, int>();
            var c = Forms.Context; 
            _soundPool = new SoundPool(8, Stream.Music, 0);

            int soundId1 = _soundPool.Load(c, Resource.Raw.b_001, 1);
            rawIdToSoundId.Add("b_001", soundId1);

            int soundId2 = _soundPool.Load(c, Resource.Raw.ji_017, 1);
            rawIdToSoundId.Add("ji_017", soundId2);

            //int soundId3 = _soundPool.Load(c, Resource.Raw.lo_040, 1);
            //rawIdToSoundId.Add("lo_040", soundId3);
        }

        public void Dispose()
        {
            if (_soundPool != null)
                _soundPool.Release();
        }
    }
}