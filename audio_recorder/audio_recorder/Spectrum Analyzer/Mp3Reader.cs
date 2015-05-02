using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace audio_recorder.Spectrum_Analyzer
{
	class Mp3Reader
	{
		public string m_fileName { private set; get; }

		private WaveStream m_waveStream;

		public Mp3Reader( string _fileName )
		{
			m_fileName = _fileName;
			m_waveStream = null;
		}

		public Byte[] getBuffer( int m_buffSize = 8192 )
		{
			Byte[] buffer = new Byte[ m_buffSize ];

			if( m_waveStream == null )
				m_waveStream = CreateInputStream( m_fileName );

			m_waveStream.CurrentTime = new TimeSpan( 0, 0, 0 );
			m_waveStream.Read( buffer, 0, buffer.Length );

			return buffer;
		}

		private WaveStream CreateInputStream( string _fileName )
		{
			WaveStream fileStream;
			if ( _fileName.EndsWith( ".mp3" ) )
			{
				WaveStream mp3Reader = new Mp3FileReader( _fileName );
				WaveStream pStream = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream( mp3Reader );
				fileStream = new NAudio.Wave.BlockAlignReductionStream(pStream);
			}
			else if ( _fileName.EndsWith( ".wav" ) )
			{
				var pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader( _fileName ));
				fileStream = new NAudio.Wave.BlockAlignReductionStream(pcm);
			}
			else
			{
				throw new InvalidOperationException( "Unsupported extension of file" );
			}
			return fileStream;
		}

	}
}
