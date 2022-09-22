using AutoMapper;
using CommonEntities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    if (sizeInMb < 1024)
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
              

                string FileName = oFile.FileName+"_"+Guid.NewGuid().ToString() + Path.GetExtension(oFile.FileName);
                string FilePath = path;

                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    oFile.CopyTo(stream);
                }

                fileModel.FILE_NAME = oFile.FileName;
                fileModel.FILE_PATH = FilePath;
                fileModel.FILE_SAVED_NAME = FileName;
                fileModel.FILE_SIZE = (oFile.Length * 1024 * 1024).ToString();
                fileModel.FILE_TYPE = Path.GetExtension(oFile.FileName);

                return fileModel;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
           

        }
        #endregion

        #region AutoMapper
        public static Mapper GetMapperConfig<T,T1>()
        {
            var config = new MapperConfiguration(mc => mc.CreateMap<T, T1>());
            return new Mapper(config);
        }
        #endregion
    }
}
