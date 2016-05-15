using OpenQA.Selenium;

namespace Omego.Selenium
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.IO.Abstractions;

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
                
                screenshotImage.Save(fileSystem.File.Create(target.CombinedPath), target.Format);
            }

            return fileSystem.FileInfo.FromFileName(target.CombinedPath);
        }
    }
}