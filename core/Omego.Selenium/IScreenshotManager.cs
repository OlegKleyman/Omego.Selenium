namespace Omego.Selenium
{
    using System.Drawing.Imaging;

    public interface IScreenshotManager
    {
        WebScreenshot GetScreenshot(int timeLimit);
    }
}