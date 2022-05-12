using System.Diagnostics;

namespace DBS
{
    class Program
    {
        public static bool find = false;
        public static void Main(String[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
    
            string mainDir = Directory.GetCurrentDirectory();
            string[] dirs = Directory.GetDirectories(mainDir);
            string[] files = Directory.GetFiles(mainDir);
            Program pr = new Program();
        
            for( int i = 0; i<files.Length; i++  )
            {
                if( files[i] == mainDir+@"\dbsFile")
                {
                    find = true;
                    pr.Find(files[i]);
                    break;
                }
            }

            if(!find)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File not a find(:<())");
            }
            Console.WriteLine("Work finish...");
            Console.ResetColor();
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
                string commands = lines[line].Split(" ")[0];
                if(commands == "cmd:")
                {
                    string arg = lines[line].Split(" ")[1];
                    Cmd(arg);
                    continue;
                }

                if(commands == "del:")
                {
                    string arg = lines[line].Split(" ")[1];
                    FileInfo fi = new FileInfo(arg);
                    if(fi.Exists)fi.Delete();
                    line++;
                    continue;
                }
                Console.WriteLine(lines[0]);
                Console.WriteLine(commands);
            }
        }
    }
}