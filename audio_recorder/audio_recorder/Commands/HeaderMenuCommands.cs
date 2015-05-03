using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Win32;

using audio_recorder.Spectrum_Analyzer;

namespace audio_recorder.Command
{
    class HeaderMenuCommands
    {
         private BaseCommand m_closeCommand;
         public ICommand CloseCommand
         {
             get
             {
                 if (m_closeCommand == null)
                 {
                     m_closeCommand = new BaseCommand(param => (param as Window).Close());
                 }
                 return m_closeCommand;
             }
        }

         private BaseCommand m_aboutCommand;
         public ICommand AboutCommand
         {
             get
             {
                 if (m_aboutCommand == null)
                 {
                     m_aboutCommand = new BaseCommand(param => (new AboutWindow()).Show());
                 }
                 return m_aboutCommand;
             }
         }


         private BaseCommand m_saveCurrentFile;
         public ICommand SaveCurrentFile
         {
             get
             {
                 if (m_saveCurrentFile == null)
                 {
                    m_saveCurrentFile = new BaseCommand(param =>
                    {
                        try
                        {
                            var fileDialog = new SaveFileDialog();
                            fileDialog.Filter = "mp3 files (*.mp3)|*.mp3|wav files (*.wav)|*.wav";
                            if( fileDialog.ShowDialog() == true )
                            {
                                // Somehowe save current to file
                            }
                        }
                        catch( Exception _ex )
                        {
                            System.Windows.MessageBox.Show( _ex.Message, _ex.GetType().ToString() );
                        }
                    });
                }
                return m_saveCurrentFile;
             }
         }

         private BaseCommand m_addExistingFile;
         public ICommand AddExistingFile
         {
             get
             {
                 if (m_addExistingFile == null)
                 {
                     m_addExistingFile = new BaseCommand(param => 
                     {
                         try
                         {
                            var fileDialog = new OpenFileDialog();
                            fileDialog.Filter = "mp3 files (*.mp3)|*.mp3|wav files (*.wav)|*.wav";
                            fileDialog.Multiselect = false;
                            if( fileDialog.ShowDialog() == true )
                            {
                                Mp3Reader reader = new Mp3Reader( fileDialog.FileName );
                                var complexSignal = FFT.fft(reader.getBuffer() );

                                var mainWindow = param as MainWindow;

                                Color[] color = {Color.Red, Color.Blue, Color.Silver, 
                                    Color.Yellow, Color.Green, Color.Khaki, Color.DarkViolet };

                                DrawManager drawManager = mainWindow.DrawManager;

                                drawManager.DrawCurve(complexSignal, color[ drawManager.CurveCount ]);
                            }
                         }
                         catch( Exception _ex )
                         {
                             System.Windows.MessageBox.Show( _ex.Message, _ex.GetType().ToString() );
                         }
                     });
                 }
                 return m_addExistingFile;
             }
         }


    }
}
