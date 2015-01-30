using FstyleLotteryXamarin.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FstyleLotteryXamarin
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new View.MainPage();
        }

        private static LotteryModel _model;

        /// <summary>
        /// 静的 Model です。
        /// </summary>
        /// <returns>Letters オブジェクトです。</returns>
        public static LotteryModel Model
        {
            get
            {
                // 必要になるまでモデルの作成を延期します
                if (_model == null)
                    _model = new LotteryModel();

                return _model;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            DependencyService.Get<ISoundPlayer>().Initialize();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            DependencyService.Get<ISoundPlayer>().Dispose();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            DependencyService.Get<ISoundPlayer>().Initialize();
        }
    }
}
