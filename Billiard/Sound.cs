using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiard
{
    public static class Sound
    {
        public static void MakeSound(string SoundFile)
        {
            var audioFile = new WaveFileReader(SoundFile);
            var waveOut = new WaveOut();
            waveOut.DeviceNumber = 0;
            waveOut.Init(audioFile);
            waveOut.Play();
        }
        public static void MakeSoundOnLoop(string SoundFile)
        {
            var audioFile = new WaveFileReader(SoundFile);
            var loopingStream = new LoopStream(audioFile);
            var waveOut = new WaveOut();
            waveOut.DeviceNumber = 0;
            waveOut.Init(loopingStream);
            waveOut.Play();
        }
    }
}
