using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Numerics;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using AudioRecorderUnity.FileType;

namespace audio_recorder.SaveRestore
{
    public static class Restorer
    {

        public static Tuple< Int32, Complex[] > RestoreFFt(
            String _fileName
        )
        {
            using (
                var fileStream = new FileStream(_fileName, FileMode.Open)
            )
            {
                var deserializator = new BinaryFormatter();

                var fileType =
                    ( fileType ) Enum.ToObject(
                        typeof( fileType), deserializator.Deserialize( fileStream )
                    );

                if( fileType != fileType.Complex )
                    return null;

                var length = ( Int32 ) deserializator.Deserialize( fileStream );

                var bufferSize = (Int32) deserializator.Deserialize( fileStream );

                Complex[] fft = new Complex[ length ];

                for( int i = 0; i < length; ++i )
                {
                    var real = ( Double ) deserializator.Deserialize( fileStream );
                    var imag = ( Double ) deserializator.Deserialize(fileStream);

                    fft[ i ] = new Complex( real, imag );
                }

                return new Tuple<int, Complex[]>(bufferSize, fft);

            }
        }

        public static ZedGraph.CurveList RestoreCurveList(
            String _fileName
        )
        {
            using (
                var fileStream = new FileStream(_fileName, FileMode.Open)
            )
            {
                return
                    new BinaryFormatter().Deserialize(fileStream) as ZedGraph.CurveList;
            }
        }

    }
}
