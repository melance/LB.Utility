using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LB.Utility.IO.FileTypes
{
    public abstract class FileType
    {
        public String? Description { get; protected set; }
        public String? Name { get; protected set; }
        public String? MediaType { get; protected set; }
        private List<String> Extensions { get; } = new();
        private List<Byte[]> Signatures { get; } = new();

        public Int32 SignatureLength => Signatures.Max(m => m.Length);

        protected FileType AddSignatures(params Byte[][] bytes)
        {
            Signatures.AddRange(bytes);
            return this;
        }

        protected FileType AddExtensions(params String[] extensions)
        {
            Extensions.AddRange(extensions);
            return this;
        }

        public Boolean Verify(String base64Bytes)
        {
            var data = Convert.FromBase64String(base64Bytes);
            var mem = new MemoryStream(data);
            var result = Verify(mem);
            mem.Dispose();
            return result;
        }

        public Boolean Verify(Stream stream)
        {
            stream.Position = 0;
            var reader = new BinaryReader(stream);
            var headerBytes = reader.ReadBytes(SignatureLength);

            return Signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
        }
    }
}