using System;
using System.Windows;
using System.Windows.Controls;
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
                MainMenuFunc.SaveFile(_currentFilePath, _mainRichText);
            }
        }

        private void Save_MenuClick(object sender, RoutedEventArgs e)
        {
            if (_currentFilePath == null)
            {
                var isGoodDialogResult = SaveFileDialog();
                if (isGoodDialogResult)
                {
                    MainMenuFunc.SaveFile(_currentFilePath, _mainRichText);
                }
            }
            else
            {
                MainMenuFunc.SaveFile(_currentFilePath, _mainRichText);
            }
        }

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

        #endregion

        private void Exit_MenuClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
