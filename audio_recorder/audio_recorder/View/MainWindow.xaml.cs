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
		private WaveIn waveInput;
		private static int discretizationFrequency = 44100;
		private static int nChannel = 1;

		private ZedGraphControl zedPanel;

//DEBUG WaveFileWriter writer;
//DEBUG string outputFilename = "имя_файла.wav";

        public MainWindow()
        {
			InitializeComponent();
			zedPanel = new ZedGraphControl();
			var host = this.FindName("windowsFormsHost") as System.Windows.Forms.Integration.WindowsFormsHost;
			host.Child = zedPanel;
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
			if (!CheckAccess())
			{
				Dispatcher.Invoke(() => waveIn_DataAvailable(sender, e));
			}
			else
			{
				var complexSignal = FFT.fft( e.Buffer );
				Draw( complexSignal );

			}
        }

		void Draw( Complex[] _signal )
		{
			PointPairList signalPoints = new PointPairList();

			for (int freq = 0; freq < 22100; ++freq)
				signalPoints.Add(freq, FFT.getAmplitude(_signal, freq));

			GraphPane myPane;
			myPane = zedPanel.GraphPane;
			myPane.CurveList.Clear();

			LineItem myCurve = myPane.AddCurve("", signalPoints, System.Drawing.Color.Red, SymbolType.None);

			myPane.AxisChange();
			zedPanel.Invalidate();
		}

        private void waveInput_RecordingStopped(object sender, EventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new EventHandler(waveInput_RecordingStopped), sender, e);
            }
            else
            {
                waveInput.Dispose();
                waveInput = null;
//DEBUG         writer.Close();
//DEBUG         writer = null;
            }
        }

        private void Draw( WaveInEventArgs e )
        {
            //SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            //mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
			//
            //var count = e.Buffer.Count();
			//
            //for( int x = 0; x < 500; ++x )
            //{
            //    Ellipse myEllipse = new Ellipse();
			//
            //    myEllipse.Fill = mySolidColorBrush;
			//
            //    myEllipse.Width = 1;
            //    myEllipse.Height = 1;
			//
            //    var amplitude = e.Buffer[x];
            //    myEllipse.Margin = new Thickness( amplitude, 0, 0, 0);
			//
            //    m_graphicStackPannel.Children.Add(myEllipse);
            //}

            //for( int x = 0; x < e.Buffer.Count(); ++x )
            //{
            //    //EllipseGeometry elGeometry = new EllipseGeometry( new Point( x, e.Buffer[x] ), 0.1, 0.1 );
            //}

            //StackPanel myStackPanel = new StackPanel();
            //myStackPanel.Width = 400;
            //myStackPanel.Height = 100;
            //myStackPanel.Margin = new Thickness(0, -100, 0, 0);

            //SolidColorBrush backGroundBrush = new SolidColorBrush();
            //backGroundBrush.Color = Color.FromRgb(0, 0, 0);
            //myStackPanel.Background = backGroundBrush;


            //var cont = this.Content as Grid;
            //cont.Children.Add(myStackPanel);
            //cont.UpdateLayout();
            ////this.Content = myStackPanel;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Start Recording");
                stopButton.IsEnabled = true;
                waveInput = new WaveIn();
                waveInput.DeviceNumber = 0;
                waveInput.DataAvailable += waveIn_DataAvailable;
                waveInput.RecordingStopped += waveInput_RecordingStopped;
                waveInput.WaveFormat = new WaveFormat(discretizationFrequency, nChannel);

//DEBUG         writer = new WaveFileWriter(outputFilename, waveInput.WaveFormat);

                waveInput.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            waveInput.StopRecording();
            stopButton.IsEnabled = false;
            MessageBox.Show("StopRecording");
        }

    }
}
