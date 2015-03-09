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

using System.Windows.Shapes;
using NAudio.Wave;

namespace audio_recorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveIn waveInput;
        private StackPanel outPanel;
        private bool HasPanel = false;

//DEBUG WaveFileWriter writer;
//DEBUG string outputFilename = "имя_файла.wav";

        public MainWindow()
        {
            InitializeComponent();
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            Draw(e);
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => waveIn_DataAvailable(sender, e));
            }
//DEBUG     else
//DEBUG     {
//DEBUG         writer.WriteData(e.Buffer, 0, e.BytesRecorded);
//DEBUG     }
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
            if (!HasPanel)
            {
                outPanel = new StackPanel();
                outPanel.Width = 600;
                outPanel.Height = 200;
                outPanel.Margin = new Thickness(-200, -200, 0, 0);

                SolidColorBrush backGroundBrush = new SolidColorBrush();
                backGroundBrush.Color = Color.FromRgb(0, 0, 0);
                outPanel.Background = backGroundBrush;

                var MainWindowGrid = this.Content as Grid;
                MainWindowGrid.Children.Add(outPanel);
                HasPanel = true;
            }

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);

            var count = e.Buffer.Count();

            for( int x = 0; x < 500; ++x )
            {
                Ellipse myEllipse = new Ellipse();

                myEllipse.Fill = mySolidColorBrush;

                myEllipse.Width = 1;
                myEllipse.Height = 1;

                var amplitude = e.Buffer[x];
                myEllipse.Margin = new Thickness( amplitude, 0, 0, 0);

                outPanel.Children.Add(myEllipse);
            }

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
                waveInput = new WaveIn();
                waveInput.DeviceNumber = 0;
                waveInput.DataAvailable += waveIn_DataAvailable;
                waveInput.RecordingStopped += waveInput_RecordingStopped;
                waveInput.WaveFormat = new WaveFormat(8000, 1);

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
            MessageBox.Show("StopRecording");
        }
    }
}
