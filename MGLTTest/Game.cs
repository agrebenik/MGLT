using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MGLT;
using Microsoft.Xna.Framework.Audio;

namespace MGLTTest
{
    class Game : MGLG
    {
        public Game() : base(400, 300) { }
        private static Microsoft.Xna.Framework.Audio.Microphone mic;
        private static int bufferIndex = 0;
        private static byte[] data;
        
        protected override void LoadContent()
        {
            base.LoadContent();
            //MGLM.Add(new MicTest((0, 0)));
            Console.WriteLine("Capture devices:");
            foreach (var device in Microsoft.Xna.Framework.Audio.Microphone.All)
                Console.WriteLine("\t" + device.Name);

            // Use default capture device
            mic = Microsoft.Xna.Framework.Audio.Microphone.Default;
            Console.WriteLine("\nUsing default capture device:\n\t" + mic.Name);

            // Prepare record buffer for 5 seconds use
            TimeSpan recDuration = TimeSpan.FromSeconds(5);
            mic.BufferDuration = TimeSpan.FromSeconds(1);
            data = new byte[mic.GetSampleSizeInBytes(recDuration)];

            // Bind event handler for when audio data becomes available
            mic.BufferReady += DataAvailable;

            // Record audio for 5 seconds
            mic.Start();
            Console.WriteLine("Recording...");
            Thread.Sleep(recDuration);
            mic.Stop();

            // Replay audio
            Console.WriteLine("Playing...");
            using (SoundEffect sound = new SoundEffect(data, 44100, AudioChannels.Mono))
                sound.Play();

            Console.ReadLine();
        }

        private static void DataAvailable(object sender, System.EventArgs e)
        {
            // Prepare buffer for available samples
            byte[] samples = new byte[mic.GetSampleSizeInBytes(TimeSpan.FromSeconds(1))];

            // Read available samples into our buffer
            int sampleSize = mic.GetData(samples);

            // Merge these samples into our overall buffer and don't overrun it
            int cropSize = sampleSize + bufferIndex > data.Length ? data.Length - bufferIndex : sampleSize;
            System.Array.Copy(samples, 0, data, bufferIndex, cropSize);
            bufferIndex += cropSize;
        }
    }
}
