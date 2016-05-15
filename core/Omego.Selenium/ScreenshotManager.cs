namespace Omego.Selenium
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public class ScreenshotManager : IScreenshotManager
    {
        private readonly IWebDriver driver;

        public ScreenshotManager(IWebDriver driver)
        {
            this.driver = driver;
        }

        public WebScreenshot GetScreenshot()
        {
            var screenshot = driver.TakeScreenshot();
            return new WebScreenshot(screenshot);
        }
    }
}