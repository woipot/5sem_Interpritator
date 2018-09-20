using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Interpritator.Source.Interpritator;
using Interpritator.Source.UserInterfaceUtilities;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Orientation = System.Windows.Controls.Orientation;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Interpritator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentFilePath;
        private DirectoryInfo _projectRoot;

        public MainWindow()
        {
            _currentFilePath = null;
            InitializeComponent();
        }
        
        
        #region Properties

        public string CommandsInput
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
        public string ErrorOutput
        {
            get
            {
                var textRange = new TextRange(
                    ErrorRich.Document.ContentStart,
                    ErrorRich.Document.ContentEnd
                );

                return textRange.Text;
            }

            set
            {
                ErrorRich.Document.Blocks.Clear();
                ErrorRich.AppendText(value);
            }
        }

        #endregion


        #region Menu events

        #region File Menu

        private void OpenDirectory_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                //TrvStructure.

                _projectRoot = new DirectoryInfo(folderBrowser.SelectedPath);
                var rootItem = CreateTreeItem(_projectRoot, false);

                var directories = Directory.GetDirectories(folderBrowser.SelectedPath);
                var files = Directory.GetFiles(folderBrowser.SelectedPath);

                var directoriesInfo = directories.Select(i => new DirectoryInfo(i));
                var filesInfo = files.Select(i => new FileInfo(i));

                foreach (var item in directoriesInfo)
                {
                    rootItem.Items.Add(CreateTreeItem(item));
                }

                foreach (var item in filesInfo)
                {
                    rootItem.Items.Add(CreateTreeItem(item));
                }

                TrvStructure.Items.Add(rootItem);
            }

        }

        private void SaveAs_MenuClick(object sender, RoutedEventArgs e)
        {
            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                MainMenuFunc.SaveFile(_currentFilePath, CommandsInput);
            }
        }

        private void Save_MenuClick(object sender, RoutedEventArgs e)
        {
            if (_currentFilePath == null)
            {
                var isGoodDialogResult = SaveFileDialog();
                if (isGoodDialogResult)
                {
                    MainMenuFunc.SaveFile(_currentFilePath, CommandsInput);
                }
            }
            else
            {
                MainMenuFunc.SaveFile(_currentFilePath, CommandsInput);
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
            var patch = SaveBinFileDialog();
            if (patch != null)
            {
                try
                {
                    Compiler.SaveToBinFile(patch, CommandsInput);
                    var interpritatorResult = NumberCommandInterpritator.StartProgram(patch);

                    ResultOutput = interpritatorResult.Key;
                    ErrorOutput = interpritatorResult.Value;
                }
                catch (CompilerException ce)
                {
                    ErrorOutput = "#Error: "+ ce.Message + "-->" + ce.WrongCommand + "Command № ["+ce.CommandNumber+"]\n";
                }
              
            }
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


        #region treeView

        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            var item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is string))
            {

                item.Items.Clear();

                DirectoryInfo expandedDir = null;

                if (item.Tag is DirectoryInfo)
                    expandedDir = (item.Tag as DirectoryInfo);
                try
                {
                    if (expandedDir != null)
                    {
                        foreach (var subDir in expandedDir.GetDirectories())
                            item.Items.Add(CreateTreeItem(subDir));

                        foreach (var subDir in expandedDir.GetFiles())
                            item.Items.Add(CreateTreeItem(subDir));
                    }
                }
                catch
                {
                    // ignored
                }
            }
            ChangeIcon(item, true);
        }

        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        {
            var item = e.Source as TreeViewItem;
            ChangeIcon(item, false);
        }

        private static TreeViewItem CreateTreeItem(FileSystemInfo o, bool isLazy = true)
        {
            var isFolder = o is DirectoryInfo;

            var item = new TreeViewItem();
            item.Tag = o;

            if(isFolder && isLazy)
                item.Items.Add("Loading...");

            var stack = new StackPanel {Orientation = Orientation.Horizontal};

            item.Header = stack;

            var icon = new Image();
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Width = 16;
            icon.Height = 16;
            icon.Source = GetImageSource(isFolder, item.IsExpanded);
            stack.Children.Add(icon);
            
            //Add the HeaderText After Adding the icon
            var textBlock = new TextBlock();
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Text = o.Name;

            stack.Children.Add(textBlock);

            return item;
        }

        private void ChangeIcon(TreeViewItem item, bool isExpand)
        {
            var isFolder = item.Tag is DirectoryInfo;
            var panel = (StackPanel)item.Header;
            var icon= (Image)panel.Children[0];

            icon.Source = GetImageSource(isFolder, isExpand);
        }

        private static ImageSource GetImageSource(bool isFolder, bool itemIsExpanded)
        {
            if (isFolder && itemIsExpanded)
            {
                return new BitmapImage(new Uri(@"Resources/Images/OpenFolder_50px.png", UriKind.RelativeOrAbsolute));
            }
            else if(isFolder && !itemIsExpanded)
            {
                return new BitmapImage(new Uri(@"Resources/Images/Folder_50px.png", UriKind.RelativeOrAbsolute));
            }

            return new BitmapImage(new Uri(@"Resources/Images/File_52px.png", UriKind.RelativeOrAbsolute));
        }

        #endregion
    }
}
