using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Common
{
    public class SolutionInfo
    {
        public EnvironmentInfo EnvironmentInfo { get; private set; }

        public char DirectorySeparatorChar { get; init; } = System.IO.Path.DirectorySeparatorChar;

        public string ExePath { get; private set; }
        public string ExeDirectory { get; private set; }
        public string ExeFileName { get; private set; }
        public string ExeFileNameWithoutExtension { get; private set; }
        public string NetVersion { get; private set; }


        public string CurrentDirectory { get; private set; }
        public string CommandLine { get; private set; }


        public string ProjectDirectory { get; private set; }
        public string ProjectName { get; private set; }


        public string SolutionDirectory { get; private set; }
        public string SolutionName { get; private set; }
        public string ParentDirectory { get; private set; }

        public List<string> AllProjectsDirectories { get; private set; }

        public SolutionInfo()
        {
            this.EnvironmentInfo = new EnvironmentInfo();

            this.ExePath = Environment.ProcessPath!;
            this.ExeDirectory = Path.GetDirectoryName(Environment.ProcessPath!)!;
            // other variants
            // this.ExeDirectory = System.AppContext.BaseDirectory;
            // this.ExeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // this.ExeDirectory = Directory.GetCurrentDirectory();
            // this.ExeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // this.ExeDirectory = Environment.CurrentDirectory;
            // this.ExeDirectory = Path.GetDirectoryName(System.Environment.ProcessPath!)!;

            this.ExeFileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.ExePath);
            this.ExeFileName = Path.GetFileName(this.ExePath);
            this.CurrentDirectory = Environment.CurrentDirectory;
            this.CommandLine = Environment.CommandLine;
            this.NetVersion = Environment.Version.ToString(2);

            this.ProjectDirectory = this.ExePath.Split("\\bin").FirstOrDefault();
            this.ProjectName = this.ProjectDirectory.Split("\\").Last();
            string[] slnxFiles = Directory.GetFiles(this.ProjectDirectory, "*.slnx");
            if (slnxFiles.Count() > 0)
            {
                this.SolutionDirectory = this.ProjectDirectory;
            }
            else
            {
                this.SolutionDirectory = Path.GetDirectoryName(this.ProjectDirectory);
                slnxFiles = Directory.GetFiles(this.SolutionDirectory, "*.slnx");
            }
            this.SolutionName = Path.GetFileNameWithoutExtension(slnxFiles.FirstOrDefault());
            this.ParentDirectory = Path.GetDirectoryName(this.SolutionDirectory);


            this.AllProjectsDirectories = Directory.GetDirectories(this.SolutionDirectory).ToList().Where(c => !c.EndsWith("\\.vs")).ToList();
        }
    }
}
