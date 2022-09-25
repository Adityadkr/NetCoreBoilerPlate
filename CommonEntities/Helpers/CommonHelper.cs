using AutoMapper;
using CommonEntities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public static class CommonHelper
    {

        static CommonHelper()
        {

        }

        #region FileHelper
        public static string ValidateFile(IFormFile oFile, string[] type, long sizeInMb)
        {
            try
            {
                string fileExtension = Path.GetExtension(oFile.FileName);
                long fileSize = oFile.Length;
                if (!type.Any(fileExtension.Contains))
                {
                    return "Invalid file format, Only" + String.Join(",", type) + " files are allowed";
                }
                else if (fileSize > (sizeInMb * 1024 * 1024))
                {
                    if (sizeInMb < 1)
                    {
                        return "File cannot be more than " + (sizeInMb * 1024) + " KB";
                    }
                    else
                    {
                        return "File cannot be more than " + sizeInMb + " MB";
                    }
                }
                else
                {
                    return "Valid file";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public static FileModel UploadFile(IFormFile oFile, string path)
        {

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileModel = new FileModel();


                string FileName = Regex.Replace(oFile.FileName.Replace(" ", ""), @"[^0-9a-zA-Z]+", "") + "_" + Guid.NewGuid().ToString() + Path.GetExtension(oFile.FileName);
                string FilePath = Path.Combine(path, FileName);

                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                  oFile.CopyTo(stream);
                }

                fileModel.FILE_NAME = oFile.FileName;
                fileModel.FILE_PATH = FilePath;
                fileModel.HALF_FILE_PATH = String.IsNullOrEmpty(FilePath.Split("wwwroot")[1]) ? "" : FilePath.Split("wwwroot")[1];
                fileModel.FILE_SAVED_NAME = FileName;
                fileModel.FILE_SIZE = GetFileSize(FilePath).ToString();
                fileModel.FILE_TYPE = Path.GetExtension(oFile.FileName);

                return fileModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        static long GetFileSize(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                return new FileInfo(FilePath).Length;
            }
            return 0;
        }
        #endregion

        #region AutoMapper
        public static Mapper GetMapperConfig<T, T1>()
        {
            var config = new MapperConfiguration(mc => mc.CreateMap<T, T1>());
            return new Mapper(config);
        }
        #endregion  

        #region BcryptPassword
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string EncryptPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        #endregion


        #region EncryptDecryptString
        public static string Encrypt(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return s;
            }
            else
            {
                var encoding = new UTF8Encoding();
                byte[] plain = encoding.GetBytes(s);
                byte[] secret = ProtectedData.Protect(plain, null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(secret);
            }
        }
        public static string Decrypt(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return s;
            }
            else
            {
                byte[] secret = Convert.FromBase64String(s);
                byte[] plain = ProtectedData.Unprotect(secret, null, DataProtectionScope.CurrentUser);
                var encoding = new UTF8Encoding();
                return encoding.GetString(plain);
            }
        }
        #endregion
    }
}
