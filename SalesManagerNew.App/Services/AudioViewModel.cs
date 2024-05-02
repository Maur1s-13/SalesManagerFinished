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

        public bool Muted = false;

        public AudioViewModel(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
            

        }

        public async void PlayAudioSucess()
        {
            if (Muted == false)
            {
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("sucess.wav"));

                audioPlayer.Volume = 1.5;

                audioPlayer.Play();
            }
            else if (Muted = true)
            {

            }

           
        }

        public async void PlayAudioFail()
        {
            if (Muted == false)
            {
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("fail.wav"));

                audioPlayer.Play();
            }
            else if (Muted == true)
            {

            }

            
        }

        
    }
}
