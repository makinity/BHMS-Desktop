using System;
using System.IO;
using WMPLib;

namespace BoardingHouse
{
    public static class SoundClicked
    {
        private static readonly WindowsMediaPlayer _player = new WindowsMediaPlayer();
        private const string SoundFolder = "Sound";

        private static void Play(string fileName)
        {
            string soundPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                SoundFolder,
                fileName
            );

            if (!File.Exists(soundPath)) return;

            _player.controls.stop();
            _player.URL = soundPath;
            _player.controls.play();
        }

        public static void sidebarButton()
        {
            Play("sidebarBtnClicked.mp3");
        }

        public static void operationsBtn()
        {
            Play("operationsBtn.mp3");
        }

        public static void itemClicked()
        {
            Play("itemClicked.mp3");
        }
    }
}
