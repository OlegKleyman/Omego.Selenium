namespace Omego.Selenium
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Represents image targets information.
    /// </summary>
    public class ImageTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTarget"/> class.
        /// </summary>
        /// <param name="directory">The directory to create in.</param>
        /// <param name="fileName">The file name of the image.</param>
        /// <param name="format">The <see cref="ImageFormat"/>.</param>
        public ImageTarget(string directory, string fileName, ImageFormat format)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (fileName.Length == 0) throw new ArgumentException("Value cannot be empty.", nameof(fileName));

            if (directory.IndexOfAny(Path.GetInvalidPathChars()) > -1)
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
            Format = format;
        }

        /// <summary>
        /// Gets <see cref="Directory"/>.
        /// </summary>
        /// <value>Gets the directory of the image.</value>
        public string Directory { get; }

        /// <summary>
        /// Gets <see cref="FileName"/>.
        /// </summary>
        /// <value>Gets the file name of the image.</value>
        public string FileName { get; }

        /// <summary>
        /// Gets <see cref="Format"/>.
        /// </summary>
        /// <value>Gets or sets the format of the image.</value>
        public ImageFormat Format { get; set; }

        /// <summary>
        /// Gets <see cref="CombinedPath"/>
        /// </summary>
        /// <value>Gets the combined directory and file name path,</value>
        public string CombinedPath => Path.Combine(Directory, FileName);
    }
}