using System;
using System.IO;

namespace Coft.ImageResizer.Models.Services
{
    public interface IZipService
    {
        string GetNewArchiveName(string oldName);
        void ParseZip(FileStream zipToOpen, FileStream zipToWrite, Predicate<string> fileNameFilter, Action<Stream, Stream> parseAction, Action<int> processingPercentage);
    }
}