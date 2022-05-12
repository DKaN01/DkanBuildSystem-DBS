using System.Diagnostics;

namespace DBS
{
    class Program
    {
        public static void Main(String[] args){

            string mainDir = Directory.GetCurrentDirectory();
            string[] dirs = Directory.GetDirectories(mainDir);
            string[] files = Directory.GetFiles(mainDir);
            Program pr = new Program();
    
            for( int i = 0; i<files.Length; i++  )
            {
                if( files[i] == mainDir+@"\dbsFile")
                {
                    pr.Find(files[i]);
                    break;
                }
            }
        }
        void Cmd(string arg)
        {

            var proc = new ProcessStartInfo()
            {
                UseShellExecute = true,
                WorkingDirectory = @"C:\Windows\System32",
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = "/c " + arg,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(proc);
        }
        public void Find(string pathToFile)
        {
            string text = File.ReadAllText(pathToFile);
            string[] lines = text.Split("\n");
            for( int line = 0; line < lines.Length; line++ ){
                if(lines[line] == "copy:")
                {
                    string args = lines[line+1];
                    string path = args.Split(" ")[0];
                    string dir = args.Split(" ")[1];
                    FileInfo fi = new FileInfo(path);
                    if(fi.Exists)fi.CopyTo(dir, true);
                    line++;
                    continue;
                }

                if(lines[line] == "move:")
                {
                    string args = lines[line+1];
                    string path = args.Split(" ")[0];
                    string dir = args.Split(" ")[1];
                    FileInfo fi = new FileInfo(path);
                    if(fi.Exists)fi.MoveTo(dir, true);
                    line++;
                    continue;
                }

                if(lines[line] == "cmd:")
                {
                    string args = lines[line+1];
                    line++;
                    continue;
                }

                if(lines[line] == "del:")
                {
                    string arg = lines[line+1];
                    FileInfo fi = new FileInfo(arg);
                    if(fi.Exists)fi.Delete();
                    line++;
                    continue;
                }
            }
        }
    }
}