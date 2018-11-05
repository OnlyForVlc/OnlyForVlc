using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AndroidApp1
{
    [Activity(Label = "MainActivity", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            var startButton = FindViewById<Button>(Resource.Id.start_button);
            startButton.Click += ClickStartStreaming;
        }

        private void ClickStartStreaming(object sender, EventArgs e)
        {
            var streamIntent = new Intent(this,typeof(StreamingActivity));
            StartActivity(streamIntent);
        }
    }
}