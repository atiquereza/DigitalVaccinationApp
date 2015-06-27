using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.IO;
using OfficeOpenXml.Style;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;

namespace AngularJSDemo.Libs
{
    public class IOFileOperation
    {
       

        public static List<Dictionary<string, string>> LoadXMLData(string fileName, string tagName, List<string> attributes)
        {
            List<Dictionary<string,string>> data = new List<Dictionary<string, string>>();
          
            XmlReader xmlReader = XmlReader.Create(fileName);
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == tagName))
                {
                    Dictionary<string,string> aData = new Dictionary<string, string>();
                    foreach (string attribute in attributes)
                    {
                        if(xmlReader.HasAttributes)
                        {
                            string attributeValue = xmlReader.GetAttribute(attribute);
                            aData.Add(attribute,attributeValue);
                        }
                    }
                    data.Add(aData);
                }
            }

            return data;
        }

        public static List<string> LoadXMLData(string tagName, string fileName)
        {
           
            List<string> data = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNode;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            xmlDoc.Load(fs);
            //xmlDoc.ge
            xmlNode = xmlDoc.GetElementsByTagName(tagName);
            foreach (XmlNode node in xmlNode)
            {
                data.Add(node.InnerText.Trim());
            }
            fs.Close();


            return data;
        }

        public static List<string> ReadXMLData(string tagName, string fileName)
        {
            List<string> data = new List<string>();

           

            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    if (reader.Name.ToLower().Equals(tagName.ToLower()))
                    {
                        data.Add(reader.ReadInnerXml());
                    }
                }

                reader.Close();
            }


            return data;

        }

        public static List<string> LoadTextFileData(string fileName)
        {
            List<string> data = new List<string>();

            

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = sr.ReadLine();
                while (line != null && line.Length > 0)
                {
                    data.Add(line.Trim());
                    line = sr.ReadLine();
                }

                sr.Close();
            }


            return data;
        }

        public static List<string> GetFileDirectories(string location)
        {
            List<string> directories = new List<string>();
            try
            {


                if (Directory.Exists(location))
                {
                    directories = Directory.GetFiles(location, "*.*", SearchOption.AllDirectories).ToList();
                }


            }
            catch
            {
            }

            return directories;
        }
        
       

        

        public static DataSet ReadExcelFile(string file, string sheetName)
        {
            List<string> sheetNames = GetCompleteSheetNames(file);
    
            OleDbConnection con =
                new OleDbConnection(
                    String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file +
                                  ";Extended Properties=\"Excel 12.0 Xml;HDR=YES; IMEX=1;\""));
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetName + "$]", con);
            DataSet aDataObjectSet = new DataSet();
            da.Fill(aDataObjectSet);
            return aDataObjectSet;
        }


        public static List<string> GetCompleteSheetNames(string filePath)
        {
            List<string> listSheet = new List<string>();
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filePath;
            if (Path.GetExtension(filePath).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (Path.GetExtension(filePath).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);
            List<string> temp = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString()))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                
                
                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        if (((drSheet["TABLE_NAME"].ToString().Trim().LastIndexOf("$") + 1) == drSheet["TABLE_NAME"].ToString().Trim().Length)
                            || (drSheet["TABLE_NAME"].ToString().Trim().LastIndexOf("$'") + 2) == drSheet["TABLE_NAME"].ToString().Trim().Length)
                        listSheet.Add(drSheet["TABLE_NAME"].ToString());

                        else
                        {
                            temp.Add(drSheet["TABLE_NAME"].ToString());
                        }
                    }
                }
            }
            return listSheet;
        }


       
        

        public static string GetCurrentExecutingLocation()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static string ReadTextFile(string file)
        {
            string data = string.Empty;
            using (StreamReader sr = new StreamReader(file))
            {
                data = sr.ReadToEnd();
                sr.Close();
            }

            return data;
        }
        public static string CreateExelFile(List<Dictionary<string, string>> data, string path, string sheetName)
        {
            string completeFileName = string.Empty;

            int prefix = 0;
            string outFile = "ZZZ.xlsx";
            string tempFileName = outFile;
            while (System.IO.File.Exists(Path.Combine(path, tempFileName)))
            {
                prefix++;
                tempFileName = prefix.ToString() + outFile;
            }



           
            completeFileName = Path.Combine(path, tempFileName);

            FileInfo newFile = new FileInfo(completeFileName);
            // If any file exists in this directory having name 'Sample1.xlsx', then delete it
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(completeFileName);
            }



            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // Openning first Worksheet of the template file i.e. 'Sample1.xlsx'
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);
                int rowIndex = 1;
                int colIndex = 1;
                foreach (KeyValuePair<string, string> keyValuePair in data[0])
                {
                    worksheet.Cells[rowIndex, colIndex].Value = keyValuePair.Key.ToUpper();
                    
                    colIndex ++;
                }


                foreach (Dictionary<string, string> dictionary in data)
                {
                    rowIndex++;
                    colIndex = 1;
                    foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                    {
                        int intData;
                        bool result = int.TryParse(keyValuePair.Value, out intData);
                        if (result)
                        {
                            worksheet.Cells[rowIndex, colIndex].Value = intData;
                        }
                        else
                        {
                            worksheet.Cells[rowIndex, colIndex].Value = keyValuePair.Value;
                        }
                        colIndex ++;
                    }
                }


                if (data.Count > 0)
                {
                    using (var range = worksheet.Cells[1, 1, 1, data[0].Count])
                    {
                        //Setting bold font
                        range.AutoFilter = true;
                        range.Style.Font.Bold = true;

                        //Setting fill type solid
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //Setting background color dark blue
                        range.Style.Fill.BackgroundColor.SetColor(Color.BurlyWood);
                        //Setting font color
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    using (var range = worksheet.Cells[1, 1, data.Count+1, data[0].Count])
                    {
                        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                }
                package.Save();
            }
            return completeFileName;
        }

        public static string CreateExelFile(Dictionary<string, List<Dictionary<string, string>>> inputData, string path)
        {
            string completeFileName = string.Empty;

            int prefix = 0;
            string outFile = "ZZZ.xlsx";
            string tempFileName = outFile;
            while (System.IO.File.Exists(Path.Combine(path, tempFileName)))
            {
                prefix++;
                tempFileName = prefix.ToString() + outFile;
            }
            completeFileName = Path.Combine(path, tempFileName);

            FileInfo newFile = new FileInfo(completeFileName);
            // If any file exists in this directory having name 'Sample1.xlsx', then delete it
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(completeFileName);
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {

                foreach (KeyValuePair<string, List<Dictionary<string, string>>> pair in inputData)
                {

                    if (pair.Value.Count > 0)
                    {
                        var fieldDictionary = pair.Value[0];

                        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                        data = pair.Value;
                        string sheetName = pair.Key;


                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);
                        int rowIndex = 1;
                        int colIndex = 1;



                        foreach (KeyValuePair<string, string> columnHeader in fieldDictionary)
                        {
                            worksheet.Cells[rowIndex, colIndex].Value = columnHeader.Key;
                            colIndex++;
                        }


                        foreach (Dictionary<string, string> dictionary in data)
                        {

                            rowIndex++;
                            colIndex = 1;
                            string rowColor = "";
                            foreach (KeyValuePair<string, string> keyValuePair in fieldDictionary)
                            {
                                if (dictionary[keyValuePair.Key] == "false")
                                {
                                    rowColor = "red";
                                }
                                int intData;
                                bool result = int.TryParse(dictionary[keyValuePair.Key], out intData);
                                if (result)
                                {
                                    worksheet.Cells[rowIndex, colIndex].Value = intData;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, colIndex].Value = dictionary[keyValuePair.Key];
                                    if (rowColor == "red")
                                    {
                                        var range = worksheet.Cells[rowIndex, 1, rowIndex, 30];
                                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        range.Style.Fill.BackgroundColor.SetColor(Color.Red);
                                    }

                                }
                                colIndex++;
                            }
                            rowColor = "";


                        }


                        if (data.Count > 0)
                        {
                            using (var range = worksheet.Cells[1, 1, 1, data[0].Count])
                            {
                                //Setting bold font
                                range.AutoFilter = true;
                                range.Style.Font.Bold = true;

                                //Setting fill type solid
                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //Setting background color dark blue
                                range.Style.Fill.BackgroundColor.SetColor(Color.BurlyWood);
                                //Setting font color
                                range.Style.Font.Color.SetColor(Color.White);
                            }
                            using (var range = worksheet.Cells[1, 1, data.Count + 1, data[0].Count])
                            {
                                range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            }
                        }
                    }

                }
                package.Save();
            }
            return completeFileName;
        }

    }
}
