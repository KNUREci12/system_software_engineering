﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using ZedGraph;

namespace audio_recorder.Spectrum_Analyzer
{
    public class DrawManager
    {
        private GraphPane m_graphPanel;
        private ZedGraphControl m_zedPanel;

        public DrawManager(ZedGraphControl _zedPanel)
        {
            m_zedPanel = _zedPanel;
            m_zedPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            m_graphPanel = new GraphPane();
            m_zedPanel.GraphPane = m_graphPanel;
            m_graphPanel.XAxis.Title.Text = "Frequency";
            m_graphPanel.YAxis.Title.Text = "Amplitude";
        }

        public void ClearCurveList()
        {
            m_graphPanel.CurveList.Clear();
        }

        public void DrawCurve(   
                Complex[] _signal
            ,   System.Drawing.Color _color
            ,   String _label = ""
            ,   SymbolType _type = SymbolType.None)
        {
            PointPairList signalPoints = new PointPairList();   

            // NOTE: to turn on mirror display use FFT.discretizationFrequency without dividing
            for (int freq = 0; freq < FFT.discretizationFrequency >> 1; ++freq)
                signalPoints.Add(freq, FFT.getAmplitude(_signal, freq));

            LineItem myCurve = m_graphPanel.AddCurve(_label, signalPoints, _color, _type);

            m_graphPanel.AxisChange();
            m_zedPanel.Invalidate();
        }

        public int CurveCount
        {
            get
            {
                return m_graphPanel.CurveList.Count;
            }
        }
    }
}
