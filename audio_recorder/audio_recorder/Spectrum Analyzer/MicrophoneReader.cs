using System;
using System.Threading.Tasks;

using System.Windows;

using System.Numerics;

using NAudio.Wave;

namespace audio_recorder.Spectrum_Analyzer
{
    public class MicrophoneReader
    {
        private WaveIn m_waveInput;
        public Int32 DiscretizationFrequency { get; private set; }
        public Int32 Сhannel { get; private set; }

        public MicrophoneReader( int _discretizationFrequency = 44100, int _channel = 1 )
        {
            DiscretizationFrequency = _discretizationFrequency;
            Сhannel = _channel;
        }

        public void StartRead(
            EventHandler<WaveInEventArgs> _dataAvailable
          , EventHandler<StoppedEventArgs> _recordingStopped )
        {
            try
            {
                m_waveInput = new WaveIn();
                m_waveInput.DeviceNumber = 0;
                m_waveInput.DataAvailable += _dataAvailable;
                m_waveInput.RecordingStopped += _recordingStopped;
                m_waveInput.WaveFormat = new WaveFormat(DiscretizationFrequency, Сhannel);
                m_waveInput.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Reset()
        {
            m_waveInput.Dispose();
            m_waveInput = null;
        }

        public void StopRecording()
        {
            m_waveInput.StopRecording();
        }

    }
}
