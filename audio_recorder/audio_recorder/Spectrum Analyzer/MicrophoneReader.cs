using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Threading;

using NAudio.Wave;
using NAudio.Mixer;

using System.Numerics;
using ZedGraph;

namespace audio_recorder.Spectrum_Analyzer
{
	class MicrophoneReader
	{
		private WaveIn m_waveIn;
		private Int32 m_discretizationFrequency = 44100;
		private Int32 m_channel = 1;

		private Action< Complex[] > DrawDelegate;

		public MicrophoneReader(
				Int32 _discretizationFrequency = 44100
			,	Int32 _channel = 1
			,	Int32 _deviceNumber = 0
		)
		{
			m_discretizationFrequency = _discretizationFrequency;
			m_channel = _channel;
		}
		
		public void Start()
		{
			m_waveIn = new WaveIn();
			m_waveIn.DeviceNumber = 0;
			m_waveIn.DataAvailable += m_DataAvailable;
			m_waveIn.RecordingStopped += m_RecordingStopped;
			m_waveIn.WaveFormat = new WaveFormat(m_discretizationFrequency, m_channel);
			m_waveIn.StartRecording();
		}

		public void Stop()
		{
			m_waveIn.StopRecording();
		}

		private void m_DataAvailable(object sender, WaveInEventArgs e)
		{
			var currentDispatcher = System.Windows.Application.Current.Dispatcher;
			if ( !currentDispatcher.CheckAccess() )
			{
				currentDispatcher.Invoke(() => m_DataAvailable(sender, e));
			}
			else
			{
				if (DrawDelegate == null)
					return;

				var complexSignal = FFT.fft(e.Buffer);
				//Draw(complexSignal);
				DrawDelegate.Invoke(complexSignal);
			}
		}

		private void m_RecordingStopped(object sender, EventArgs e)
		{
			var currentDispatcher = System.Windows.Application.Current.Dispatcher;
			if (!currentDispatcher.CheckAccess() )
			{
				currentDispatcher.Invoke(() => m_RecordingStopped(sender, e));
			}
			else
			{
				m_waveIn.Dispose();
				m_waveIn = null;
			}
		}

		public void setDraw( Action< Complex[] > _action )
		{
			DrawDelegate = _action;
		}
	}
}
