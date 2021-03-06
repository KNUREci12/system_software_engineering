﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using System.Drawing;

using ZedGraph;

namespace audio_recorder.Spectrum_Analyzer
{
    public class DrawManager
    {
        private GraphPane m_graphPanel;
        private ZedGraphControl m_zedPanel;

        private Color[] m_colors;

        private Int32 m_colorNumber;

        public DrawManager(ZedGraphControl _zedPanel)
        {
            m_zedPanel = _zedPanel;
            m_zedPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            m_graphPanel = new GraphPane();
            m_zedPanel.GraphPane = m_graphPanel;
            m_graphPanel.XAxis.Title.Text = "Frequency";
            m_graphPanel.YAxis.Title.Text = "Amplitude";

            m_colorNumber = 0;
            ConstrColors();

        }

        public void ClearCurveList()
        {
            m_graphPanel.CurveList.Clear();
        }

        public void DrawCurve(
                Complex[] _signal
            ,   Int32 _bufferSize
            ,   System.Drawing.Color _color
            ,   String _label = ""
            ,   SymbolType _type = SymbolType.None
        )
        {
            PointPairList signalPoints = new PointPairList();

            for (int freq = 0; freq < FFT.discretizationFrequency >> 1; ++freq)
                signalPoints.Add(freq, FFT.getAmplitude( _signal, freq, _bufferSize ) );

            m_graphPanel.AddCurve( _label, signalPoints, _color, _type );

            m_graphPanel.AxisChange();
            m_zedPanel.Invalidate();
        }

        public void DrawCurve(
                Tuple< Int32, Complex[] > _signal
            ,   System.Drawing.Color _color
            ,   String _label = ""
            ,   SymbolType _type = SymbolType.None
        )
        {
            DrawCurve( _signal.Item2, _signal.Item1, _color, _label, _type );
        }

        public void DrawCurve(
                Complex[] _signal
            ,   Int32 _bufferSize
        )
        {
            DrawCurve( _signal, _bufferSize, this.GetAvailableColor() );
        }

        public void DrawCurve(
            Tuple<Int32, Complex[]> _signal
        )
        {
            DrawCurve( _signal.Item2, _signal.Item1, this.GetAvailableColor() );
        }

        public void DrawCurve(
            CurveList _curvesList
        )
        {
            for( int i = 0; i < _curvesList.Count - 1; ++i )
            {
                PointPairList signalPoints = new PointPairList();

                for( int j = 0; j < _curvesList[ i ].Points.Count - 1; ++j )
                    signalPoints.Add( _curvesList[ i ][ j ] );

                m_graphPanel.AddCurve(@"", signalPoints, _curvesList[i].Color, SymbolType.None);

                m_graphPanel.AxisChange();
                m_zedPanel.Invalidate();
            }
        }

        public Color GetAvailableColor()
        {
            if ( m_colorNumber >= m_colors.Length )
                throw new Exception( @"max count of curves." );

            return m_colors[ m_colorNumber++ ];
        }

        public void Refresh()
        {
            this.ClearCurveList();
            m_zedPanel.Refresh();
            this.m_colorNumber = 0;
        }

        public ZedGraph.CurveList GetCurveList()
        {
            return this.m_graphPanel.CurveList;
        }

        private void ConstrColors()
        {
            m_colors =
                new Color[]
                {
                        Color.Violet
                    ,   Color.Blue
                    ,   Color.Yellow
                    ,   Color.Green
                };
        }

    }
}
