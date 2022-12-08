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
        public MainPage()
        {
            InitializeComponent();
            Core.Initialize();
            libVLC = new LibVLC();

            audio = new MediaPlayer(libVLC);
            audio.EncounteredError += Audio_EncounteredError;
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
            string url = "https://mangoradio.stream.laut.fm/mangoradio?t302=2022-12-07_13-54-16&uuid=e330361f-f9fe-4734-a64a-8adc5e30f9bb";
            var media = new Media(libVLC, url, FromType.FromLocation);
            audio.Media = media;
            audio.Media.AddOption(":no-video");
            audio.Play();
        }
    }
}
