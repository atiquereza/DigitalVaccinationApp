using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalVaccination.Libs
{
    public class FileLocations
    {
        public static string FileUploadLocation { get { return fileUploadLocation; } }
        private static string fileUploadLocation = @"C:\temp\uploads\";
        public static string ZipFileLocation { get { return zipFileLocation; } }
        private static string zipFileLocation = @"D:\Test\";
        public static string FileDownloadLocation { get { return fileDownloadLocation; } }
        private static string fileDownloadLocation = @"C:\temp\OMCBDownloads\";
    }
}