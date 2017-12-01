using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    static class Service
    {
        public static List<string> ListDirectories(string path)
        {
            List<string> dirs = new List<string>()
            {
                ".",
                ".."
            };

            DirectoryInfo di = new DirectoryInfo(path);

            foreach (DirectoryInfo d in di.GetDirectories())
            {
                dirs.Add(d.Name);
            }

            return dirs;
        }

        public static string ChangeDirectory(string current, string dir)
        {
            dir = dir.Trim('"');

            DirectoryInfo di = new DirectoryInfo(current + dir);
            if(di.Exists)
                return current + dir + "/";

            return current;
        }

        public static void MakeDirectory(string dir)
        {
            Directory.CreateDirectory(dir);
        }

        public static string RemoveDirectory(string dir)
        {
            try
            {
                Directory.Delete(dir);
            }
            catch
            {
                return "Directory is not empty!";
            }

            return "";
        }
    }
}
