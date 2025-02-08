using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;



namespace Common
{
    public static class CommonFunction
    {
        

        public static string SendMail(string MailTo, string Mailcc, string Subject, string MailRemarks, string strBody, string Password)
        {

            #region oldCode
            /*
            Subject = "Assigned as a Auditee Head";
            string scheme = ConfigurationManager.AppSettings["SchemeForWebHost"];
            string host = ConfigurationManager.AppSettings["WebHost"];//localhost 
            string port = ConfigurationManager.AppSettings["WebPort"] + "/changepassword?u=" + Mailcc;//"44397/api/ActivateUser/Activate?uid="+MailTo;
            var varifyUrl = scheme + host + port; //Activationcode

            string Body = "<br/>You have been assigned as a Auditee Head for office IAD.</b><br/><br/>Your login credentials are given below. <br/>" + Mailcc + "<br/>" + Password + "<br/><br/>" + "Use the below given link for first time login and change the password." +
          " <br/><br/><a href='" + varifyUrl + "'>" + varifyUrl + "</a> " + "<br/>";
            Body = String.IsNullOrEmpty(MailRemarks) ? Body : Body + "<br/> <b>Remarks:</b>" + MailRemarks + "";
            Body = Body + "<br/>From  " + "<br/><b>IAD Head</b>";

           
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = ConfigurationManager.AppSettings["SmtpIP"];
            smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            MailMessage mailMessage = new MailMessage();
            mailMessage.Subject = Subject;
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"]);

            //TO eMail will be in CC as CCA designation.
            //CC eMail will be in To email as Auditee Head.
            if (ConfigurationManager.AppSettings["DummyTo"] == "")
            {
                string[] ToEmailArray = Mailcc.Split(new char[] { ',' });
                foreach (var email in ToEmailArray)
                {
                    mailMessage.To.Add(new MailAddress(email.Trim()));
                }
            }
            else
            {
                MailTo = ConfigurationManager.AppSettings["DummyTo"];
                mailMessage.To.Add(new MailAddress(MailTo.Trim()));

            }
            if (ConfigurationManager.AppSettings["CCEmail"].ToString() == "")
            {
                string[] ccAddressArray;
                ccAddressArray = MailTo.Split(new char[] { ',' });
                foreach (string email in ccAddressArray)
                {
                    mailMessage.CC.Add(new MailAddress(email.Trim()));
                }
            }
            else
            {
                Mailcc = ConfigurationManager.AppSettings["CCEmail"];
                mailMessage.CC.Add(new MailAddress(Mailcc.Trim()));
            }

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = Body;

            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            try
            {
                smtpClient.Send(mailMessage);
                strMessage = "OnSuccess";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            mailMessage.Dispose();
            */
            #endregion
            string strMessage = string.Empty;
           

            //mailMessage.Dispose();
            return strMessage;

        }
          public static string RandomPassword()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            // _rdm.Next(_min, _max);
            return "EPS@" + _rdm.Next(_min, _max);
        }
        public static string GetIPAddress()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();

        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            if (list == null || list.Count() == 0)
            {
                return new DataTable();
            }
            DataTable table = new DataTable();
            var properties = new PropertyInfo[20]; // typeof(T).GetProperties();
            foreach (T l in list)
            {
                Type t = l.GetType();
                properties = t.GetProperties();
                break;
            }
            foreach (var property in properties)
            {
                Type type = Type.GetType(property.PropertyType.FullName);
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = Nullable.GetUnderlyingType(type);
                }
                table.Columns.Add(property.Name, type);
            }
            foreach (T li in list)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(li, null);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static void SendGenericMail(string MailTo, string Subject, string strBody, string mailCC = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            string strMessage = string.Empty;
            try
            {

                smtpClient.Host = ConfigurationManager.AppSettings["SmtpIP"];
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

                mailMessage.Subject = Subject;
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"]);
                mailMessage.From = fromAddress;

                MailAddress toAddr = new MailAddress(MailTo.ToString().Trim());
                mailMessage.To.Add(toAddr);

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = strBody;

                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;
                if (!string.IsNullOrEmpty(mailCC))
                {
                    MailAddress ccAddress = new MailAddress(mailCC.ToString().Trim());
                    mailMessage.CC.Add(toAddr);
                }

                try
                {
                    smtpClient.Send(mailMessage);
                    mailMessage.Dispose();
                }
                catch (Exception ex)
                {

                    strMessage = "Exception Message: " + ex.Message;
                    try
                    {
                        strMessage += ("Inner Exception" + ex.InnerException?.ToString());
                    }
                    catch (Exception ax)
                    {
                        strMessage += "1 - Exception Message: " + ex.Message;

                    }
                    try
                    {
                        strMessage += ("Stack Trace: " + ex.StackTrace?.ToString());
                    }
                    catch (Exception ax)
                    {

                        strMessage += "1 - Exception Message: " + ex.Message;
                    }

                    throw ex;
                }

            }
            catch (Exception ex)
            {

                strMessage = "2 Exception Message: " + ex.Message;
                try
                {
                    strMessage += ("2 Inner Exception" + ex.InnerException?.ToString());
                }
                catch (Exception ax)
                {
                    strMessage += "2 1 - Exception Message: " + ex.Message;
                }
                try
                {
                    strMessage += ("2 Stack Trace: " + ex.StackTrace?.ToString());
                }
                catch (Exception ax)
                {
                    strMessage += "2 1 - Exception Message: " + ex.Message;
                }

                throw ex;
            }
        }
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.  
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.  
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }
        public static string DecryptStringAES(string cipherText)
        {
            //var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var keybytes = Encoding.UTF8.GetBytes("SPA865HYEMEW237K");
            var iv = Encoding.UTF8.GetBytes("SPA865HYEMEW237K");
            string converted = string.Empty;
            converted = cipherText.Replace('-', '+').Replace(' ', '+').Replace('_', '/').PadRight(4 * ((cipherText.Length + 3) / 4), '=');
            var encrypted = Convert.FromBase64String(converted);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }
        public static string EncryptStringAES(string cipherText)
        {
            //var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var keybytes = Encoding.UTF8.GetBytes("SPA865HYEMEW237K");
            var iv = Encoding.UTF8.GetBytes("SPA865HYEMEW237K");

            var encrypted = EncryptStringToBytes(cipherText, keybytes, iv);
            var encryptedvalue = Convert.ToBase64String(encrypted);
            return string.Format(encryptedvalue);
        }
        private const string AesIV256 = @"&TER%$HFTRE*&GTR";
        private const string AesKey256 = @"TYW{T465}[56]TREJH%^$*GTEJR%*(O)";
        /// <summary>
        /// getComDecryption
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int getComDecryption(string value)
        {
            int id;
            if (Int32.TryParse(value, out int Value))
            {
                id = Int32.Parse(value);
            }
            else
            {
                try
                {
                    id = Convert.ToInt32(Decrypt256(value));
                }
                catch
                {
                    id = Convert.ToInt32(DecryptStringAES(value));
                }
            }
            return id;
        }

        /// <summary>
        /// String Decryption
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string getStringComDecryption(string value)
        {
            try
            {
                string data;
                try
                {
                    data = Decrypt256(value);
                }
                catch
                {
                    data = DecryptStringAES(value);
                }
                return data;
            }
            catch
            {
                return "";
            }
            
        }

        public static string Encrypt256(string text)
        {
            // AesCryptoServiceProvider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Convert string to byte array
            byte[] src = Encoding.Unicode.GetBytes(text);

            // encryption
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                // Convert byte array to Base64 strings
                return Convert.ToBase64String(dest);
            }
        }
        /// <summary>
        /// AES decryption
        /// </summary>
        public static string Decrypt256(string text)
        {
            // AesCryptoServiceProvider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Convert Base64 strings to byte array
       
                string converted = string.Empty;
                converted = text.Replace('-', '+').Replace(' ', '+').Replace('_', '/').PadRight(4 * ((text.Length + 3) / 4), '=');
                byte[] src = System.Convert.FromBase64String(converted);
           
            // decryption
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }
        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        /// <summary>
        /// Global function to Convert Reader data in to List of type T
        /// </summary>
        public static List<T> DataReaderToList<T>(IDataReader rdr)
        {
            List<T> lst = new List<T>();
            T entity;
            Type type = typeof(T);
            PropertyInfo col;
            List<PropertyInfo> columns = new List<PropertyInfo>();

            //Get all the properties in etity class

            PropertyInfo[] props = type.GetProperties();

            for (int index = 0; index < rdr.FieldCount; index++)
            {
                col = props.FirstOrDefault(c => c.Name == rdr.GetName(index));
                if (col != null)
                {
                    columns.Add(col);
                }
            }

            while (rdr.Read())
            {
                entity = Activator.CreateInstance<T>();

                for (int idx = 0; idx < columns.Count; idx++)
                {
                    if (rdr[columns[idx].Name].Equals(DBNull.Value))
                    {
                        columns[idx].SetValue(entity, null, null);
                    }
                    else
                    {
                        columns[idx].SetValue(entity, rdr[columns[idx].Name], null);
                    }
                }
                lst.Add(entity);
            }
            return lst;
        }
    }
    public class MimeType
    {
        private static readonly byte[] BMP = { 66, 77 };
        private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] EXE_DLL = { 77, 90 };
        private static readonly byte[] GIF = { 71, 73, 70, 56 };
        private static readonly byte[] ICO = { 0, 0, 1, 0 };
        private static readonly byte[] JPG = { 255, 216, 255 };
        private static readonly byte[] MP3 = { 255, 251, 48 };
        private static readonly byte[] OGG = { 79, 103, 103, 83, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        private static readonly byte[] RAR = { 82, 97, 114, 33, 26, 7, 0 };
        private static readonly byte[] SWF = { 70, 87, 83 };
        private static readonly byte[] TIFF = { 73, 73, 42, 0 };
        private static readonly byte[] TORRENT = { 100, 56, 58, 97, 110, 110, 111, 117, 110, 99, 101 };
        private static readonly byte[] TTF = { 0, 1, 0, 0, 0 };
        private static readonly byte[] WAV_AVI = { 82, 73, 70, 70 };
        private static readonly byte[] WMV_WMA = { 48, 38, 178, 117, 142, 102, 207, 17, 166, 217, 0, 170, 0, 98, 206, 108 };
        private static readonly byte[] ZIP_DOCX = { 80, 75, 3, 4 };
        private static readonly byte[] XLSX = { 80, 75, 3, 4, 20, 0, 6, 0 };
        private static readonly byte[] XLS = { 208, 207, 17, 224, 161, 177, 26, 225 };

        public static string GetMimeType(byte[] file, string fileName)
        {

            string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE

            //Ensure that the filename isn't empty or null
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return mime;
            }

            //Get the file extension
            string extension = Path.GetExtension(fileName) == null
                                   ? string.Empty
                                   : Path.GetExtension(fileName).ToUpper();

            //Get the MIME Type
            if (file.Take(2).SequenceEqual(BMP))
            {
                mime = "image/bmp";
            }
            else if (file.Take(8).SequenceEqual(DOC))
            {
                if(extension==".DOC")
                {
                    mime = "application/msword";
                }
                else if (extension == ".XLS")
                {
                    mime = "application/vnd.ms-excel";
                }               
            }
            else if (file.Take(2).SequenceEqual(EXE_DLL))
            {
                mime = "application/x-msdownload"; //both use same mime type
            }
            else if (file.Take(4).SequenceEqual(GIF))
            {
                mime = "image/gif";
            }
            else if (file.Take(4).SequenceEqual(ICO))
            {
                mime = "image/x-icon";
            }
            else if (file.Take(3).SequenceEqual(JPG))
            {
                mime = "image/jpeg";
            }
            else if (file.Take(3).SequenceEqual(MP3))
            {
                mime = "audio/mpeg";
            }
            else if (file.Take(14).SequenceEqual(OGG))
            {
                if (extension == ".OGX")
                {
                    mime = "application/ogg";
                }
                else if (extension == ".OGA")
                {
                    mime = "audio/ogg";
                }
                else
                {
                    mime = "video/ogg";
                }
            }
            else if (file.Take(7).SequenceEqual(PDF))
            {
                mime = "application/pdf";
            }
            else if (file.Take(16).SequenceEqual(PNG))
            {
                mime = "image/png";
            }
            else if (file.Take(7).SequenceEqual(RAR))
            {
                mime = "application/x-rar-compressed";
            }
            else if (file.Take(3).SequenceEqual(SWF))
            {
                mime = "application/x-shockwave-flash";
            }
            else if (file.Take(4).SequenceEqual(TIFF))
            {
                mime = "image/tiff";
            }
            else if (file.Take(11).SequenceEqual(TORRENT))
            {
                mime = "application/x-bittorrent";
            }
            else if (file.Take(5).SequenceEqual(TTF))
            {
                mime = "application/x-font-ttf";
            }
            else if (file.Take(4).SequenceEqual(WAV_AVI))
            {
                mime = extension == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
            }
            else if (file.Take(16).SequenceEqual(WMV_WMA))
            {
                mime = extension == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
            }
            else if (file.Take(8).SequenceEqual(XLSX))
            {
                mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            else if (file.Take(8).SequenceEqual(XLS))
            {
                mime = "application/vnd.ms-excel";
            }
            else if (file.Take(4).SequenceEqual(ZIP_DOCX))
            {
                mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/x-zip-compressed";
            }    

            return mime;
        }

        
    }
}
