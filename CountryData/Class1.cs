using System.IO.Compression;
using System.Linq;

namespace CountryData
{
    class Class1
    {
        public void Foo()
        {
            var zipArchive = ZipFile.OpenRead(@"C:\Code\CountryData\Data\PostCodes\PostCodes.zip");
            var entry = zipArchive.Entries.Single(x=>x.Name== "AU.json.txt");
            entry.Open();
        }
    }
}
