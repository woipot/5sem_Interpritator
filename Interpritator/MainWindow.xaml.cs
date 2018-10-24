using System.Windows;

namespace Interpritator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
        }



        //#region File Menu

        //private void OpenDirectory_Click(object sender, RoutedEventArgs e)
        //{
        //    var folderBrowser = new FolderBrowserDialog();

        //    folderBrowser.ShowDialog();

        //    if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
        //    {
        //        //TrvStructure.

        //        var _projectRoot = new DirectoryInfo(folderBrowser.SelectedPath);
        //        var rootItem = CreateTreeItem(_projectRoot, false);

        //        var directories = Directory.GetDirectories(folderBrowser.SelectedPath);
        //        var files = Directory.GetFiles(folderBrowser.SelectedPath);

        //        var directoriesInfo = directories.Select(i => new DirectoryInfo(i));
        //        var filesInfo = files.Select(i => new FileInfo(i));

        //        foreach (var item in directoriesInfo)
        //        {
        //            rootItem.Items.Add(CreateTreeItem(item));
        //        }

        //        foreach (var item in filesInfo)
        //        {
        //            rootItem.Items.Add(CreateTreeItem(item));
        //        }

        //        TrvStructure.Items.Add(rootItem);
        //    }

        //}


        //#endregion


       





        //#region treeView

        //public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        //{
        //    var item = e.Source as TreeViewItem;
        //    if ((item.Items.Count == 1) && (item.Items[0] is string))
        //    {

        //        item.Items.Clear();

        //        DirectoryInfo expandedDir = null;

        //        if (item.Tag is DirectoryInfo)
        //            expandedDir = (item.Tag as DirectoryInfo);
        //        try
        //        {
        //            if (expandedDir != null)
        //            {
        //                foreach (var subDir in expandedDir.GetDirectories())
        //                    item.Items.Add(CreateTreeItem(subDir));

        //                foreach (var subDir in expandedDir.GetFiles())
        //                    item.Items.Add(CreateTreeItem(subDir));
        //            }
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
        //    ChangeIcon(item, true);
        //}

        //private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        //{
        //    var item = e.Source as TreeViewItem;
        //    ChangeIcon(item, false);
        //}

        //private static TreeViewItem CreateTreeItem(FileSystemInfo o, bool isLazy = true)
        //{
        //    var isFolder = o is DirectoryInfo;

        //    var item = new TreeViewItem();
        //    item.Tag = o;


        //    if(isFolder && isLazy)
        //        item.Items.Add("Loading...");

        //    var stack = new StackPanel {Orientation = Orientation.Horizontal};

        //    item.Header = stack;

        //    var icon = new Image();
        //    icon.VerticalAlignment = VerticalAlignment.Center;
        //    icon.Width = 16;
        //    icon.Height = 16;
        //    icon.Source = GetImageSource(ItemTypeGeter.GetType(item));
        //    stack.Children.Add(icon);

        //    //Add the HeaderText After Adding the icon

        //    var textBlock = new TextBlock();
        //    textBlock.VerticalAlignment = VerticalAlignment.Center;
        //    textBlock.Text = o.Name;

        //    stack.Children.Add(textBlock);

        //    return item;
        //}

        //private void ChangeIcon(TreeViewItem item, bool isExpand)
        //{
        //    var isFolder = item.Tag is DirectoryInfo;
        //    var panel = (StackPanel)item.Header;
        //    var icon= (Image)panel.Children[0];
            
        //    icon.Source = GetImageSource(ItemTypeGeter.GetType(item, isExpand));
        //}

        //private static ImageSource GetImageSource(TreeViewItemTypes type)
        //{
        //    switch (type)
        //    {
        //        case TreeViewItemTypes.Folder:
        //            return new BitmapImage(new Uri(@"Resources/Images/Folder.png", UriKind.RelativeOrAbsolute));

        //        case TreeViewItemTypes.OpenFolder:
        //            return new BitmapImage(new Uri(@"Resources/Images/OpenFolder.png", UriKind.RelativeOrAbsolute));

        //        case TreeViewItemTypes.InwFile:
        //            return new BitmapImage(new Uri(@"Resources/Images/Inw_File.png", UriKind.RelativeOrAbsolute));

        //        case TreeViewItemTypes.InwoiFile:
        //            return new BitmapImage(new Uri(@"Resources/Images/Inwoi_File.png", UriKind.RelativeOrAbsolute));

        //        default:
        //            return new BitmapImage(new Uri(@"Resources/Images/File.png", UriKind.RelativeOrAbsolute));
        //    }
        //}

        //private void TreeViewItem_Select(object sender, RoutedEventArgs e)
        //{
        //    if (sender is TreeViewItem item)
        //    {
        //        var tag = item.Tag;
        //        var isFile = tag is FileInfo;
        //        if (isFile)
        //        {
        //            var fileInfo = (FileInfo) tag;
        //            var extension = fileInfo.Extension;
        //            if (extension == "txt" || extension == "inw")
        //            {
        //                var sr = new StreamReader(fileInfo.FullName);

        //                CommandsInput = sr.ReadToEnd();

        //                sr.Dispose();
        //            }
        //        }

        //    }
        //}

        //#endregion


    }
}