namespace Omego.Selenium.Tests.Integration.Features
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.IO.Abstractions;

    using FluentAssertions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    using Xbehave;

    public class SeleniumExtensions
    {
        private IWebDriver driver;

        [Background]
        public void Background()
        {
            "Given I have an IWebDriver object"._(() => driver = new FirefoxDriver()).Teardown(() => driver.Dispose());

            "And it's full screen"._(() => this.driver.Manage().Window.Maximize());
        }

        [Scenario]
        [Example(@".\screenshots", "screen.png")]
        [CLSCompliant(false)]
        public void TakeScreenShot(string directoryPath, string fileName)
        {
            var pathToFile = Path.Combine(directoryPath, fileName);

            "Given I used a web driver to go to a website"._(
                () => driver.Navigate().GoToUrl("http://www.thedailywtf.com"));
            $"When I call the SaveTo WebScreenshot method"._(
                () =>
                new ScreenshotManager(driver).GetScreenshot(5000)
                    .SaveTo(new FileSystem(), new ImageTarget(directoryPath, fileName, ImageFormat.Bmp))).Teardown(
                        () =>
                            {
                                if (Directory.Exists(directoryPath))
                                {
                                    Directory.Delete(directoryPath, true);
                                }
                            });
            "Then the screenshot should be saved"._(
                () => File.Exists(pathToFile).Should().BeTrue("The screenshot needs to exist"));
        }
    }
}