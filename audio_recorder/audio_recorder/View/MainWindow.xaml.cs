﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Numerics;

using System.Drawing;

using NAudio.Wave;

using ZedGraph;

using audio_recorder.Spectrum_Analyzer;

using System.IO;

using System.Diagnostics;

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

        public Color MainCurve { get; set; }

        private NotesWindow m_noteWindow;

        public MainWindow()
        {
            InitializeComponent();

            m_zedPanel = new ZedGraphControl();
            var host = this.FindName("windowsFormsHost") as System.Windows.Forms.Integration.WindowsFormsHost;
            host.Child = m_zedPanel;
            DrawManager = new DrawManager(m_zedPanel);

            m_noteWindow = null;

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
                    ,   CurrentBufferSize
                    ,   MainCurve
                );

                if( m_noteWindow != null )
                    m_noteWindow.NoteAnalyze(
                            CurrentComlexSignal
                        ,   CurrentBufferSize
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

            try
            {
                MicrophoneReader = new MicrophoneReader();
                MicrophoneReader.StartRead(DataAvailable, RecordingStopped);

                stopButton.IsEnabled = true;
                startButton.IsEnabled = false;
            }
            catch{}

        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = false;
            startButton.IsEnabled = true;
            MicrophoneReader.StopRecording();
        }

        private void window_Closed(object sender, EventArgs e)
        {
            MicrophoneReader.StopRecording();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            DrawManager.Refresh();
        }

        private void showNotesButton_Click(object sender, RoutedEventArgs e)
        {
            if( m_noteWindow != null  )
            {
                m_noteWindow.Close();
                m_noteWindow = null;
                return;
            }

            m_noteWindow = new NotesWindow( this );
            m_noteWindow.Show();
        }

        private void show3D_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = @"3d.multifft";

                SaveRestore.Saver.Save(
                        this.DrawManager.GetCurveList()
                    ,   path
                );

                Process.Start( @"3drun.exe").WaitForExit();

            }
            catch( System.IO.FileNotFoundException )
            {
                MessageBox.Show( @"can`t find 3drun.exe" );
            }
            catch( Exception _exception )
            {
                #if DEBUG
                    MessageBox.Show( _exception.Message );
                #endif
                #if RELEASE
                    MessageBox.Show( @"Something went wrong. Please send report." );
                #endif
            }
        }

    }
}
