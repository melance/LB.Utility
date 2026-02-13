using LB.Utility.IO.FileTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.IO
{
    public class FileTypeValidation
    {
        /// <summary>
        /// Represents an unknown file type
        /// </summary>
        private static readonly FileTypeVerifyResult Unknown = new()
        {
            Name = "Unknown",
            Description = "Unknown File Type",
            IsValid = false
        };

        public static readonly List<FileType> WebImageTypes = new()
        {
            new Jpeg(),
            new Png()
        };

        public FileTypeValidation()
        {
            Types = new List<FileType>
                {
                    new Jpeg(),
                    new Png(),
                    new Mp3()
                }
                .OrderByDescending(x => x.SignatureLength)
                .ToList();
        }

        public FileTypeValidation(List<FileType> types) => Types = types;

        /// <summary>
        /// Types that are checked when verifying file type
        /// </summary>
        public List<FileType> Types { get; } = new();

        /// <summary>
        /// Validates that the file is in the list of types
        /// </summary>
        public Boolean IsInTypes(Stream stream)
        {
            return GetFileType(stream).IsValid;
        }

        /// <summary>
        /// Checks the file to determine the type
        /// </summary>
        public FileTypeVerifyResult GetFileType(String base64Bytes)
        {
            var data = Convert.FromBase64String(base64Bytes);
            using var mem = new MemoryStream(data);
            return GetFileType(mem);
        }

        /// <summary>
        /// Checks the file to determine the type
        /// </summary>
        public FileTypeVerifyResult GetFileType(Stream stream)
        {
            FileTypeVerifyResult? result = null;
            var index = 0;

            while (result == null && index < Types.Count)
            {
                var type = Types[index];
                if (type.Verify(stream))
                {
                    result = new()
                    {
                        Name = type.Name,
                        Description = type.Description,
                        MediaType = type.MediaType,
                        IsValid = true
                    };
                }
                index++;
            }

            return result ?? Unknown;
        }
    }
}
