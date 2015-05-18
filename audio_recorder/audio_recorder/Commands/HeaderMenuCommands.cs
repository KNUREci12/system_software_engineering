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

using NAudio.Wave;

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
                     m_closeCommand = new BaseCommand(param => (param as Window).Close());

                 return m_closeCommand;
             }
        }

         private BaseCommand m_aboutCommand;
         public ICommand AboutCommand
         {
             get
             {
                 if (m_aboutCommand == null)
                     m_aboutCommand = new BaseCommand(param => (new AboutWindow()).Show());

                 return m_aboutCommand;
             }
         }

         //private BaseCommand m_settingsCommand;
         //public ICommand SettingsCommand
         //{
         //    get
         //    {
         //        if (m_settingsCommand == null)
         //            m_settingsCommand = new BaseCommand(param => (new SettingsWindow()).Show());
         //
         //        return m_settingsCommand;
         //    }
         //}

         private BaseCommand m_addExistingFile;
         public ICommand AddExistingFile
         {
             get
             {
                 if (m_addExistingFile == null)
                 {
                    m_addExistingFile = new BaseCommand(
                        param =>
                        {
                            try
                            {
                                var fileDialog = new OpenFileDialog();

                                fileDialog.Filter = @"fft files (*.fft)|*.fft";
                                fileDialog.Multiselect = false;

                                if( fileDialog.ShowDialog() == true )
                                {
                                    if( fileDialog.FileName != null )
                                    {
                                        var fftSignal =
                                            SaveRestore.Restorer.RestoreFFt( fileDialog.FileName );

                                        var mainWindow = param as MainWindow;

                                        DrawManager drawManager = mainWindow.DrawManager;
                                        drawManager.DrawCurve( fftSignal );
                                    }
                                }
                            }
                            catch( Exception _exception )
                            {
                                System.Windows.MessageBox.Show( _exception.Message );
                            }
                        }
                    );
                 }
                 return m_addExistingFile;
             }
         }

         private BaseCommand m_saveFile;
         public ICommand SaveFile
         {
             get
             {
                 if (m_saveFile == null)
                 {
                     m_saveFile = new BaseCommand(
                         param =>
                         {
                             try
                             {
                                 var fileDialog = new SaveFileDialog();

                                 fileDialog.Filter = @"fft files (*.fft)|*.fft";

                                 if (fileDialog.ShowDialog() == true)
                                 {
                                     if (fileDialog.FileName != null)
                                     {
                                        var mainWindow = param as MainWindow;

                                        SaveRestore.Saver.Save(
                                                mainWindow.CurrentComlexSignal
                                            ,   mainWindow.CurrentBufferSize
                                            ,   fileDialog.FileName
                                        );
                                     }
                                 }
                             }
                             catch (Exception _exception)
                             {
                                 System.Windows.MessageBox.Show(_exception.Message);
                             }
                         }
                     );
                 }
                 return m_saveFile;
             }
         }

    }
}
