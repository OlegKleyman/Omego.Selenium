namespace Omego.Selenium.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    using FluentAssertions;

    using NSubstitute;

    using Omego.Selenium.Extensions;
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

            var screenshot = manager.GetScreenshot(500);

            screenshot.Should().NotBeNull();
            screenshot.AsByteArray.ShouldBeEquivalentTo(new byte[] { 181, 235, 45 });
        }

        [CLSCompliant(false)]
        [Theory]
        [MemberData(nameof(ScreenshotManagerTestData.SaveScreenshotAsShouldThrowExceptionWhenTimeLimitIsReachedCases),
            null, MemberType = typeof(ScreenshotManagerTestData))]
        public void SaveScreenshotAsShouldThrowExceptionWhenTimeLimitIsReached(Screenshot screenshot)
        {
            var driver = Substitute.For<IMockWebDriver>();
            driver.GetScreenshot().Returns(screenshot);

            var manager = GetScreenshotManager(driver);

            Action getScreenshot = () => manager.GetScreenshot(500);

            getScreenshot.ShouldThrow<TimeoutException>()
                .Which.Message.Should()
                .MatchRegex(@"Unable to get screenshot after trying for 50\dms.");
        }

        private IScreenshotManager GetScreenshotManager(IWebDriver driver)
        {
            return new ScreenshotManager(driver);
        }


        public interface IMockWebDriver : IWebDriver, ITakesScreenshot
        {
        }

        public static class ScreenshotManagerTestData
        {
            public static IEnumerable<object[]> SaveScreenshotAsShouldThrowExceptionWhenTimeLimitIsReachedCases
                => new List<object[]> { new object[] { new Screenshot(String.Empty) }, new object[] { (Screenshot)null } };

            public static IEnumerable<object[]>
                SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNullCases
                => new List<object[]> { new object[] { null } };
        }
    }
}
