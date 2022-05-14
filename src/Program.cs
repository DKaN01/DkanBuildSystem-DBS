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
            // string text = File.ReadAllText(pathToFile);
            IEnumerable<string> lines = File.ReadLines(pathToFile);
            foreach(string line in lines){
                string commands = line.Split(" ")[0];
                switch(commands)
                {
                    case "cmd:":
                        cmd(line.Split(" ")[1]);
                        break;
                    case "del:":
                        del(line.Split(" ")[1]);
                        break;    
                    case "move:":
                        move(line.Split(" ")[1], line.Split(" ")[2]);
                        break;
                    case "copy:":
                        copy(line.Split(" ")[1], line.Split(" ")[2]);
                        break;
                    default:
                        continue; 
                }
            }
        }

        private void copy(string file, string dir)
        {
            string path = Path.Combine(dir,file);
            try
            {
                File.Copy(file, path, true);
            }
            catch (System.Exception)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("ERROR COPY:\n {0}\n{1}",file,path);
                Console.ResetColor();
            }
        }
        private void move(string file, string dir)
        {
            string path = Path.Combine(dir,file);
            if(File.Exists(file) && !File.Exists(dir))
            {
                try
                {
                    File.Move(file, path);
                }
                catch (System.Exception)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("ERROR MOVE:\n {0}\n{1}",file,path);
                    Console.ResetColor();
                }
            }
        }
        private void cmd(string line)
        {
            Cmd(line);
        }
        private void del(string line)
        {
            FileInfo fi = new FileInfo(line);
            if(fi.Exists)fi.Delete();
        }
    }
}