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
using Coursework.DSInterfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Coursework.Droid.MapService))]

namespace Coursework.Droid
{
    [Activity (Label = "MapService", Theme = "@style/AppTheme")]
    class MapService : IMap
    {
     

        public void DisplaySelectedConstructionOnMap()
        {
            var intent = new Intent(Forms.Context, typeof(DisplaySelectedConstructionOnMapActivity));
            Forms.Context.StartActivity(intent);
        }

        public void DisplaySearchResultOnMap()
        {
            var intent = new Intent(Forms.Context, typeof(DisplaySearchResultOnMapActivity));
            Forms.Context.StartActivity(intent);

        }


    }
}