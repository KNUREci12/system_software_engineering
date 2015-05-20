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
using System.Windows.Shapes;

using System.Numerics;

using audio_recorder.Spectrum_Analyzer;

namespace audio_recorder
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {

        private Note_Analyzer.Notes m_selectNote;

        public NotesWindow( MainWindow _main )
        {
            InitializeComponent();
            m_selectNote = new Note_Analyzer.Notes();
        }

        private void DrawBaseFreq()
        {
            if( OctaveBox.SelectedIndex == -1 || NoteBox.SelectedIndex == -1 )
                return;

            StanadardFreq.Content = m_selectNote.ToString();

        }

        public void NoteAnalyze(
                Complex[] _signal
            ,   Int32 _bufferSize
        )
        {
            if (OctaveBox.SelectedIndex == -1 || NoteBox.SelectedIndex == -1)
                return;

            var noteFreq = m_selectNote.GetFreq();

            FirstCF.Content =
                LocMaximum(
                        noteFreq
                    ,   _signal
                    ,   _bufferSize
                );

            FifthCF.Content =
                LocMaximum(
                        noteFreq * 5
                    ,   _signal
                    ,   _bufferSize
                ) / 5;

            TenthCF.Content =
                LocMaximum(
                        noteFreq * 10
                    ,   _signal
                    ,   _bufferSize
                ) / 10;

            FifteenthCF.Content =
                LocMaximum(
                        noteFreq * 15
                    ,   _signal
                    ,   _bufferSize
                ) / 15;
        }

        private Double LocMaximum(
                Double _baseFreq
            ,   Complex[] _signal
            ,   Int32 _bufferSize
        )
        {
            Int32 maxValueIndex = 0;
            Double maxValue = 0;

            Int32 index = Convert.ToInt32( _baseFreq - 10 );
            Int32 end = Convert.ToInt32(_baseFreq + 10 );

            if( index > 22000 || end > 22000 )
                return Double.NaN;

            for (; index < end; ++index)
            {
                var amplitude =
                    FFT.getAmplitude( _signal, index, _bufferSize );

                if( maxValue < amplitude )
                {
                    maxValue = amplitude;
                    maxValueIndex = index;
                }
            }

            return maxValueIndex;
        }

        private void octave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_selectNote.OctaveValue =
                Note_Analyzer.Notes.OctaveFromString(
                    (e.AddedItems[0] as System.Windows.Controls.ComboBoxItem).Content.ToString()
                );
            DrawBaseFreq();
        }

        private void note_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_selectNote.NoteValue =
                Note_Analyzer.Notes.NoteFromString(
                    (e.AddedItems[0] as System.Windows.Controls.ComboBoxItem).Content.ToString()
                );
            DrawBaseFreq();
        }
    }
}
