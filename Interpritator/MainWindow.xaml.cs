using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
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
        

        #region Menu events

        #region File Menu

        private void SaveAs_MenuClick(object sender, RoutedEventArgs e)
        {
            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                MainMenuFunc.SaveFile(_currentFilePath, MainInputText);
            }
        }

        private void Save_MenuClick(object sender, RoutedEventArgs e)
        {
            if (_currentFilePath == null)
            {
                var isGoodDialogResult = SaveFileDialog();
                if (isGoodDialogResult)
                {
                    MainMenuFunc.SaveFile(_currentFilePath, MainInputText);
                }
            }
            else
            {
                MainMenuFunc.SaveFile(_currentFilePath, MainInputText);
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
            //TODO: delete this (debug)
            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                Compiler.SaveToBinFile(_currentFilePath, MainInputText);
            }

            Compiler.DecodeBinFile(_currentFilePath, OutputRich);
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

        #endregion

       
    }
}
