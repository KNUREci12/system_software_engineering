using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;
using NAudio.Mixer;

namespace audio_recorder.Spectrum_Analyzer
{
	class MicrophoneReader
		:	NAudio.Wave.WaveIn
	{
		private WaveIn m_waveIn;
		private int m_discretizationFrequency = 44100;
		private int m_channel = 1;

		MicrophoneReader( int _discretizationFrequency = 44100, int _channel = 1 )
		{
			m_discretizationFrequency = _discretizationFrequency;
			m_channel = _channel;
		}
	}
}
