using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;
using Coursework.Model;
using static Android.Gms.Maps.GoogleMap;


using Coursework.ViewModel;
using Android.Content.Res;
using Coursework;
using Android.Net;
using Android.Graphics;
using System.Net;
using Android.Text.Method;
using FFImageLoading;
using FFImageLoading.Work;
using System.Threading.Tasks;
using System;
using System.Threading;
using Square.Picasso;
using System.Collections.Generic;

namespace Coursework.Droid
{
    [Activity]
    internal class DisplaySearchResultOnMapActivity : Activity, IOnMapReadyCallback, IInfoWindowAdapter, IOnInfoWindowClickListener
    {
#warning "FIX INFO WINDOW!"
        private GoogleMap map;
        private Construction selectedConstruction;


        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            selectedConstruction = new Construction();

            LatLng latLngUser = new LatLng(ConfigurationManager.Configuration.UserLtd, ConfigurationManager.Configuration.UserLnt);
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latLngUser, 11);
            map.MoveCamera(camera);
            string message = App.CurrentLanguage == "en" ? "You are here" : "Вы здесь";
            MarkerOptions options2 = new MarkerOptions().SetPosition(latLngUser).SetTitle(message).SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.baseline_person_pin_black_36));
            map.AddMarker(options2);
            
           

            if (ConfigurationManager.Configuration.DisplayedConstructions.Count != 0)
            {
                map.SetInfoWindowAdapter(this);
                map.SetOnInfoWindowClickListener(this);
                List<Marker> markers = new List<Marker>();
                ConfigurationManager.Configuration.DisplayedConstructions.ForEach((i) =>
                {
                    LatLng latLngObject = new LatLng(i.Lat, i.Long);
                    MarkerOptions options1 = new MarkerOptions();
                    options1.SetPosition(latLngObject).SetTitle(i.Name);
                    Marker m = map.AddMarker(options1);
                    m.ShowInfoWindow();
                    m.HideInfoWindow();
                });

            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            Task.Run(() => {

                foreach(var c in ConfigurationManager.Configuration.DisplayedConstructions)
                {
                    ImageService.Instance.LoadUrl(c.Image, TimeSpan.FromDays(7)).DownloadOnly();
                }
            });
            SetContentView(Resource.Layout.MapLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            Title = App.CurrentLanguage == "en" ? "Map" : "Карта"; 
            SetUpMap();
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpMap()
        {
            if (map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            LatLng latLngUser = new LatLng(ConfigurationManager.Configuration.UserLtd, ConfigurationManager.Configuration.UserLnt);
            
            if (!marker.Position.Equals(latLngUser) ) {
                selectedConstruction = ConfigurationManager.Configuration.DisplayedConstructions.Find((i) => i.Name.CompareTo(marker.Title) == 0);
                try
                {
                    Android.Views.View view = LayoutInflater.Inflate(Resource.Layout.InfoWindowLayout, null, false);
                    ImageView iv = view.FindViewById<ImageView>(Resource.Id.iv);
                    view.FindViewById<TextView>(Resource.Id.txtName).Text = selectedConstruction.Name + " (" + selectedConstruction.Address + ")";
                    ImageService.Instance.LoadUrl(selectedConstruction.Image).Into(iv);
                    return view;
                }catch (Exception)
                {
                    string message = App.CurrentLanguage == "en" ? "No internet connection avaible" : "Отсутствует соединение с интернетом";
                    Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
                }
            }
            return null;
        }


        public void OnInfoWindowClick(Marker marker)
        {

            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle(selectedConstruction.Name);
            alertDiag.SetMessage(selectedConstruction.Description);
            alertDiag.SetPositiveButton("Ok", (senderAlert, args) => {
                alertDiag.Dispose();
            });
          
            Dialog diag = alertDiag.Create();
            diag.Show();

        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

    }
}