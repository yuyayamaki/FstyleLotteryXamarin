﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using FstyleLotteryXamarin.Helpers;

namespace FstyleLotteryXamarin.DataModel
{
    public class LotteryModel : ObservableObject
    {
        public LotteryModel()
        {
            defaultTextLotteryItems.CollectionChanged += TextLotteryItems_CollectionChanged;
            TextLotteryItems.CollectionChanged += TextLotteryItems_CollectionChanged;

            //// Add DataChanged event handler of RoamingSettings
            //Windows.Storage.ApplicationData.Current.DataChanged += RoamingSettings_DataChanged;
        }

        void TextLotteryItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        //// CoreDispatcher for runing on UI thread
        //public Windows.UI.Core.CoreDispatcher CurrentDispatcher { get; set; }

        //async void RoamingSettings_DataChanged(Windows.Storage.ApplicationData sender, object args)
        //{
        //    if (CurrentDispatcher == null)
        //        throw new InvalidOperationException("RoamingOptionSettingsのCurrentDispatcherプロパティを事前に設定してください。");

        //    await CurrentDispatcher.RunAsync(
        //            Windows.UI.Core.CoreDispatcherPriority.Normal,
        //            () =>
        //            {
        //                // Usually run RasiePropertyChanged
        //            });
        //}

        public ObservableCollection<LotteryItem> MainLotteryItems;
        private ObservableCollection<LotteryItem> tempNumberLotteryItems;

        private ObservableCollection<LotteryItem> defaultTextLotteryItems = new ObservableCollection<LotteryItem>{
    new LotteryItem(Strings.Resource.LotteryItem1),
    new LotteryItem(Strings.Resource.LotteryItem2),
    new LotteryItem(Strings.Resource.LotteryItem3),
    new LotteryItem(Strings.Resource.LotteryItem4),
    new LotteryItem(Strings.Resource.LotteryItem5),
    new LotteryItem(Strings.Resource.LotteryItem6),
    new LotteryItem(Strings.Resource.LotteryItem7)};

        private ObservableCollection<LotteryItem> _textLotteryItems;
        public ObservableCollection<LotteryItem> TextLotteryItems
        {
            get
            {
                if(_textLotteryItems == null)
                    GetRestoredTextLotteryItems();
                return _textLotteryItems;
            }
        }

        public bool IsNumberMode
        {
            get { return Settings.GetValue<bool>(false); }
            set { Settings.SetValue<bool>(value); }
        }

        public int NumberLottteryItemsCount
        {
            get { return Settings.GetValue<int>(50); }
            set { Settings.SetValue<int>(value); }
        }


        public bool IsShuffled
        {
            get { return Settings.GetValue<bool>(true); }
            set { Settings.SetValue<bool>(value); }
        }

        public void GenerateLotteryItems()
        {
            if (IsNumberMode)
            {
                tempNumberLotteryItems = new ObservableCollection<LotteryItem>();

                for (int i = 0; i < NumberLottteryItemsCount; i++)
                {
                    tempNumberLotteryItems.Add(new LotteryItem((i + 1).ToString()));
                }

                if (IsShuffled)
                    MainLotteryItems = new ObservableCollection<LotteryItem>(tempNumberLotteryItems.OrderBy(i => Guid.NewGuid()));
                else
                    MainLotteryItems = tempNumberLotteryItems;
            }
            else
                MainLotteryItems = TextLotteryItems;
        }
        
        //private T GetValue<T>(T defaultValue, [System.Runtime.CompilerServices.CallerMemberName] string key = "")
        //{
        //    if (!_settings.Values.ContainsKey(key))
        //        return defaultValue;

        //    object value = _settings.Values[key];
        //    return (value == null) ? default(T) : (T)value;
        //}

        //private void SetValue<T>(T newValue, [System.Runtime.CompilerServices.CallerMemberName] string key = "")
        //{
        //    _settings.Values[key] = newValue;

        //    RaisePropertyChanged(key);            
        //}

        public void SetTextLotteryItems()
        {
            string[] storedTextData = new string[_textLotteryItems.Count];
            var index = 0;
            foreach (LotteryItem item in _textLotteryItems)
            {
                storedTextData[index] = item.Text;
                index++;
            }

            var json = JsonConvert.SerializeObject(storedTextData);
            Settings.TextDataSettings = json;
        }
			
        private void GetRestoredTextLotteryItems()
        {
            var json = Settings.TextDataSettings;
			if (String.IsNullOrEmpty(json))
            {
                _textLotteryItems = defaultTextLotteryItems;
            }
            else
            {
                _textLotteryItems = new ObservableCollection<LotteryItem>();
                var storedTextData = JsonConvert.DeserializeObject<string[]>(json);
                foreach (var text in storedTextData)
                {
                    _textLotteryItems.Add(new LotteryItem(text));
                }
            }
        }

        public void ResetAllIsNotYet()
        {
            foreach (var item in MainLotteryItems)
            {
                item.IsNotYet = true;
            }
        }
    }
}
