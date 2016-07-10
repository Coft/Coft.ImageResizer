using Coft.ImageResizer.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.ImageResizer.Models.Services
{
    public class ZipService : IZipService
    {
        public void ParseZip(FileStream zipToOpen, FileStream zipToWrite, Predicate<string> fileNameFilter, Action<Stream, Stream> parseAction, Action<int> processingPercentage)
        {
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read),
                                newArchive = new ZipArchive(zipToWrite, ZipArchiveMode.Create))
            {
                int entriesDone = 0;
                int entriesCount = archive.Entries.Count;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    processingPercentage(100 * entriesDone++ / entriesCount);

                    if (fileNameFilter(entry.Name))
                    {
                        ZipArchiveEntry newEntry = newArchive.CreateEntry(entry.FullName);

                        using (Stream stream = entry.Open(),
                            newStream = newEntry.Open())
                        {
                            parseAction(stream, newStream);
                        }
                    }
                }
            }
        }

        public string GetNewArchiveName(string oldName)
        {
            return oldName.Replace(".zip", Configuration.FilenameApendix + ".zip");
        }
    }
}
