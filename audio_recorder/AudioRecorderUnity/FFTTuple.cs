using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioRecorderUnity
{
    public class FFTTuple
    {
        public Int32 BuffSize;
        public Complex[] FFT;

        public FFTTuple(Int32 _item1, Complex[] _item2)
        {
            BuffSize = _item1;
            FFT = _item2;
        }
    }
}
