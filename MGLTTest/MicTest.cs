using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGLT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MGLTTest
{
    class MicTest : MGLSprite
    {
        private Microphone _curMic = null;
        private long _lastBuffer = 0;
        private int _bufferIndex = 0;
        private byte[] _audioData;
        public MicTest(MGLPoint p) : base(p,"Joh")
        {
            _curMic = getMic();
            _curMic.BufferDuration = TimeSpan.FromMilliseconds(100);
            _curMic.BufferReady += DataAvailable;
            _audioData = new byte[_curMic.GetSampleSizeInBytes(TimeSpan.FromSeconds(5))];
            _curMic.Start();
            _lastBuffer = MGLM.MS();
        }
        
        private Microphone getMic()
        {
            Microphone result = Microphone.Default;
            foreach (Microphone mic in Microphone.All)
            {
                if (mic.Name.Contains("NVIDIA RTX Voice"))
                {
                    result = mic;
                }
            }
            return result;
        }

        private void DataAvailable(object sender, System.EventArgs e)
        {
            // Prepare buffer for available samples
            byte[] samples = new byte[_curMic.GetSampleSizeInBytes(TimeSpan.FromSeconds(1))];

            // Read available samples into our buffer
            int sampleSize = _curMic.GetData(samples);

            // Merge these samples into our overall buffer and don't overrun it
            int cropSize = sampleSize + _bufferIndex > _audioData.Length ? _audioData.Length - _bufferIndex : sampleSize;
            System.Array.Copy(samples, 0, _audioData, _bufferIndex, cropSize);
            _bufferIndex += cropSize;
        }

        public override void Update(GameTime time)
        {
            if (MGLM.MS() - _lastBuffer >= 5000 && _curMic.State == MicrophoneState.Started)
            {
                Console.WriteLine("five seconds have elapsed");
                _curMic.Stop();
                using (SoundEffect sound = new SoundEffect(_audioData, 44100, AudioChannels.Mono))
                    sound.Play();
            }
        }
    }
}
