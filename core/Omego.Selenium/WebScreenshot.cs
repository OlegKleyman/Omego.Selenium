namespace Omego.Selenium
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.IO.Abstractions;

    using OpenQA.Selenium;

    public class WebScreenshot
    {
        private readonly Screenshot screenshot;

        public WebScreenshot(Screenshot screenshot)
        {
            if (screenshot == null) throw new ArgumentNullException(nameof(screenshot));

            this.screenshot = screenshot;
        }

        public byte[] AsByteArray => screenshot.AsByteArray;

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