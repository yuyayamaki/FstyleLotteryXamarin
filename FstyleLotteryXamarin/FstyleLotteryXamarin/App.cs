using FstyleLotteryXamarin.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FstyleLotteryXamarin
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new ContentPage
            {
                Content = new Label
                {
                    Text = "Hello, Forms !",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                },
            };
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
    }
}
