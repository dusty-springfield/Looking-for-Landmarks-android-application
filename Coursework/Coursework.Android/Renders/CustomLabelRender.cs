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
using Coursework.Droid.Renders;
using Coursework.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRender))]
namespace Coursework.Droid.Renders
{

    public class CustomLabelRender : LabelRenderer
    {
        public CustomLabelRender() : base(){ }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                System.GC.Collect();
            }
            catch (Exception) { }
        }
    }
}