using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Coursework.Model;


namespace Coursework.Droid
{
    [Activity]
    public class DisplaySelectedConstructionOnMapActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap map;
      
        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            
            LatLng latLngObject = new LatLng(ConfigurationManager.Configuration.SelectedConstruction.Lat, ConfigurationManager.Configuration.SelectedConstruction.Long);
            LatLng latLngUser = new LatLng(ConfigurationManager.Configuration.UserLtd, ConfigurationManager.Configuration.UserLnt);
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latLngObject, 16);
            map.MoveCamera(camera);

            MarkerOptions options1 = new MarkerOptions().SetPosition(latLngObject).SetTitle(ConfigurationManager.Configuration.SelectedConstruction.Name);
            MarkerOptions options2 = new MarkerOptions().SetPosition(latLngUser).SetTitle(App.CurrentLanguage == "en" ? "You are here" : "Вы здесь")
                .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.baseline_person_pin_black_36));
            map.AddMarker(options2);
            map.AddMarker(options1);
        }
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
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
    }
}