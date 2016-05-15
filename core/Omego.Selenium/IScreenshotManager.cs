namespace Omego.Selenium
{
    public interface IScreenshotManager
    {
        WebScreenshot GetScreenshot(int timeLimit);
    }
}