using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Views;
using LibVLCSharp.Shared;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;

namespace AndroidApp1
{
    [Activity(Label = "StreamingActivity")]
    public class StreamingActivity : Activity
    {
        private VideoView _mVideoView;
        private LinearLayout _mVideoWrapper;
        private Button _mBackButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Streaming);
            _mVideoWrapper = FindViewById<LinearLayout>(Resource.Id.video_wrapper);
            _mBackButton = FindViewById<Button>(Resource.Id.back_button);
            _mBackButton.Click += ClickStopStreaming;
        }

        protected override void OnResume()
        {
            base.OnResume();
            _mVideoView = new VideoView(this);
            var lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            _mVideoWrapper.AddView(_mVideoView, lp);
            var media = new Media(_mVideoView.LibVLC, "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_175k.mov",
                Media.FromType.FromLocation);
            var configuration = new MediaConfiguration();
            configuration.EnableHardwareDecoding();
            media.AddOption(configuration);
            _mVideoView.MediaPlayer.Play(media);
        }

        private void ClickStopStreaming(object sender, EventArgs e)
        {
            StopStream();
        }

        protected override void OnPause()
        {
            base.OnPause();
            StopStream();
        }

        private void StopStream()
        {
            if (_mVideoView != null)
            {
                if (_mVideoView.MediaPlayer != null)
                {
                    if (_mVideoView.MediaPlayer.IsPlaying)
                    {
                        Log.Debug("StreamingActivity", "MediaPlayer.Stop ");
                        _mVideoView.MediaPlayer.Stop();
                    }
                }

                _mVideoWrapper.RemoveView(_mVideoView);
                _mVideoView.Dispose();
            }
        }
    }
}