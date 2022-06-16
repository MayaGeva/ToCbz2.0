using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace ToCbz2
{
    class CompileToCbz
    {
        public DirectoryInfo Path { get; set; }
        public DirectoryInfo Destination { get; set; }
        public int FileName { get; set; }
        public CompileToCbz(string path)
        {
            Path = new DirectoryInfo(path);
            Destination = new DirectoryInfo(path + "destination");
            FileName = 1;
            Compile();
        }
        public void Compile()
        {
            var folders = Path.EnumerateDirectories().OrderBy(d => d.CreationTime).ToArray();
            Array.Reverse(folders);
            Directory.CreateDirectory(Destination.FullName);
            for (int i = 0; i < folders.Length; i++)
            {
                Convert(folders[i]);
            }

            Zip(Directory.GetParent(Directory.GetParent(Path.FullName).FullName));
        }
        public void Zip(DirectoryInfo endPath)
        {
            string zipPath = endPath.FullName + "\\" + Path.Name + ".zip";

            ZipFile.CreateFromDirectory(Destination.FullName, zipPath);
            File.Move(zipPath, zipPath.Replace(".zip", ".cbz"));
            DeleteFiles(Directory.GetParent(Destination.FullName));
        }
        public void Convert(DirectoryInfo folder)
        {
            string addToNum = "";
            FileInfo[] files = folder.GetFiles();
            foreach (FileInfo file in files)
            {
                if (FileName > 1000)
                    addToNum = "";
                else if (FileName > 100)
                    addToNum = "0";
                else if (FileName > 10)
                    addToNum = "00";
                else
                    addToNum = "000";
                File.Copy(file.FullName, Destination.FullName + "\\" + addToNum + FileName + ".png");
                FileName++;
            }
            DeleteFiles(folder);
        }
        public void DeleteFiles(DirectoryInfo folder)
        {
            Directory.Delete(folder.FullName, true);
        }
    }
}
