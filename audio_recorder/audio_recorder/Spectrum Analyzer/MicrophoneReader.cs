using System;
using System.Threading.Tasks;

using System.Windows;

using System.Numerics;

using NAudio.Wave;

namespace audio_recorder.Spectrum_Analyzer
{
    class MicrophoneReader
    {
        private WaveIn m_waveInput;
        private int m_discretizationFrequency = 44100;
        private int m_channel = 1;

        public MicrophoneReader(int _discretizationFrequency = 44100, int _channel = 1)
        {
            m_discretizationFrequency = _discretizationFrequency;
            m_channel = _channel;
        }

        public void StartRead(EventHandler<WaveInEventArgs> _dataAvailable, EventHandler<StoppedEventArgs> _recordingStopped)
        {
            try
            {
                m_waveInput = new WaveIn();
                m_waveInput.DeviceNumber = 0;
                m_waveInput.DataAvailable += _dataAvailable;
                m_waveInput.RecordingStopped += _recordingStopped;
                m_waveInput.WaveFormat = new WaveFormat(m_discretizationFrequency, m_channel);
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
