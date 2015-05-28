using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioRecorderUnity
{
    public static class AmplitudeGetter
    {
        public static Single getAmplitude(FFTTuple _tuple, Int32 _freq)
        {
            int index = Convert.ToInt32(_freq * _tuple.FFT.Length / 44100);

            return _tuple.FFT[index].Magnitude / _tuple.BuffSize * 2;
        }
    }
}
