namespace Omego.Selenium.Tests.Unit
{
    using System.Drawing.Imaging;

    using FluentAssertions;

    using NSubstitute;

    using Omego.Selenium.Tests.Unit.Extensions;

    using OpenQA.Selenium;

    using Xunit;

    public class ScreenshotManagerTests
    {
        [Fact]
        public void GetScreenshotShouldReturnScreenshot()
        {
            var driver = Substitute.For<IMockWebDriver>();
            driver.GetScreenshot().Returns(new Screenshot("test"));

            var manager = GetScreenshotManager(driver);

            var screenshot = manager.GetScreenshot();

            screenshot.Should().NotBeNull();
            screenshot.AsByteArray.ShouldBeEquivalentTo(new byte[] { 181, 235, 45 });
        }

        private IScreenshotManager GetScreenshotManager(IWebDriver driver)
        {
            return new ScreenshotManager(driver);
        }

        public interface IMockWebDriver : IWebDriver, ITakesScreenshot
        {
        }
    }
}
