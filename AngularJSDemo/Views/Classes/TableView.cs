using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Views.Classes
{
    public class TableView
    {
        public static string GetTableView(List<Dictionary<string, string>> tableData,string tableTitle)
        {
            string data = string.Empty;
            if(tableData.Count == 0)
            {
                return data;
            }

            data += "<fieldset><legend>" + tableTitle + "</legend><table style=\"margin-left: auto; margin-right: auto;border-collapse:collapse; width: 100%;\">";
            data += "<tr style=\"border: 1px solid darkgray; font-size: .95em;\">";
            foreach (KeyValuePair<string,string> keyValuePair in tableData[0])
            {
                data += "<td style=\"border: 1px solid darkgray; font-size: .95em;\"><strong>" + keyValuePair.Key + "</strong></td>";
            }
            data += "</tr>";


            foreach (Dictionary<string, string> keyValuePair in tableData)
            {
                data += "<tr style=\"border: 1px solid darkgray; font-size: .95em;\">";
                foreach (KeyValuePair<string, string> valuePair in keyValuePair)
                {
                    data += "<td style=\"border: 1px solid darkgray; font-size: .95em;\">" + valuePair.Value + "</td>";
                }
                data += "</tr>";
            }
            
            data += "</table></fieldset>";
            return data;
        }
    }
}