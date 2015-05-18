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
            ,   Int32 _freq
            ,   Int32 _bufferSize
        )
        {
            int index = Convert.ToInt32( _freq * _fft.Length / discretizationFrequency );

            return _fft[index].Magnitude / _bufferSize * 2;
        }

        public static Complex[] fft(Byte[] _buffer)
        {
            return fft( convertSignal( _buffer ) );
        }

        public static Complex[] fft(Complex[] _signal)
        {
            AForge.Math.Complex[] afSignal = new AForge.Math.Complex[ _signal.Length ];

            for( int i = 0; i < _signal.Length; ++i )
                afSignal[ i ] = _signal[ i ].ToAForgeComplex();

            AForge.Math.FourierTransform.FFT(
                    afSignal
                ,   AForge.Math.FourierTransform.Direction.Forward
            );

            Complex[] afterFFt = new Complex[_signal.Length];

            for( int i = 0; i < afterFFt.Length; ++i )
                afterFFt[ i ] = afSignal[ i ].ToComplex();

            return afterFFt;
        }


        private static Complex[] convertSignal(
                Byte[] _buffer
            ,   int _samplingFrequency = defaultSamplingFrequency
        )
        {
            if( !isPowerOfTwo( _samplingFrequency ) )
                new ArgumentException( @"sampling frequency must be power of two" );

            Complex[] complexSignal = new Complex[ _samplingFrequency / 2 ];

            for( int i = 0; i < complexSignal.Length; ++i )
            {
                var leftPart = _buffer[ i * 2 + 1 ] << 8;

                short sample = (short)( ( leftPart ) | _buffer[i]);

                complexSignal[ i ] = sample / 32768f;
            }

            return complexSignal;
        }

        private static bool isPowerOfTwo(int n)
        {
            return ( n & (n - 1) ) == 0;
        }
    }
}
