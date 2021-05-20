using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Week_5_Console_Examples
{
    class Program
    {
        public static byte[] Compress(byte[] raw)
        {
            using(MemoryStream memory = new MemoryStream())
            {
                using(GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static byte[] Decompress(byte[] compressed, int originalSize)
        {
            byte[] uncompressed = new byte[originalSize];
            using (MemoryStream memory = new MemoryStream(compressed))
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Decompress, true))
                {
                    gzip.Read(uncompressed, 0, originalSize);
                }
                return uncompressed;
            }
        }

        public static void ZipArchiveExample()
        {
            string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string imagesPath = Path.Combine(myDoc, "VGP232/Images");

            string imagesZip = Path.Combine(imagesPath, "myImagesBundle.zip");

            DirectoryInfo dir = new DirectoryInfo(imagesPath);
            FileInfo[] allImagesFiles = dir.GetFiles("*.png");

            using(FileStream file = new FileStream(imagesZip, FileMode.Create))
            {
                using(ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Update))
                {
                    foreach(var png in allImagesFiles)
                    {
                        ZipArchiveEntry entry = zip.CreateEntryFromFile(png.FullName, png.Name, CompressionLevel.Optimal);
                    }
                }
            }
        }

        public static void ZipFileExtractionExample()
        {
            string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string imagesPath = Path.Combine(myDoc, "VGP232/Images");
            string extractedImagePath = Path.Combine(myDoc, "VGP232/Images/ExtractedImages");

            string imagesZip = Path.Combine(imagesPath, "myImagesBundle.zip");

            if(Directory.Exists(extractedImagePath))
            {
                Directory.Delete(extractedImagePath, true);
            }

            ZipFile.ExtractToDirectory(imagesZip, extractedImagePath);
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //byte[] text = Encoding.ASCII.GetBytes(new string('A', 200000));
            //File.WriteAllBytes("raw.bin", text);

            //byte[] compressed = Compress(text);
            //File.WriteAllBytes("compressed.bin", compressed);

            //byte[] expand = Decompress(compressed, 200000);
            //File.WriteAllBytes("uncompressed.bin", expand);

            //ZipArchiveExample();
            ZipFileExtractionExample();
        }
    }
}
