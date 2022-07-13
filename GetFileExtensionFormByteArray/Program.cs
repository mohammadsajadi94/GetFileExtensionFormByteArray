using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using MimeDetective;

namespace GetFileExtensionFormByteArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStream fileMht = new FileStream("enter path file here", FileMode.Open);
            byte[] bfile = new byte[fileMht.Length];
            fileMht.Read(bfile, 0, Convert.ToInt32(bfile.Length));
            string FileExtension = GetFileExtensionFormByteArray(bfile);
            fileMht.Close();
        }
        public static string GetFileExtensionFormByteArray(byte[] byteArray)
        {
            Stream stream = new MemoryStream(byteArray);
            var content = MimeDetective.ContentReader.Default.ReadFromStream(stream);
            var ins = new MimeDetective.ContentInspectorBuilder()
            {
                Definitions = MimeDetective.Definitions.Default.All()
            }.Build();
            var res = ins.Inspect(content);
            var fi = res.ByFileExtension();
            var type = fi[0].Extension.ToString();
            return type;
        }
    }
}
