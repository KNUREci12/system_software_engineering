using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using AForge;

namespace audio_recorder.Spectrum_Analyzer
{
	static class FFT
	{
		public const int defaultSamplingFrequency = 8192;
		public static int discretizationFrequency = 44100;

		public static double getAmplitude(
				Complex[] _fft
			,	int _freq
		)
		{
			int index = Convert.ToInt32( _freq * _fft.Length / discretizationFrequency );
			return _fft[index].Magnitude *2 / _fft.Length;
		}

		public static Complex[] fft(Byte[] _buffer)
		{
			return fft( convertSignal( _buffer ) );
		}

		public static Complex[] fft(Complex[] _signal)
		{
			AForge.Math.Complex[] afSignal = new AForge.Math.Complex[ _signal.Length ];

			Complex[] afterFFt = new Complex[ _signal.Length ];

			for( int i = 0; i < _signal.Length; ++i )
				afSignal[ i ] = new AForge.Math.Complex( _signal[ i ].Real, _signal[ i ].Imaginary );

			AForge.Math.FourierTransform.FFT( afSignal, AForge.Math.FourierTransform.Direction.Forward );

			for( int i = 0; i < afterFFt.Length; ++i )
				afterFFt[ i ] = new Complex( afSignal[ i ].Re, afSignal[ i ].Im );

			return afterFFt;
		}


		private static Complex[] convertSignal(
				Byte[] _buffer
			,	int _samplingFrequency = defaultSamplingFrequency // add check 2^n
		)
		{
			Complex[] complexSignal = new Complex[ _samplingFrequency / 2 ];

			for( int i = 0; i < complexSignal.Length; ++i )
			{
				var leftPart = _buffer[ i * 2 + 1 ] << 8;
				var rightPart = _buffer[i * 2];
				short sample = ( short )( ( _buffer[ i + 1 ] << 8 ) | _buffer[ i + 0 ] );
				complexSignal[ i ] = sample / 32768f;
			}

			return complexSignal;
		}

	}
}
