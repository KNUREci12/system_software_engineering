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

        public Complex[] CurrentComlexSignal { get; private set; }

        public Int32 CurrentBufferSize { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            m_zedPanel = new ZedGraphControl();
            var host = this.FindName("windowsFormsHost") as System.Windows.Forms.Integration.WindowsFormsHost;
            host.Child = m_zedPanel;
            MicrophoneReader = new MicrophoneReader();
            DrawManager = new DrawManager(m_zedPanel);
        }

        public DrawManager DrawManager { get; private set; }

        public MicrophoneReader MicrophoneReader { get; private set; }

        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            if( CheckAccess() )
            {
                CurrentComlexSignal = FFT.fft(e.Buffer);

                CurrentBufferSize = e.BytesRecorded;

                DrawManager.Refresh();

                DrawManager.DrawCurve(
                        CurrentComlexSignal
                    ,   e.BytesRecorded
                    ,   System.Drawing.Color.Red
                );
            }
            else
            {
                Dispatcher.Invoke(() => DataAvailable(sender, e));
            }
        }

        private void RecordingStopped(object sender, EventArgs e)
        {
            if( CheckAccess() )
                MicrophoneReader.Reset();
            else
                Dispatcher.BeginInvoke(new EventHandler(RecordingStopped), sender, e);
        }


        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if( !stopButton.IsEnabled )
            {
                stopButton.IsEnabled = true;
                MicrophoneReader.StartRead(DataAvailable, RecordingStopped);
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = false;
            MicrophoneReader.StopRecording();
        }
    }
}
