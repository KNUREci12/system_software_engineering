using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Numerics;

using NAudio.Wave;

using ZedGraph;

using audio_recorder.Spectrum_Analyzer;

namespace audio_recorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private ZedGraphControl m_zedPanel;

        private DrawManager m_drawManager;
        private MicrophoneReader m_microphoneReader;

        public MainWindow()
        {
			InitializeComponent();

			m_zedPanel = new ZedGraphControl();
			var host = this.FindName("windowsFormsHost") as System.Windows.Forms.Integration.WindowsFormsHost;
			host.Child = m_zedPanel;
            m_microphoneReader = new MicrophoneReader();
            m_drawManager = new DrawManager( m_zedPanel );
        }

        public static readonly DependencyProperty DrawManagerProperty =
            DependencyProperty.Register("DrawManager", typeof(DrawManager), typeof(MainWindow), new UIPropertyMetadata(null));

        public DrawManager DrawManager
        {
            get { return m_drawManager; }
        }

        void DataAvailable(object sender, WaveInEventArgs e)
        {
			if( CheckAccess() )
			{
                var complexSignal = FFT.fft(e.Buffer);
                m_drawManager.ClearCurveList();
                m_drawManager.DrawCurve(complexSignal, System.Drawing.Color.Red);
			}
			else
			{
                Dispatcher.Invoke(() => DataAvailable(sender, e));
			}
        }

        private void RecordingStopped(object sender, EventArgs e)
        {
            if( CheckAccess() )
                m_microphoneReader.Reset();
            else
                Dispatcher.BeginInvoke(new EventHandler(RecordingStopped), sender, e);
        }


        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = true;
            m_microphoneReader.StartRead(DataAvailable, RecordingStopped);
            MessageBox.Show("Start Recording");
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = false;
            m_microphoneReader.StopRecording();
            MessageBox.Show("StopRecording");
        }

    }
}
