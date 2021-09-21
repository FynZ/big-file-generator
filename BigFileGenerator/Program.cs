using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BigFileGenerator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var directory = new DirectoryInfo(@"C:\Users\Fynzie\Desktop\big_files");
            directory.Create();
            Console.WriteLine($"Generating files to directory {directory.FullName}");

            await CreateBigFile(directory, new FileSize(40, Size.MegaBytes));
            await CreateBigFile(directory, new FileSize(100, Size.MegaBytes));
            await CreateBigFile(directory, new FileSize(250, Size.MegaBytes));
            await CreateBigFile(directory, new FileSize(500, Size.MegaBytes));

            Console.WriteLine($"Generation end");

            return 0;
        }

        public static ValueTask CreateBigFile(DirectoryInfo directory, FileSize size)
        {
            var fileName = $"File_{size.Size}-{size.Type.ToString()}";

            var stream = File.Create($"{directory.FullName}/{fileName}");

            var length = size.ByteCount;
            var content = new string('A', (int) length);
            return stream.WriteAsync(Encoding.UTF8.GetBytes(content));
        }
    }

    public readonly struct FileSize
    {
        public int Size { get; }
        public Size Type { get; }

        public FileSize(int size, Size type)
        {
            Size = size;
            Type = type;
        }

        public long ByteCount => Size * (long)Type;
    }

    public enum Size
    {
        Byte = 1,
        KiloBytes = 1024,
        MegaBytes = 1024 * 1024,
        GigaBytes = 1024 * 1024 * 1024
    }
}
