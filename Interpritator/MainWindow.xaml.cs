using System;
using System.Windows;
using System.Windows.Controls;
using Interpritator.Source.Interpritator;
using Microsoft.Win32;

namespace Interpritator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string _currentFilePath;
        private RichTextBox _mainRichText;

        public MainWindow()
        {
            _currentFilePath = null;

            InitializeComponent();

            _mainRichText = (RichTextBox) FindName("MainText");
        }


        #region Menu events

        private void SaveAs_MenuClick(object sender, RoutedEventArgs e)
        {
            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                Compiler.SaveToBinFile(_currentFilePath, _mainRichText);
            }
        }

        private void Save_MenuClick(object sender, RoutedEventArgs e)
        {
            if (_currentFilePath == null)
            {
                var isGoodDialogResult = SaveFileDialog();
                if (isGoodDialogResult)
                {
                    Compiler.SaveToBinFile(_currentFilePath, _mainRichText);
                }
            }
            else
            {
                Compiler.SaveToBinFile(_currentFilePath, _mainRichText);
            }
        }

        #endregion


        #region Dialogs

        private bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.DefaultExt = "inwoi";
            openFileDialog.Filter = "Number Interpritator commands|*.inwoi";

            if (openFileDialog.ShowDialog() == true)
            {
                _currentFilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        private bool SaveFileDialog()
        {

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "inwoi";
            saveFileDialog.Filter = "Number Interpritator commands|*.inwoi";

            if (saveFileDialog.ShowDialog() == true)
            {
                
                _currentFilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        #endregion
    }
}
