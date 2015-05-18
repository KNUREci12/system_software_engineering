using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audio_recorder.Spectrum_Analyzer
{
    class MicroAnalyzer
    {

        #region public fields
        #endregion

        #region private fields

            private MicrophoneReader m_microphone;
            private DrawManager m_drawManager;

        #endregion

        #region constructors

            MicroAnalyzer(
                    MicrophoneReader _microphone
                ,   DrawManager _drawManager
            )
            {
                m_microphone = _microphone;
                m_drawManager = _drawManager;
            }

        #endregion

        #region public methods
        #endregion

        #region private methods
        #endregion

    }
}
