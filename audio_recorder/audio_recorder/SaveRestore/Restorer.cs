using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Numerics;

using System.Runtime.Serialization.Formatters.Binary;

namespace audio_recorder.SaveRestore
{
    public static class Restorer
    {

        public static Tuple< Int32, Complex[] > RestoreFFt(
            String _fileName
        )
        {
            using(
                var stream = new FileStream( _fileName, FileMode.Open )
            )
            {
                var formatter = new BinaryFormatter();

                var fft = formatter.Deserialize( stream ) as Complex[];

                var bufferSize = (Int32)formatter.Deserialize( stream );

                return new Tuple<int,Complex[]>( bufferSize, fft );
            }

        }

        public static Complex ReadComplex(
            this BinaryReader _reader
        )
        {
                var real = _reader.ReadDouble();
                var imag = _reader.ReadDouble();

                return new Complex( real, imag );
        }

    }
}
