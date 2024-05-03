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
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("sucess.wav"));

                audioPlayer.Volume = 1.5;

                currentAudioPlayer = audioPlayer;

                currentAudioPlayer.Play();
            }
            else if (Mute == true)
            {
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("sucess.wav"));

                audioPlayer.Volume = 0;

                currentAudioPlayer = audioPlayer;

                currentAudioPlayer.Play();
            }
            
        }

        public async void MuteSound(bool mute)
        {
            Mute = mute; // Update the Mute property

            // Adjust the volume of the audio player if it's playing
            if (mute && currentAudioPlayer != null)
            {
                // Set volume to 0 to mute
                currentAudioPlayer.Volume = 0;
            }
            else if (!mute && currentAudioPlayer != null)
            {
                // Restore original volume
                currentAudioPlayer.Volume = 1.5; // Or whatever the original volume was
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
                var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("fail.wav"));

                audioPlayer.Volume = 0;

                currentAudioPlayer = audioPlayer;

                currentAudioPlayer.Play();
            }
                
          
        }

        
    }
}
