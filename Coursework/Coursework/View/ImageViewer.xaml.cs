using Coursework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageViewer : ContentPage
	{
		public ImageViewer (Construction c)
		{
            InitializeComponent();
            BindingContext = new { Image = c.Image, Name = c.Name };
        }
	}
}