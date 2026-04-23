
using Microsoft.Win32;


namespace Expeditious.WpfWinUI
{
    public static  class IoDialogUI
    {
        static public String GetFileNewPath(String filepath)
        {
            String? folder = System.IO.Path.GetDirectoryName(filepath);
            String? filename = $"{System.IO.Path.GetFileNameWithoutExtension(filepath)}_{DateTime.Now.Ticks}.txt";
            return System.IO.Path.Combine(folder, filename);
        }


        public const String FILTER_TXT = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        public const String FILTER_MP4 = "Video mp4 files (*.mp4)|*.mp4|All files (*.*)|*.*";

        static public readonly String FOLDER_PATH_MYDOCUMENTS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static public readonly String FOLDER_PATH_DOWNLOADS = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        static public readonly String FOLDER_PATH_DESKTOP = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
        static public readonly String FOLDER_PATH_TEMP_WINDOWS = System.IO.Path.GetTempPath();
        static public readonly String FOLDER_PATH_TEMP_OWN = @"C:\TEMP\";
        static public readonly String FOLDER_PATH_C = @"C:\";


        static public List<String> GetFilesFromOpenDialog(String filter = FILTER_TXT, String initialDirectory = null)
        {
            initialDirectory = initialDirectory == null ? FOLDER_PATH_C : initialDirectory;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = filter;
            openFileDialog.InitialDirectory = initialDirectory;

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileNames.ToList();

            return null;
        }


        static public String GetFileFromOpenDialog(String filter = FILTER_TXT, String initialDirectory = null)
        {
            initialDirectory = initialDirectory == null ? FOLDER_PATH_C : initialDirectory;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = filter;
            openFileDialog.InitialDirectory = initialDirectory;

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;

            return null;
        }


        static public void OpenFileInNotepad(String filePath)
        {
            System.Diagnostics.Process.Start(filePath);
            //System.Diagnostics.Process.Start(@"c:\test");
            //System.Diagnostics.Process.Start("explorer.exe", @"C:\Users");
        }
    }
}
