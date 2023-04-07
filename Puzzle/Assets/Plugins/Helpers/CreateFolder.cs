using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saklambac.NetFramework.Helpers
{
    public class CreateFolder
    {
        public static string Database_Folder_Path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\Puzzle_Data\\";
        public static void CreateForTextDb()
        {
            if (!Directory.Exists(Database_Folder_Path))
            {
                DirectoryInfo di = Directory.CreateDirectory(Database_Folder_Path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            /*
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "SaklambacDb/TextDb"))
            {
                DirectoryInfo di = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "SaklambacDb/TextDb");
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            */
        }

        public static void CreateForJsonDb()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "SaklambacDb/JsonDb"))
            {
                DirectoryInfo di = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "SaklambacDb/JsonDb");
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

    }
}
