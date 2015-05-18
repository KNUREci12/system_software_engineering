using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using System.Numerics;

namespace audio_recorder.Spectrum_Analyzer
{
    public static class ComlexConvertor
    {
        public static AForge.Math.Complex ToAForgeComplex(
            this Complex _value
        )
        {
            return
                new AForge.Math.Complex( _value.Real, _value.Imaginary );
        }

        public static Complex ToComplex(
            this AForge.Math.Complex _value
        )
        {
            return
                new Complex( _value.Re, _value.Im );
        }
    }
}
