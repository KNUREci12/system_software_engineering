﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace AudioRecorderUnity
{
    public static class Restore
    {
        public static FFTTuple RestoreFFt(
                String _fileName
            )
        {
            using (
                var reader = new BinaryReader(
                    new FileStream(_fileName, FileMode.Open)
                )
            )
            {
                Int32 fftLength = reader.ReadInt32();
                Int32 bufferSize = reader.ReadInt32();

                if (fftLength < 0 || bufferSize < 0)
                    throw new Exception(@"uncorrect file restore.");

                Complex[] fft = new Complex[fftLength];

                for (int i = 0; i < fft.Length; ++i)
                    fft[i] = reader.ReadComplex();

                return new FFTTuple(bufferSize, fft);
            }
        }

        public static Complex ReadComplex(
            this BinaryReader _reader
        )
        {
            var real = _reader.ReadDouble();
            var imag = _reader.ReadDouble();

            return new Complex(real, imag);
        }
    }
}
