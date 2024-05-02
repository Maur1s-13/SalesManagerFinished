using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagerNew.Lib.Interfaces;
using System.Xml.Serialization;

namespace SalesManagerNew.App.Services
{
    public class AudioViewModel : ISound
    {
        readonly IAudioManager audioManager;

        public AudioViewModel(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public async void PlayAudioSucess()
        {
            var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("sucess.wav"));

            audioPlayer.Play();
        }

        public async void PlayAudioFail()
        {
            var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("fail.wav"));

            audioPlayer.Play();
        }
    }
}
