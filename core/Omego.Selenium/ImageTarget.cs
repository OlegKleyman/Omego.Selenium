namespace Omego.Selenium
{
    using System;
    using System.IO;

    public class ImageTarget
    {
        public string Directory { get; }
        public string FileName { get; }

        public ImageTarget(string directory, string fileName)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (fileName.Length == 0) throw new ArgumentException("Value cannot be empty.", nameof(fileName));

            if (directory.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new ArgumentException(
                    $"Directory \"{directory}\" contains invalid characters.",
                    nameof(directory));
            }

            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new ArgumentException($"File name \"{fileName}\" contains invalid characters.", nameof(fileName));
            }

            Directory = directory;
            FileName = fileName;
        }
    }
}