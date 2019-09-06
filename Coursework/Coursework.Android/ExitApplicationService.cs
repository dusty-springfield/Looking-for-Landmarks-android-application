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

[assembly: Xamarin.Forms.Dependency(typeof(Coursework.Droid.ExitApplicationService))]
namespace Coursework.Droid
{
    public class ExitApplicationService : IExitApplication
    {
        public void ExitApplication()
        {
            Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}