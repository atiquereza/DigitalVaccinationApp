using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Web;
using Microsoft.Exchange.WebServices.Data;
using Shell32;
using Folder = Shell32.Folder;

namespace AngularJSDemo.Libs
{
    public class ZipFiles
    {
        [STAThread]
        public static string ZipFolder(string sourceFolder, string destinationFile)
        {

            byte[] emptyzip = new byte[] { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }

            FileStream fs = File.Create(destinationFile);
            fs.Write(emptyzip, 0, emptyzip.Length);
            fs.Flush();
            
            fs.Close();
            fs = null;
            File.SetAttributes(destinationFile, FileAttributes.Normal);
            Shell objShell = new Shell();
            Folder destination = objShell.NameSpace(destinationFile);
            Folder source = objShell.NameSpace(sourceFolder);
            FolderItems items = source.Items();
            destination.CopyHere(items, 20);
            System.Threading.Thread.Sleep(1000);
            
            return destinationFile;
        }
        public static string ExtractZipFile(string sourceFile, string destinationFolder)
        { 
            Shell objShell = new Shell();
            Folder destination = objShell.NameSpace(destinationFolder);
            Folder source = objShell.NameSpace(sourceFile);

            foreach (var file in source.Items())
            {
                destination.CopyHere(file, 4 | 16);
            }
            return destinationFolder;
        }

        public static string CreateFolderToZipFiles()
        {
            string zipFolder = FileLocations.FileUploadLocation + "Plan";

            int index = 0;
            while (Directory.Exists(zipFolder))
            {

                index++;
                zipFolder = FileLocations.FileUploadLocation + "Plan" + index.ToString();
            }


            Directory.CreateDirectory(zipFolder);
            return zipFolder;
        }

    }
}