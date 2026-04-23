
using System.Collections;


namespace Expeditious.Common
{
    public class EnvironmentInfo
    {
        public string PathSystemDirectory { get; private set; }
        public string UserName { get; private set; }
        public string UserDomainName { get; private set; }
        public int ProcessId { get; private set; }
        public int CurrentManagedThreadId { get; private set; }

        public string MachineName { get; private set; }


        public IDictionary EnvironmentVariables { get; private set; }
        public List<string> CommandLineArgs { get; private set; }
        public string PathMyDocuments { get; private set; }
        public string PathApplicationData { get; private set; }
        public string PathDesktopDirectory { get; private set; }
        public string PathProgramFiles { get; private set; }
        public string PathProgramFilesX86 { get; private set; }
        public string PathDesktopLogicalDirectory { get; private set; }
        public string PathTemp { get; private set; }
        public string PathMyComputer { get; private set; }
        public string PathRecent { get; private set; }
        public string PathWindows { get; private set; }
        public long WorkingSet { get; private set; }
        public string NewLine { get; private set; }

        public string CurrentDirectory { get; private set; }
        // public Environment.ProcessCpuUsage CpuUsage { get; private set; }

        public List<DriveInfo> DriveInfos { get; private set; } = DriveInfo.GetDrives().ToList();


        public EnvironmentInfo()
        {
            this.PathSystemDirectory = Environment.SystemDirectory;
            this.UserName = Environment.UserName;
            this.UserDomainName = Environment.UserDomainName;
            this.CurrentManagedThreadId = Environment.CurrentManagedThreadId;
            this.ProcessId = Environment.ProcessId;
            this.MachineName = Environment.MachineName;

            this.EnvironmentVariables = Environment.GetEnvironmentVariables();

            this.CommandLineArgs = Environment.GetCommandLineArgs().ToList();
            this.NewLine = Environment.NewLine;

            this.PathMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.PathApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.PathDesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            this.PathProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            this.PathProgramFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            this.PathDesktopLogicalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.PathMyComputer = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            this.PathRecent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            this.PathWindows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            this.PathTemp = Path.GetTempPath().Substring(0, Path.GetTempPath().Length - 1);
            this.WorkingSet = Environment.WorkingSet;

            this.CurrentDirectory = Environment.CurrentDirectory; // or Directory.GetCurrentDirectory()
            // this.CpuUsage = Environment.CpuUsage;
        }
    }
}
