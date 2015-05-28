using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Numerics;

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
                var writer = new BinaryWriter(
                    new FileStream( _fileName, FileMode.Create )
                )
            )
            {
                writer.Write( _signal.Length );

                writer.Write( _bufferSize );

                foreach( var it in _signal )
                    writer.Write( it );

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
