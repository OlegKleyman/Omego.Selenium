using OpenQA.Selenium;

namespace Omego.Selenium
{
    using System;

    public class WebScreenshot
    {
        private readonly Screenshot screenshot;

        public WebScreenshot(Screenshot screenshot)
        {
            if (screenshot == null) throw new ArgumentNullException(nameof(screenshot));

            this.screenshot = screenshot;
        }

        public byte[] AsByteArray => screenshot.AsByteArray;
    }
}