using FstyleLotteryXamarin.DataModel;
using FstyleLotteryXamarin.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace FstyleLotteryXamarin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            IsStartButtonVisible = true;

            lotteryModel = App.Model;
            lotteryModel.SetTextLotteryItems();
            lotteryModel.GenerateLotteryItems();

            this.lotteryViewItems.AddRange(GetStringListFromLotteryItems(lotteryModel.MainLotteryItems).ToArray());

            // Case of less than 7 items
            if (lotteryModel.MainLotteryItems.Count <= 6)
            {
                switch (lotteryModel.MainLotteryItems.Count)
                {
                    case 2:
                        for (int i = 0; i < 2; i++)
                        {
                            lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                            lotteryViewItems.Add(lotteryModel.MainLotteryItems[1].Text);
                        }
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                        break;
                    case 3:
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[1].Text);
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[2].Text);
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                        break;
                    case 4:
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[1].Text);
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[2].Text);
                        break;
                    case 5:
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[1].Text);
                        break;
                    case 6:
                        lotteryViewItems.Add(lotteryModel.MainLotteryItems[0].Text);
                        break;
                }

            }

            this.setInitialItems();
        }

        private List<string> GetStringListFromLotteryItems(ObservableCollection<LotteryItem> observableCollection)
        {
            var tempList = new List<string>();
            foreach (var item in observableCollection)
            {
                tempList.Add(item.Text);
            }
            return tempList;
        }

        private LotteryModel lotteryModel;

        private List<string> lotteryViewItems = new List<string>();

        private int currentFirstItemIndex = 6;

        private string _text1;
        private string _text2;
        private string _text3;
        private string _text4;
        private string _text5;
        private string _text6;
        private string _text7;
        public string Text1
        {
            get { return _text1; }
            set
            {
                Set(ref _text1, value);
            }
        }

        public string Text2
        {
            get { return _text2; }
            set
            {
                Set(ref _text2, value);
            }
        }

        public string Text3
        {
            get { return _text3; }
            set
            {
                Set(ref _text3, value);
            }
        }

        public string Text4
        {
            get { return _text4; }
            set
            {
                Set(ref _text4, value);
            }
        }

        public string Text5
        {
            get { return _text5; }
            set
            {
                Set(ref _text5, value);
            }
        }

        public string Text6
        {
            get { return _text6; }
            set
            {
                Set(ref _text6, value);
            }
        }

        public string Text7
        {
            get { return _text7; }
            set
            {
                Set(ref _text7, value);
            }
        }

        private bool _isStartButtonVisible;
        public bool IsStartButtonVisible
        {
            get { return _isStartButtonVisible; }
            set
            {
                Set(ref _isStartButtonVisible, value);
                RaisePropertyChanged(() => IsStopButtonVisible);
            }
        }
        public bool IsStopButtonVisible
        {
            get
            {
                return IsStartButtonVisible == true ? false : true;
            }
        }

        // LegendMode means do not care the item has already elected
        private bool _isLegendMode = false;
        public bool IsLegendMode
        {
            get
            {
                return _isLegendMode;
            }
            set
            {
                Set(ref _isLegendMode, value);
                if (value)
                {
                    lotteryModel.ResetAllIsNotYet();
                    if (IsStartButtonVisible == true && CanExcuteStartCommand == false)
                        CanExcuteStartCommand = true;
                }
            }
        }

        private RelayCommand _startCommand;
        private RelayCommand _stopCommand;
        private RelayCommand _cleanUpCommand;

        private bool _canExcuteStartCommand = true;
        private bool _canExcuteStopCommand = false;

        public bool CanExcuteStartCommand
        {
            get
            {
                return _canExcuteStartCommand;
            }
            set
            {
                Set(ref _canExcuteStartCommand, value);
                StartCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanExcuteStopCommand
        {
            get
            {
                return _canExcuteStopCommand;
            }
            set
            {
                Set(ref _canExcuteStopCommand, value);
                StopCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the StartCommand.
        /// </summary>
        public RelayCommand StartCommand
        {
            get
            {
                return _startCommand
                    ?? (_startCommand = new RelayCommand(new Action(
                                          () =>
                                          {
                                              DependencyService.Get<IRouletteMusicPlayer>().LoopPlay();

                                              IsStartButtonVisible = false;
                                              CanExcuteStopCommand = false;
                                              CanExcuteStartCommand = false;

                                              // Timer for tick
                                              Device.StartTimer(new TimeSpan(0, 0, 0, 0, 125), rouletteTimer_Tick);
                                          }),
                                          () => CanExcuteStartCommand));
            }
        }

        /// <summary>
        /// Gets the StopCommand.
        /// </summary>
        public RelayCommand StopCommand
        {
            get
            {
                return _stopCommand
                    ?? (_stopCommand = new RelayCommand(
                                          () =>
                                          {
                                              IsStartButtonVisible = true;
                                              isStopButtonClicked = true;
                                          },
                                          () => CanExcuteStopCommand));
            }
        }

        /// <summary>
        /// Gets the CleanUpCommand.
        /// </summary>
        public RelayCommand CleanUpCommand
        {
            get
            {
                return _cleanUpCommand
                    ?? (_cleanUpCommand = new RelayCommand(new Action(
                                          () =>
                                          {
                                              //wavePlayer.Dispose();
                                              //rouletteMusicPlayer.Dispose();
                                          })));
            }
        }

        private bool isStopButtonClicked = false;
        private int countUpForSkip = 0;
        private int limitCount = 8;

        private bool rouletteTimer_Tick()
        {
            if (!isStopButtonClicked)
            {
                if (countUpForSkip >= limitCount)
                {
                    shiftText();

                    this.PlaySound("b_001");

                    if (limitCount > 1)
                    {
                        limitCount = limitCount - 1;
                        countUpForSkip = 0;
                    }
                    else
                    {
                        if (_canExcuteStopCommand == false)
                            CanExcuteStopCommand = true;
                    }
                }
                else
                {
                    countUpForSkip++;
                }
            }
            else // Slow down tick
            {
                if (countUpForSkip >= limitCount)
                {
                    shiftText();

                    this.PlaySound("b_001");

                    if (limitCount < 8)
                    {
                        limitCount = limitCount + 1;
                        countUpForSkip = 0;
                    }
                    else
                    {
                        if (_isLegendMode == false)
                        {
                            if (lotteryModel.MainLotteryItems.Where(item => item.Text == _text4 && item.IsNotYet == true).Count() > 0)
                            {
                                // Mark as has already elected
                                lotteryModel.MainLotteryItems.Where(item => item.Text == _text4 && item.IsNotYet == true).First().IsNotYet = false;
                            }
                            else
                            {
                                // Skip this item
                                countUpForSkip = 0;
                                return true;
                            }
                        }

                        if (lotteryModel.MainLotteryItems.Where(item => item.IsNotYet == true).Count() > 0)
                            CanExcuteStartCommand = true;

                        Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(c =>
                        {
                            isStopButtonClicked = false;

                            DependencyService.Get<IRouletteMusicPlayer>().Stop();
                            this.PlaySound("ji_017");
                        });

                        return false;
                    }
                }
                else
                {
                    countUpForSkip++;
                }
            }

            return true;
        }

        private void shiftText()
        {
            Text1 = _text2;
            Text2 = _text3;
            Text3 = _text4;
            Text4 = _text5;
            Text5 = _text6;
            Text6 = _text7;

            if (lotteryModel.MainLotteryItems.Count >= 7)
                Text7 = lotteryViewItems[currentFirstItemIndex];
            else
            {
                switch (lotteryModel.MainLotteryItems.Count)
                {
                    case 6:
                        if (currentFirstItemIndex + 1 > 6)
                            currentFirstItemIndex -= 6;
                        Text7 = lotteryViewItems[currentFirstItemIndex + 1];
                        break;
                    case 5:
                        if (currentFirstItemIndex + 1 > 5)
                            currentFirstItemIndex -= 5;
                        Text7 = lotteryViewItems[currentFirstItemIndex + 2];
                        break;
                    case 4:
                        if (currentFirstItemIndex + 1 > 4)
                            currentFirstItemIndex -= 4;
                        Text7 = lotteryViewItems[currentFirstItemIndex];
                        break;
                    case 3:
                        while (currentFirstItemIndex + 1 > 3)
                        {
                            currentFirstItemIndex -= 3;
                        }
                        Text7 = lotteryViewItems[currentFirstItemIndex];
                        break;
                    case 2:
                        while (currentFirstItemIndex + 1 > 2)
                        {
                            currentFirstItemIndex -= 2;
                        }
                        Text7 = lotteryViewItems[currentFirstItemIndex];
                        break;
                }
            }

            if (currentFirstItemIndex == lotteryModel.MainLotteryItems.Count - 1)
            {
                currentFirstItemIndex = 0;
            }
            else
            {
                currentFirstItemIndex++;
            }
        }

        private void setInitialItems()
        {
            Text1 = lotteryViewItems[0];
            Text2 = lotteryViewItems[1];
            Text3 = lotteryViewItems[2];
            Text4 = lotteryViewItems[3];
            Text5 = lotteryViewItems[4];
            Text6 = lotteryViewItems[5];
            Text7 = lotteryViewItems[6];
        }

        private void PlaySound(string soundId)
        {
            DependencyService.Get<ISoundPlayer>().Play(soundId);
        }

        public string StartButtonUiResource
        {
            get { return Strings.Resource.StartButton; }
        }

        public string StopButtonUiResource
        {
            get { return Strings.Resource.StopButton; }
        }
    }
}
