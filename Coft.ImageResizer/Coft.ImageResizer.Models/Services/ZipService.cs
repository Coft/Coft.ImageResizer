using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.ImageResizer.Models.Services
{
    public class ZipService
    {
        public static void ParseZip(FileStream zipToOpen, FileStream zipToWrite)
        {
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read),
                                newArchive = new ZipArchive(zipToWrite, ZipArchiveMode.Create))
            {

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (ImageService.IsImageFilename(entry.Name))
                    {
                        ZipArchiveEntry newEntry = newArchive.CreateEntry(entry.FullName);

                        using (Stream stream = entry.Open(),
                            newStream = newEntry.Open())
                        {
                            ImageService.ResizeImage(stream, newStream);
                        }
                    }
                }
            }
        }
    }
}
