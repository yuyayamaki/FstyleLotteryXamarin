using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FstyleLotteryXamarin.View
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

			defaultView.BindingContext = new ViewModel.MainViewModel ();

            this.SizeChanged += OnPageSizeChanged;
        }
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            var scaleRatio = (this.Width / 533.3 < this.Height /294) ? this.Width / 533.3 : this.Height /294;

            var labelFontSize = Device.OnPlatform(32, 17.8 * scaleRatio, 32);
            foreach (var visualElement in rouletteItemGrid.Children)
            {
                if (visualElement.GetType().Equals(typeof(Label)))
                {
                    ((Label)visualElement).FontSize = labelFontSize;
                }
            }

            var buttonFontSize = Device.OnPlatform(45, 25 * scaleRatio, 45);
            startButton.FontSize = buttonFontSize;
            stopButton.FontSize = buttonFontSize;

            var imageMargin = Device.OnPlatform(
                new Thickness(-130, -145, -250, 5.6 * scaleRatio),
                new Thickness(-72 * scaleRatio, -78 * scaleRatio, -139 * scaleRatio, 10),
                new Thickness(-130, -145, -250, 10));
            imageContent.Padding = imageMargin;

            buttonPanel.Padding = Device.OnPlatform(15, 8 * scaleRatio, 15);
        }
    }
}
