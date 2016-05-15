namespace Omego.Selenium
{
    using System;
    using System.Diagnostics;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    /// <summary>
    /// Represents a screenshot manager.
    /// </summary>
    public class ScreenshotManager : IScreenshotManager
    {
        private readonly IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotManager"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> object to use to retrieve the screenshot.</param>
        public ScreenshotManager(IWebDriver driver)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            this.driver = driver;
        }

        /// <summary>
        /// Gets a <see cref="WebScreenshot"/>.
        /// </summary>
        /// <param name="timeLimit">The time limit to get the screenshot.</param>
        /// <returns>A <see cref="WebScreenshot"/> object.</returns>
        /// <exception cref="TimeoutException">
        /// Thrown when screenshot is unable to be retrieved within the specified time limit.
        /// </exception>
        /// <remarks>
        /// This method will attempt to retrieve the screenshot at least once irrelevant
        /// of the time limit specified. Additionally, the time limit is not a gaurantee
        /// if an internal call to get the screenshot takes longer than the time limit
        /// then the total time will exceed the time limit.
        /// </remarks>
        public WebScreenshot GetScreenshot(int timeLimit)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Screenshot screenshot;
            const int invalidScreenshotLength = 0;

            Func<Screenshot, bool> isNotRetrieved =
                (screen) => (screen == null) || (screen.AsByteArray.Length == invalidScreenshotLength);

            do
            {
                screenshot = driver.TakeScreenshot();
            }
            while (stopWatch.ElapsedMilliseconds < timeLimit && (isNotRetrieved(screenshot)));

            stopWatch.Stop();

            if (isNotRetrieved(screenshot))
            {
                throw new TimeoutException(
                    $"Unable to get screenshot after trying for {stopWatch.ElapsedMilliseconds}ms.");
            }

            return new WebScreenshot(screenshot);
        }
    }
}