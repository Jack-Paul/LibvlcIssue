using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibVlcSharpIssue
{
    public partial class MainPage : ContentPage
    {
        public LibVLC libVLC;
        public MediaPlayer audio;
        public string Log;
        public MainPage()
        {
            InitializeComponent();
            Core.Initialize();
            libVLC = new LibVLC(enableDebugLogs: true);

            audio = new MediaPlayer(libVLC);
            audio.EncounteredError += Audio_EncounteredError;
            libVLC.Log += LibVLC_Log;
        }

        private void LibVLC_Log(object sender, LogEventArgs e)
        {
            Log +="Time" + DateTime.Now + System.Environment.NewLine +
                "Level" + e.Level + System.Environment.NewLine +
                "Message:" + e.Message + System.Environment.NewLine +
                "Module:" + e.Module + System.Environment.NewLine +
                "Soruce File" + e.SourceFile + System.Environment.NewLine +
                "Source Line" + e.SourceLine + System.Environment.NewLine + System.Environment.NewLine
                ;
        }

        private void Audio_EncounteredError(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Failed to play", "failed to play", "ok");
            });
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            audio.Stop();
            string url = "https://mangoradio.stream.laut.fm/mangoradio?t302=2022-12-07_13-54-16&uuid=e330361f-f9fe-4734-a64a-8adc5e30f9bb";
            var media = new Media(libVLC, url, FromType.FromLocation);
            audio.Media = media;
            audio.Media.AddOption(":no-video");
            audio.Play();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            audio.Stop();
            string url = "https://live.trucksim.fm/";
            var media = new Media(libVLC, url, FromType.FromLocation);
            audio.Media = media;
            audio.Media.AddOption(":no-video");
            audio.Play();
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var s = Log;
        }
    }
}
