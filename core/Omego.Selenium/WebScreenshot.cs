using OpenQA.Selenium;

namespace Omego.Selenium
{
    public class WebScreenshot
    {
        private readonly Screenshot screenshot;

        public WebScreenshot(Screenshot screenshot)
        {
            this.screenshot = screenshot;
        }

        public byte[] AsByteArray => screenshot.AsByteArray;
    }
}