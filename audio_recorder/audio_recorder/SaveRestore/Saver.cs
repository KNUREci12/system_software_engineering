using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Numerics;

using ZedGraph;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using AudioRecorderUnity.FileType;

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
            if( _signal == null )
            {
                #if DEBUG
                    System.Windows.MessageBox.Show( @"try to save empty spectrum.");
                #endif

                return;
            }

            using(
                var fileStream = new FileStream( _fileName, FileMode.Create )
            )
            {
                var serializator = new BinaryFormatter();

                serializator.Serialize( fileStream, fileType.Complex );

                serializator.Serialize(fileStream, _signal.Length );

                serializator.Serialize(fileStream, _bufferSize );

                foreach( var it in _signal )
                {
                    serializator.Serialize(fileStream, it.Real);
                    serializator.Serialize(fileStream, it.Imaginary);
                }

            }
        }

        public static void Save(
                ZedGraph.CurveList _list
            ,   String _fileName = @"file.fftAmpl"
        )
        {
            if( _list.Count == 0 )
            {
                #if DEBUG
                    System.Windows.MessageBox.Show(@"try to save empty graph.");
                #endif

                return;
            }

            using (
                var fileStream = new FileStream(_fileName, FileMode.Create)
            )
            {
                var serializator = new BinaryFormatter();

                serializator.Serialize(fileStream, _list );

            }
        }

    }
}
