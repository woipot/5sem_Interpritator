using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Interpritator.Source.Interpritator;
using Interpritator.Source.UserInterfaceUtilities;
using Microsoft.Win32;

namespace Interpritator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentFilePath;

        public MainWindow()
        {
            _currentFilePath = null;

            InitializeComponent();

        }
        
        
        #region Properties

        public string CommandInput
        {
            get
            {
                var textRange = new TextRange(
                    MainInputText.Document.ContentStart,
                    MainInputText.Document.ContentEnd
                );

                return textRange.Text;
            }

            set
            {
                MainInputText.Document.Blocks.Clear();
                MainInputText.AppendText(value);
            }
        }

        public string ResultOutput
        {
            get
            {
                var textRange = new TextRange(
                    OutputRich.Document.ContentStart,
                    OutputRich.Document.ContentEnd
                );

                return textRange.Text;
            }

            set
            {
                OutputRich.Document.Blocks.Clear();
                OutputRich.AppendText(value);
            }
        }

        #endregion


        #region Menu events

        #region File Menu

        private void SaveAs_MenuClick(object sender, RoutedEventArgs e)
        {
            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                MainMenuFunc.SaveFile(_currentFilePath, CommandInput);
            }
        }

        private void Save_MenuClick(object sender, RoutedEventArgs e)
        {
            if (_currentFilePath == null)
            {
                var isGoodDialogResult = SaveFileDialog();
                if (isGoodDialogResult)
                {
                    MainMenuFunc.SaveFile(_currentFilePath, CommandInput);
                }
            }
            else
            {
                MainMenuFunc.SaveFile(_currentFilePath, CommandInput);
            }
        }


        private void Exit_MenuClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion


        #region Run Menu

        private void Start_MenuClick(object sender, RoutedEventArgs e)
        {
            //var patch = SaveBinFileDialog();
            //if (patch != null)
            //{
            //    Compiler.SaveToBinFile(patch, MainInputText);
            //    NumberCommandInterpritator.StartProgram(patch, OutputRich);
            //}

            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                Compiler.SaveToBinFile(_currentFilePath, CommandInput);
            }

            ResultOutput = Compiler.DecodeBinFile(_currentFilePath);
        }

        #endregion


        #endregion


        #region Dialogs

        private bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                DefaultExt = "inw",
                Filter = "Simple interpritator text file|*.inw"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _currentFilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        private bool SaveFileDialog()
        {

            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "inw",
                Filter = "Simple interpritator text file|*.inw"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                
                _currentFilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        private string OpenBinFileDialog()
        {
            string newFilePatch = null;
            var openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                DefaultExt = "inwoi",
                Filter = "Command interpritator file|*.inwoi"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                newFilePatch = openFileDialog.FileName;
            }
            return newFilePatch;
        }

        private string SaveBinFileDialog()
        {
            string newFilePatch = null;
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "inwoi",
                Filter = "Command interpritator file|*.inwoi"
            };

            if (saveFileDialog.ShowDialog() == true)
            {

                newFilePatch = saveFileDialog.FileName;
            }
            return newFilePatch;
        }

        #endregion


    }
}
