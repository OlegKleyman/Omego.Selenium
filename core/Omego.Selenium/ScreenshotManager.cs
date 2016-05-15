namespace Omego.Selenium
{
    using System;
    using System.Diagnostics;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public class ScreenshotManager : IScreenshotManager
    {
        private readonly IWebDriver driver;

        public ScreenshotManager(IWebDriver driver)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            this.driver = driver;
        }

        public WebScreenshot GetScreenshot(int timeLimit)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Screenshot screenshot;

            const int invalidScreenshotLength = 0;

            do
            {
                screenshot = driver.TakeScreenshot();
            }
            while (stopWatch.ElapsedMilliseconds < timeLimit
                   && ((screenshot == null) || (screenshot.AsByteArray.Length == invalidScreenshotLength)));

            stopWatch.Stop();

            if (screenshot == null || screenshot.AsByteArray.Length == invalidScreenshotLength)
            {
                throw new TimeoutException(
                    $"Unable to get screenshot after trying for {stopWatch.ElapsedMilliseconds}ms.");
            }

            return new WebScreenshot(screenshot);
        }
    }
}