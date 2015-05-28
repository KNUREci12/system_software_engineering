using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioRecorderUnity
{
    public struct Complex
    {
        public Double real;
        public Double imag;

        public Complex(Double _real, Double _imag)
        {
            real = _real;
            imag = _imag;
        }

        public Single Magnitude
        {
            get
            {
                return Convert.ToSingle(Math.Sqrt(real * real + imag * imag));
            }
        }
    }
}
