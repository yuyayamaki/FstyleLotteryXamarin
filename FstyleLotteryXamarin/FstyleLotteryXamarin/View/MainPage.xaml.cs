using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FstyleLotteryXamarin.View
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

			defaultView.BindingContext = new ViewModel.MainViewModel ();
        }
    }
}
