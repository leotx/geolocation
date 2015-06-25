using System;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Widget;

namespace Geolocation
{
    [Activity(Label = "Geolocation", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {
        private LocationManager LocMgr { get; set; }
        private TextView Latitude { get; set; }
        private TextView Longitude { get; set; }

        public void OnLocationChanged(Location location)
        {
            Latitude.Text = "Latitude: " + location.Latitude;
            Longitude.Text = "Longitude: " + location.Longitude;
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            LocMgr = GetSystemService(LocationService) as LocationManager;
            Latitude = FindViewById<TextView>(Resource.Id.txtLatitude);
            Longitude = FindViewById<TextView>(Resource.Id.txtLongitude);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var locationCriteria = new Criteria {Accuracy = Accuracy.Coarse, PowerRequirement = Power.Medium};

            var locationProvider = LocMgr.GetBestProvider(locationCriteria, true);

            if (locationProvider != null)
            {
                LocMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
            }
        }
    }
}