namespace Omego.Selenium.Tests.Integration.Features
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;

    using FluentAssertions;

    using Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    using Xbehave;
    
    public class SeleniumExtensions
    {
        private IWebDriver driver;

        [Background]
        public void Background()
        {
            "Given I have an IWebDriver object"._(() => driver = new FirefoxDriver())
                .Teardown(() => this.driver.Dispose());
        }

        [Scenario]
        [Example(@".\screenshots", "screen.png")]
        [CLSCompliant(false)]
        public void TakeScreenShot(string directoryPath, string fileName)
        {
            var pathToFile = Path.Combine(directoryPath, fileName);

            "Given I used a web driver to go to a website"._(() => driver.Navigate().GoToUrl("http://www.google.com"))
                .Teardown(() => driver.Dispose());
            $"When I call the SaveScreenShotAs in {pathToFile} extension method"._(
                () => driver.SaveScreenshotAs(3000, new ImageTarget(directoryPath, fileName, ImageFormat.Png)));
            "Then the screenshot should be saved"._(
                () => File.Exists(pathToFile).Should().BeTrue("The screenshot needs to exist"));
        }
    }
}
