namespace Omego.Selenium
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.IO.Abstractions;

    using OpenQA.Selenium;

    /// <summary>
    ///     Represents a web screenshot.
    /// </summary>
    public class WebScreenshot
    {
        private readonly Screenshot screenshot;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebScreenshot" /> class.
        /// </summary>
        /// <param name="screenshot">The <see cref="Screenshot" /> to use as a template.</param>
        public WebScreenshot(Screenshot screenshot)
        {
            if (screenshot == null) throw new ArgumentNullException(nameof(screenshot));

            this.screenshot = screenshot;
        }

        /// <summary>
        ///     Gets <see cref="AsByteArray" />.
        /// </summary>
        /// <value>The byte array representation of the current object.</value>
        public byte[] AsByteArray => screenshot.AsByteArray;

        /// <summary>
        ///     Saves the current object to the <see cref="IFileSystem" /> object.
        /// </summary>
        /// <param name="fileSystem">The <see cref="IFileSystem" /> object to use for operations.</param>
        /// <param name="target">The <see cref="ImageTarget" /> to use when saving this object.</param>
        /// <returns>A <see cref="FileInfoBase" /> object representing the create screenshot file.</returns>
        [CLSCompliant(false)]
        public FileInfoBase SaveTo(IFileSystem fileSystem, ImageTarget target)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));
            if (target == null) throw new ArgumentNullException(nameof(target));

            using (var imageStream = new MemoryStream(screenshot.AsByteArray))
            {
                var screenshotImage = Image.FromStream(imageStream);

                if (!fileSystem.Directory.Exists(target.Directory))
                {
                    fileSystem.Directory.CreateDirectory(target.Directory);
                }

                using (var file = fileSystem.File.Create(target.CombinedPath))
                {
                    screenshotImage.Save(file, target.Format);
                }
            }

            return fileSystem.FileInfo.FromFileName(target.CombinedPath);
        }
    }
}