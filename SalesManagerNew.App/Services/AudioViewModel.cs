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

        private IAudioPlayer currentAudioPlayer;

        public bool Mute;

        public AudioViewModel(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
            
        }


        public async void PlayAudioSucess()
        {

            if ( Mute == false)
            {
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("sucessfull.mp3"));

                audioPlayer.Volume = 1.5;

                currentAudioPlayer = audioPlayer;

                currentAudioPlayer.Play();
            }
            else if (Mute == true)
            {
                
            }
            
        }

        public async void MuteSound(bool mute)
        {
            Mute = mute; 

           
            if (mute && currentAudioPlayer != null)
            {
                
                currentAudioPlayer.Volume = 0;
            }
            else if (!mute && currentAudioPlayer != null)
            {
                
                currentAudioPlayer.Volume = 1.5; 
            }
        }

        public async void PlayAudioFail()
        {

            if (Mute == false)
            {
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("fail.wav"));

                audioPlayer.Volume = 1.5;

                currentAudioPlayer = audioPlayer;

                currentAudioPlayer.Play();

            }
            else if (Mute == true)
            {
                
            }
                
          
        }

        
    }
}
