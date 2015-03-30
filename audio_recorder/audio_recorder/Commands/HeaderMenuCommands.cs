using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

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
                     m_saveCurrentFile = new BaseCommand(param => (
                        new SaveFileDialog()).ShowDialog());
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
                     m_addExistingFile = new BaseCommand(param => (
                        new OpenFileDialog()).ShowDialog());
                 }
                 return m_addExistingFile;
             }
         }


    }
}
