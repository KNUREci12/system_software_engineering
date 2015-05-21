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
    public static class Saver
    {
        public static void Save(
                Complex[] _signal
            ,   Int32 _bufferSize
            ,   String _fileName = @"file.fft"
        )
        {
            using(
                var stream = new FileStream( _fileName, FileMode.Create )
            )
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize( stream, _signal );

                formatter.Serialize( stream, _bufferSize );

            }
        }

        public static void Write(
                this BinaryWriter _writer
            ,   Complex _value
        )
        {
            _writer.Write( _value.Real );
            _writer.Write( _value.Imaginary );
        }
    }
}
