using System.IO;
using System.Windows.Controls;

namespace Interpritator.Source.UserInterfaceUtilities
{
    public enum TreeViewItemTypes
    {
        Folder, 
        OpenFolder,
        File,
        InwFile,
        InwoiFile
    }

    public static class ItemTypeGeter
    {
        public static TreeViewItemTypes GetType(TreeViewItem item, bool isExpanded = false)
        {
            var isFolder = item.Tag is DirectoryInfo;
            if (isFolder && isExpanded)
            {
                return TreeViewItemTypes.OpenFolder;
            }

            if (isFolder)
            {
                return TreeViewItemTypes.Folder;
            }

            if (!isFolder)
            {
                var isInwFile = (item.Tag as FileInfo).Extension == ".inw";
                if (isInwFile)
                {
                    return TreeViewItemTypes.InwFile;
                }

                var isInwoiFile = (item.Tag as FileInfo).Extension == ".inwoi";
                if(isInwoiFile)
                {
                    return TreeViewItemTypes.InwoiFile;
                }
            }

            return TreeViewItemTypes.File;
        }
    }
}
