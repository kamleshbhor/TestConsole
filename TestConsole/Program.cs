using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TestConsole
{
    class Program
    {

        public static void Main(string[] args)
        {
            //getExcelFile();
            // getExcelFileSGSCORTEMPLATE();
            //getExcelCORTemplateProp();
             getTaxonomyData();
            //getCorTemplatePropertiesData();
            //TestLength();
            //validateUser();
            //VarcharTest();
            //string salt = GetSalt();
            //string pwd = HashItWithrfc2898Util("Kamlesh", "9TmOSaGUBIZS1x1S531oCV2ySVLVbJZz9030KvtSK3w=");
            //TestGetAPI();
            //TestPostAPI();
            //TestPutAPI();
            //TestDeleteAPI();
        }

        public static void getExcelCORTemplateProp()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kamlesh.bhor\Desktop\Corr.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            using (var file = new StreamWriter(@"C:\Users\kamlesh.bhor\Desktop\Cor1.txt"))
            {
                //iterate over the rows and columns and print to the file as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    // write the value to the console
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        string text = xlRange.Cells[i, 1].Value2.ToString();
                        string word1 = "DOC_TYPE_VALUE='";
                        string word2 = "DOC_CATEGORY_VALUE= '";
                        string word3 = "DOC_CLASS_VALUE='";
                        string word4 = "template_name='";
                        var FirstString = text.Split(new[] { word1 }, StringSplitOptions.None)[1];
                        string docTypeValue = FirstString.Split(new[] { "'" }, StringSplitOptions.None)[0];
                        var SecondString = FirstString.Split(new[] { word2 }, StringSplitOptions.None)[1];
                        string docCategoryValue = SecondString.Split(new[] { "'" }, StringSplitOptions.None)[0];
                        var ThirdString= SecondString.Split(new[] { word3 }, StringSplitOptions.None)[1];
                        string docClassValue = ThirdString.Split(new[] { "'" }, StringSplitOptions.None)[0];
                        var FourthString = ThirdString.Split(new[] { word4 }, StringSplitOptions.None)[1];
                        string templateName = FourthString.Split(new[] { "'" }, StringSplitOptions.None)[0];

                        string temp = string.Format(@"IF EXISTS(SELECT * FROM sgt_cor_template_properties where TEMPLATE_ID=(select template_id from sgs_cor_templates where template_name='{0}'))
BEGIN
	IF NOT EXISTS( SELECT * FROM sgt_cor_template_properties where TEMPLATE_ID=(select template_id from sgs_cor_templates where template_name='{0}') AND  DOC_TYPE_VALUE='{1}' AND DOC_CATEGORY_VALUE= '{2}'AND DOC_CLASS_VALUE='{3}')
	BEGIN
		{4}
	END
END
", templateName, docTypeValue, docCategoryValue, docClassValue, text);

                        file.WriteLine(temp);
                    }
                }
                file.Close();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        public static void getExcelFileSGSCORTEMPLATE()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kamlesh.bhor\Desktop\Corr.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            using (var file = new StreamWriter(@"C:\Users\kamlesh.bhor\Desktop\Cor1.txt"))
            {
                //iterate over the rows and columns and print to the file as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    // write the value to the console
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        string text = xlRange.Cells[i, 1].Value2.ToString();
                        string word1 = "template_group_value='";
                        string word2 = "template_name='";
                        var FirstString = text.Split(new[] { word1 }, StringSplitOptions.None)[1];
                        string One = FirstString.Split(new[] { "'" }, StringSplitOptions.None)[0];
                        var SecondString = FirstString.Split(new[] { word2 }, StringSplitOptions.None)[1];
                        string Two = SecondString.Split(new[] { "'" }, StringSplitOptions.None)[0];

                        string temp = string.Format(@"IF NOT EXISTS(SELECT * FROM sgs_cor_templates where template_group_value='{0}' AND template_name='{1}')
BEGIN
	{2}
END
", One, Two, text);

                        file.WriteLine(temp);
                    }
                }
                file.Close();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        public static void getExcelFile()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kamlesh.bhor\Desktop\Corr.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            using (var file = new StreamWriter(@"C:\Users\kamlesh.bhor\Desktop\Cor1.txt"))
            {
                //iterate over the rows and columns and print to the file as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    // write the value to the console
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 2] != null && xlRange.Cells[i, 1].Value2 != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        //                        string temp = string.Format(@"IF NOT EXISTS(SELECT 1 FROM SGS_CODE_VALUE WITH(NOLOCK) WHERE CODE_ID = 2619 AND CODE_VALUE = '{0}') 
                        //BEGIN
                        //	 INSERT INTO DBO.SGS_CODE_VALUE (CODE_ID, CODE_VALUE, DESCRIPTION, DATA1, DATA2, DATA3, COMMENTS, START_DATE, END_DATE, CODE_VALUE_ORDER, LEGACY_CODE_ID, CREATED_BY, CREATED_DATE, MODIFIED_BY, MODIFIED_DATE, UPDATE_SEQ)
                        //	 VALUES (2619,'{1}','{2}',NULL,NULL, NULL,NULL,NULL, NULL,NULL,NULL, 'Kamlesh.Bhor',GETDATE(),'Kamlesh.Bhor',GETDATE(),0)
                        //END
                        //GO 
                        //", xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 2].Value2.ToString());
                        //Console.Write(temp + "\n");
                        string temp = string.Format(@"IF NOT EXISTS(SELECT 1 FROM SGS_CODE_VALUE WITH(NOLOCK) WHERE CODE_ID = 2619 AND CODE_VALUE = '{0}') 
BEGIN
	 INSERT INTO DBO.SGS_CODE_VALUE (CODE_ID, CODE_VALUE, DESCRIPTION, DATA1, DATA2, DATA3, COMMENTS, START_DATE, END_DATE, CODE_VALUE_ORDER, LEGACY_CODE_ID, CREATED_BY, CREATED_DATE, MODIFIED_BY, MODIFIED_DATE, UPDATE_SEQ)
	 VALUES (2619,'{1}','{2}',NULL,NULL, NULL,NULL,NULL, NULL,NULL,NULL, 'Kamlesh.Bhor',GETDATE(),'Kamlesh.Bhor',GETDATE(),0)
END
GO
", xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 2].Value2.ToString());

                        file.WriteLine(temp);
                    }
                }
                file.Close();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        public static void getTaxonomyData()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kamlesh.bhor\Desktop\Corr.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            using (var file = new StreamWriter(@"C:\Users\kamlesh.bhor\Desktop\Cor1.txt"))
            {
                //iterate over the rows and columns and print to the file as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    // write the value to the console
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 2] != null && xlRange.Cells[i, 1].Value2 != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        string temp = string.Format(@"IF NOT EXISTS(SELECT 1 FROM [SGT_FILENET_TAXONOMY] WITH (NOLOCK) WHERE [CLASS_DESCRIPTION]='{1}' AND [CATEGORY_DESCRIPTION]='{3}' AND [DOC_TYPE]='{4}')
BEGIN
  INSERT INTO [dbo].[SGT_FILENET_TAXONOMY] ([CLASS_VALUE],[CLASS_DESCRIPTION],[CATEGORY_ID],[CATEGORY_VALUE],[CATEGORY_DESCRIPTION],[DOC_TYPE],[DOC_DESCRIPTION],[FN_SECURITY],[CREATED_BY],[CREATED_DATE],[MODIFIED_BY],[MODIFIED_DATE],[UPDATE_SEQ])
  VALUES ('{0}','{1}',2757,'{2}','{3}','{4}','{5}','spModify','{6}',GETDATE(),'Kamlesh.Bhor',GETDATE(),	0)
END
GO
", xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 2].Value2.ToString(), xlRange.Cells[i, 3].Value2.ToString(), xlRange.Cells[i, 4].Value2.ToString(), xlRange.Cells[i, 5].Value2.ToString(), xlRange.Cells[i, 6].Value2.ToString(), xlRange.Cells[i, 7].Value2.ToString());

                        file.WriteLine(temp);
                    }
                }
                file.Close();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        public static void getCorTemplatePropertiesData()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kamlesh.bhor\Desktop\Corr.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            using (var file = new StreamWriter(@"C:\Users\kamlesh.bhor\Desktop\Cor1.txt"))
            {
                //iterate over the rows and columns and print to the file as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    // write the value to the console
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 2] != null && xlRange.Cells[i, 1].Value2 != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        string temp = string.Format(@"IF EXISTS(SELECT * FROM SGT_COR_TEMPLATE_PROPERTIES WITH (NOLOCK) WHERE TEMPLATE_ID=(SELECT TOP 1 TEMPLATE_ID FROM SGS_COR_TEMPLATES WITH (NOLOCK) WHERE TEMPLATE_NAME='{0}'))
BEGIN
	IF NOT EXISTS( SELECT * FROM SGT_COR_TEMPLATE_PROPERTIES WITH (NOLOCK) WHERE TEMPLATE_ID=(SELECT TOP 1 TEMPLATE_ID FROM SGS_COR_TEMPLATES WITH (NOLOCK) WHERE TEMPLATE_NAME='{0}') AND  DOC_TYPE_VALUE='{1}' AND DOC_CATEGORY_VALUE= '{2}'AND DOC_CLASS_VALUE='{3}')
	BEGIN
		UPDATE SGT_COR_TEMPLATE_PROPERTIES SET DOC_TYPE_VALUE='{1}',DOC_CATEGORY_VALUE= '{2}', DOC_CLASS_VALUE='{3}' WHERE TEMPLATE_ID=(SELECT TOP 1 TEMPLATE_ID FROM SGS_COR_TEMPLATES WHERE TEMPLATE_NAME='{0}');
	END
END
", xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 2].Value2.ToString(), xlRange.Cells[i, 3].Value2.ToString(), xlRange.Cells[i, 4].Value2.ToString());

                        file.WriteLine(temp);
                    }
                }
                file.Close();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }
        public static void TestLength()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kamlesh.bhor\Desktop\Corr.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            using (var file = new StreamWriter(@"C:\Users\kamlesh.bhor\Desktop\Cor1.txt"))
            {
                //iterate over the rows and columns and print to the file as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {

                    //                string temp = string.Format(@"IF NOT EXISTS(SELECT 1 FROM SGS_CODE_VALUE WITH(NOLOCK) WHERE CODE_ID = 2757 AND CODE_VALUE = '{0}') 
                    //BEGIN
                    //	 INSERT INTO DBO.SGS_CODE_VALUE (CODE_ID, CODE_VALUE, DESCRIPTION, DATA1, DATA2, DATA3, COMMENTS, START_DATE, END_DATE, CODE_VALUE_ORDER, LEGACY_CODE_ID, CREATED_BY, CREATED_DATE, MODIFIED_BY, MODIFIED_DATE, UPDATE_SEQ)
                    //	 VALUES (2757,'{1}','{2}',NULL,NULL, NULL,NULL,NULL, NULL,NULL,NULL, 'Kamlesh.Bhor',GETDATE(),'Kamlesh.Bhor',GETDATE(),0)
                    //END
                    //GO
                    //", xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 2].Value2.ToString());
                    string abc = xlRange.Cells[i, 1].Value2.ToString();
                        file.WriteLine(xlRange.Cells[i, 1].Value2.ToString() +"-" + abc.Length);
                    
                }
                file.Close();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        public static void validateUser()
        {
            


            string username = "admin";
            string dbpassword = "0api1+Rf4+1G9iE0HX9fwQ==";
            SqlParameter password = new SqlParameter();

            password.ParameterName ="@pwd";// Defining Name
            password.SqlDbType = SqlDbType.VarChar; // Defining DataType
            password.Direction = ParameterDirection.Input;
            password.Size = dbpassword.Length;

           
            string connectionstring = "Server=192.10.215.153;Database=NeoSISBase_WV_Dev;User ID=DEVUSER;password=Sagitec11;Persist Security Info=True;Asynchronous Processing=True; Column Encryption Setting=Enabled";
            try
            {
                //select @nm=name,@ct=city from student_detail where rollno=@rn"
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionstring);
                conn.Open();
                var lPassword = "declare @password varchar(100) = '0api1+Rf4+1G9iE0HX9fwQ == '";

                // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM SGS_User Where USER_ID = '" + username + "' AND PASSWORD=@'" +dbpassword + "'", conn);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                // cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 0).Value = dbpassword;
                cmd.Parameters.Add(password);
                password.Value = dbpassword;
                cmd.Connection = conn;
                
                cmd.CommandText = "SELECT * FROM SGS_User Where USER_ID = '" + username + "' AND PASSWORD='" + password + "'";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                }
                conn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void VarcharTest() {
            //<add key="SagiProviderConnectionString" value="Server=192.10.215.180;Database=NeoSISBase_MD_Dev;User ID=uiuser;password=Sagitec12;"/><add key="SagiProviderConnectionString" value="Server=192.10.215.180;Database=NeoSISBase_MD_Dev;User ID=uiuser;password=Sagitec12;"/>
            using (var connection = new SqlConnection(@"Data Source=192.10.215.180;Initial Catalog=NeoSISBase_MD_Dev;User ID=uiuser;password=Sagitec12;Persist Security Info=True;Asynchronous Processing=True;"))
            {
                string username = "admin";
                //string passwrd = "0api1+Rf4+1G9iE0HX9fwQ==";
                string passwrd = "Es0YP8zC9zjjNg3Q1W7jHw=";

                connection.Open();

                var command = new SqlCommand("SELECT * FROM SGS_User Where USER_ID = '" + username + "' AND PASSWORD=@Password", connection);
                // inferred from string length
                command.Parameters.Add("@Password", SqlDbType.VarChar, -1).Value = passwrd;
                                
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine();
                    }
                    var table = reader.GetSchemaTable();

                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine("{0} = {1}({2}) => SqlDbType.{3}",
                            row["ColumnName"],
                            row["DataTypeName"],
                            row["ColumnSize"],
                            Enum.GetName(typeof(SqlDbType), row["NonVersionedProviderType"]));
                    }
                }
            }
        }
        private const int BYTE_LENGTH = 32;
        private const int RFC2898_ITERATION = 20000;
        public static string GetSalt()
        {

            var lobjRandom = new RNGCryptoServiceProvider();

            // Empty salt array
            byte[] lbytSalt = new byte[BYTE_LENGTH];


            // Build the random bytes
            lobjRandom.GetNonZeroBytes(lbytSalt);

            // Return the string encoded salt
            return Convert.ToBase64String(lbytSalt);
        }

        public static string HashItWithrfc2898Util(string astrPassword, string astrSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(astrSalt);

            //==>password-based key derivation functionality PBKDF2
            Rfc2898DeriveBytes lRfcPBKDF2 = new Rfc2898DeriveBytes(astrPassword,
                                                                saltBytes,
                                                                RFC2898_ITERATION);
            byte[] Rfc2898DeriveBytes = lRfcPBKDF2.GetBytes(BYTE_LENGTH);
            string computedHash = BitConverter.ToString(Rfc2898DeriveBytes);
            lRfcPBKDF2.Reset();
            return computedHash.Replace("-", "");
        }

        public static void TestGetAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://sagitec-1503/restservice/");
            // Add an Accept header for JSON format.  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.  
            HttpResponseMessage response = client.GetAsync("api/Employee/1").Result;  // Blocking call!  
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                var customerJsonString = Convert.ToString(response.Content.ReadAsStringAsync());
                Console.WriteLine("Your response data is: " + customerJsonString);

                // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
                var deserialized = JsonConvert.DeserializeObject<Employee>(customerJsonString);
                // Do something with it
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            Console.ReadLine();
        }

        public static void TestPostAPI()
        {
            using (var client = new HttpClient())
            {
                Employee p = new Employee { EmployeeId = 1, EmployeeName = "Test", Address="Test", Department="Dept" };
                client.BaseAddress = new Uri("http://localhost:63165/");
                var response = client.PostAsJsonAsync("api/Employee", p).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                    Console.WriteLine("Error");
            }
        }

        public static void TestPutAPI()
        {
            using (var client = new HttpClient())
            {
                Employee p = new Employee { EmployeeId = 1, EmployeeName = "Test", Address = "Test", Department = "Dept" };
                client.BaseAddress = new Uri("http://localhost:63165/");
                var response = client.PutAsJsonAsync("api/Employee/1", p).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
        }

        public static void TestDeleteAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63165/");
            var response = client.DeleteAsync("api/Employee/10").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Write("Success");
            }
            else
                Console.Write("Error");
        }

        public class Employee
        {
            public int EmployeeId
            {
                get;
                set;
            }
            public string EmployeeName
            {
                get;
                set;
            }
            public string Address
            {
                get;
                set;
            }
            public string Department
            {
                get;
                set;
            }
        }
    }



}
