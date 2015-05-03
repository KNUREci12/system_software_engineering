using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NAudio.Wave;

using ZedGraph;

using System.Numerics;

using audio_recorder.Spectrum_Analyzer;

namespace audio_recorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

		private ZedGraphControl zedPanel;

		private Mp3Reader fileReader;

		//private MicrophoneReader mcReader;

        public MainWindow()
        {
			InitializeComponent();
			zedPanel = new ZedGraphControl();
			var host = this.FindName("windowsFormsHost") as System.Windows.Forms.Integration.WindowsFormsHost;
			host.Child = zedPanel;
        }

		void Draw( Complex[] _signal )
		{
			PointPairList signalPoints = new PointPairList();

			for (int freq = 0; freq < 22100; ++freq)
				signalPoints.Add(freq, FFT.getAmplitude(_signal, freq));
				//signalPoints.Add(freq, WindowSignal.Hann( FFT.getAmplitude(_signal, freq), 22100 ));

			GraphPane myPane;
			myPane = zedPanel.GraphPane;
			myPane.CurveList.Clear();

			LineItem myCurve = myPane.AddCurve("", signalPoints, System.Drawing.Color.Red, SymbolType.None);

			myPane.AxisChange();
			zedPanel.Invalidate();
		}

		void Draw(NAudio.Dsp.Complex[] _signal)
		{
			PointPairList signalPoints = new PointPairList();

			for (int freq = 0; freq < 22100; ++freq)
				//signalPoints.Add(freq, FFT.getAmplitude(_signal, freq));
				signalPoints.Add(freq, FFT.getAmplitude(_signal, freq) * NAudio.Dsp.FastFourierTransform.HammingWindow(FFT.getIndex(_signal, freq), _signal.Length));
			//signalPoints.Add(freq, WindowSignal.Hann( FFT.getAmplitude(_signal, freq), 22100 ));

			GraphPane myPane;
			myPane = zedPanel.GraphPane;
			myPane.CurveList.Clear();

			LineItem myCurve = myPane.AddCurve("", signalPoints, System.Drawing.Color.Red, SymbolType.None);

			myPane.AxisChange();
			zedPanel.Invalidate();
		}

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
				stopButton.IsEnabled = true;

				fileReader = new Mp3Reader("signal.wav");

				Byte[] buff = fileReader.getBuffer();

				var comp = FFT.convertSignal(buff);

				NAudio.Dsp.Complex[] naudioComplex = new NAudio.Dsp.Complex[comp.Length];

				for( int i = 0; i < comp.Length; ++i )
				{
					naudioComplex[ i ].X = (float)comp[i].Magnitude;
					naudioComplex[ i ].Y = (float)comp[i].Imaginary;
				}

				//NAudio.Dsp.FastFourierTransform.FFT(false, 12, naudioComplex);

				var afterFFt = FFT.fft(buff);

				//Draw(naudioComplex);
				Draw(afterFFt);

				//mcReader = new MicrophoneReader();
				//mcReader.setDraw((Complex[] _complex) => { Draw(_complex); });
				//mcReader.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
			//mcReader.Stop();
        }

    }
}
