namespace Omego.Selenium.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Drawing.Imaging;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public static class WebDriverExtensions
    {
        public static void SaveScreenshotAs(this IWebDriver driver, int timeLimit, ImageTarget target)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Screenshot screenshot;

            const int invalidScreenshotLength = 0;

            do
            {
                screenshot = driver.TakeScreenshot();
            }
            while (stopWatch.ElapsedMilliseconds < timeLimit && screenshot?.AsByteArray?.Length == invalidScreenshotLength);

            stopWatch.Stop();

            if (screenshot?.AsByteArray?.Length == invalidScreenshotLength)
            {
                throw new TimeoutException(
                    $"Unable to get screenshot after trying for {stopWatch.ElapsedMilliseconds}ms.");
            }
        }
    }
}
