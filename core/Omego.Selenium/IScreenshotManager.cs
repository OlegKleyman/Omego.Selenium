namespace Omego.Selenium
{
    using System;

    /// <summary>
    /// Represents a screenshot manager.
    /// </summary>
    public interface IScreenshotManager
    {
        /// <summary>
        /// Gets a <see cref="WebScreenshot"/>.
        /// </summary>
        /// <param name="timeLimit">The time limit to get the screenshot.</param>
        /// <returns>A <see cref="WebScreenshot"/> object.</returns>
        /// <exception cref="TimeoutException">
        /// Thrown when screenshot is unable to be retrieved within the specified time limit.
        /// </exception>
        WebScreenshot GetScreenshot(int timeLimit);
    }
}