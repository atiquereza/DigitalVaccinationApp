using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DigitalVaccination.Libs
{
    public class DBGateway
    {
        public MySqlConnection SqlConnectionObj { get; set; }
        public MySqlCommand SqlCommandobj { get; set; }
        string conString = string.Empty;
        public DBGateway()
        {
            try
            {
                conString = ConfigurationManager.ConnectionStrings["DigitalVaccinationConnectionString"].ConnectionString;
                SqlConnectionObj = new MySqlConnection(conString);
                SqlCommandobj = SqlConnectionObj.CreateCommand();
            }
            catch (Exception exception)
            {
                conString = "Server=localhost;Port=3306;Database=tikaappdb;Uid=root;password='root';Convert Zero Datetime = true";
                SqlConnectionObj = new MySqlConnection(conString);
                SqlCommandobj = SqlConnectionObj.CreateCommand();
            }
        }

        public DBGateway(string connnectionStringName)
        {
            try
            {
                this.conString = ConfigurationManager.ConnectionStrings[connnectionStringName].ConnectionString;
                SqlConnectionObj = new MySqlConnection(conString);
                SqlCommandobj = SqlConnectionObj.CreateCommand();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            

            
        }

        public string CreateDatabase(string queryString)
        {
            MySqlConnection SqlConnectionObjToCreate = new MySqlConnection();
            try
            {
                string conStringToCreate = "Server=localhost;Port=3306;Uid=root;password=\"\";Convert Zero Datetime = true";
                SqlConnectionObjToCreate = new MySqlConnection(conStringToCreate);

                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObjToCreate);

                SqlConnectionObjToCreate.Open();
                command.CommandText = queryString;
                command.ExecuteNonQuery();
                return "Database created.";
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Create Database. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObjToCreate != null && SqlConnectionObjToCreate.State == ConnectionState.Open)
                {
                    SqlConnectionObjToCreate.Close();
                }
            }


        }


        public string DropCreateTable(string queryString)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObj);

                SqlConnectionObj.Open();
                command.CommandText = queryString;
                command.ExecuteNonQuery();
                return "Operation Successfull..";
            }
            catch (Exception ex)
            {
                throw new Exception("Operation failed. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }


        }

        //public string Backup(string backUPFile)
        //{
        //    try
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            using (MySqlBackup mb = new MySqlBackup(cmd))
        //            {

        //                cmd.Connection = SqlConnectionObj;
        //                SqlConnectionObj.Open();
        //                mb.ExportToFile(backUPFile);
        //                SqlConnectionObj.Close();
        //            }
        //        }

        //        return "Database Backup Successfull.";
        //    }
        //    catch (Exception exception)
        //    {
        //        return "Unable To Backup. Exception: " + exception.Message;
        //    }
        //}
        public string BulkDataInsert(string tableName, string fieldTerminator, string lineTerminator, string fileName, int numberOfLineToSkip)
        {
            try
            {

                string msg = string.Empty;
                MySqlBulkLoader bulkLoader = new MySqlBulkLoader(SqlConnectionObj);
                bulkLoader.TableName = tableName;
                bulkLoader.FieldTerminator = fieldTerminator;
                bulkLoader.LineTerminator = lineTerminator;
                bulkLoader.FileName = fileName;
                bulkLoader.NumberOfLinesToSkip = numberOfLineToSkip;
                int rows = bulkLoader.Load();

                return "Data Success fully Inserted";
            }
            catch (Exception exception)
            {
                return "Exception from Bulk Insert. Message: " + exception.Message;
            }
        }

        public string BulkDataInsert(string tableName, char fieldQuotationCharacter, string fieldTerminator, string lineTerminator, string fileName, int numberOfLineToSkip)
        {
            string msg = string.Empty;
            MySqlBulkLoader bulkLoader = new MySqlBulkLoader(SqlConnectionObj);
            bulkLoader.Timeout = 1000 * 60 * 5;
            bulkLoader.TableName = tableName;
            bulkLoader.FieldQuotationOptional = false;
            bulkLoader.FieldQuotationCharacter = fieldQuotationCharacter;
            bulkLoader.FieldTerminator = fieldTerminator;
            bulkLoader.LineTerminator = lineTerminator;
            bulkLoader.FileName = fileName;
            bulkLoader.NumberOfLinesToSkip = numberOfLineToSkip;

            int rows = bulkLoader.Load();

            return msg;
        }


        public DataSet Select(string queryString)
        {
            try
            {
                //SqlConnectionObj.Open();
                DataSet aDataSet = new DataSet();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(queryString, SqlConnectionObj);
                dataAdapter.SelectCommand.CommandTimeout = 300;
                dataAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Select. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }

        }
        public DataSet Select(string queryString, Hashtable parameterTable)
        {
            try
            {

                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObj);
                foreach (DictionaryEntry entry in parameterTable)
                {
                    command.Parameters.Add("@" + entry.Key, MySqlDbType.Text, 100).Value = entry.Value;

                }
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                dataAdapter.SelectCommand = command;
                dataAdapter.SelectCommand.CommandTimeout = 300;
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);


                return dataSet;

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Select. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }

        }

        public string Insert(string queryString, Hashtable parameterTable)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObj);
                foreach (DictionaryEntry entry in parameterTable)
                {
                    command.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                }

                SqlConnectionObj.Open();
                command.CommandText = queryString;
                command.ExecuteNonQuery();
                return "Data Successfully Inserted.";
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Insert. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }


        }

        public string Insert(string queryString, Hashtable parameterTable, out int insertId)
        {
            insertId = 0;
            try
            {
                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObj);
                foreach (DictionaryEntry entry in parameterTable)
                {
                    command.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                }

                SqlConnectionObj.Open();
                command.CommandText = queryString;
                insertId = Convert.ToInt32(command.ExecuteScalar());
                return "Data Successfully Inserted.";
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Insert. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }


        }

        public string BatchInsert(string insertString, List<Dictionary<string, string>> insertData)
        {
            if (insertData.Count == 0)
            {
                return "No Data.";
            }
            try
            {
                MySqlCommand command = new MySqlCommand(insertString, SqlConnectionObj);
                SqlConnectionObj.Open();
                command.Parameters.Clear();

                foreach (KeyValuePair<string, string> keyValuePair in insertData[0])
                {
                    command.Parameters.AddWithValue("@" + keyValuePair.Key, keyValuePair.Value);
                }

                int index = 0;

                foreach (Dictionary<string, string> dictionary in insertData)
                {

                    index++;
                    if (index < insertData.Count)
                    {
                        insertString += ",(";
                        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                        {
                            command.Parameters.AddWithValue("@" + keyValuePair.Key + index, keyValuePair.Value);
                            insertString = insertString + "@" + keyValuePair.Key + index + ",";
                        }
                        insertString = insertString.TrimEnd(',');
                        insertString += ")";
                    }
                }

                command.CommandText = insertString;
                command.ExecuteNonQuery();
                command.Parameters.Clear();


                return "Data successfully Inserted.";

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Insert...Message: " + ex.Message);
                
            }

            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }

        }

        public string Insert(string queryString)
        {
            try
            {
                SqlConnectionObj.Open();
                SqlCommandobj.CommandText = queryString;
                SqlCommandobj.ExecuteNonQuery();
                return "Data Successfully Inserted.";
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Insert. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }


        }
        public string Update(string queryString)
        {
            try
            {

                SqlConnectionObj.Open();
                SqlCommandobj.CommandText = queryString;
                SqlCommandobj.ExecuteNonQuery();
                return "Data Successfully Updated.";


            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Update. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
        }
        public string Update(string queryString, Hashtable parameterTable)
        {
            try
            {

                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObj);
                foreach (DictionaryEntry entry in parameterTable)
                {
                    command.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                }

                SqlConnectionObj.Open();
                command.CommandText = queryString;
                command.ExecuteNonQuery();
                return "Data Successfully Updated.";


            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Update. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
        }


        public string Delete(string queryString)
        {
            try
            {
                SqlConnectionObj.Open();
                SqlCommandobj.CommandText = queryString;
                SqlCommandobj.ExecuteNonQuery();
                return "Data Successfully Deleted.";
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Delete. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
        }


        public string Delete(string queryString, Hashtable parameterTable)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(queryString, SqlConnectionObj);
                foreach (DictionaryEntry entry in parameterTable)
                {
                    command.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                }

                SqlConnectionObj.Open();
                command.CommandText = queryString;
                command.ExecuteNonQuery();

                return "Data Successfully Deleted.";
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Delete. Message: " + ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
        }
    }
}