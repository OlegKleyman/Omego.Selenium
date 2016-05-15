namespace Omego.Selenium.Tests.Unit.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    using FluentAssertions;

    using NSubstitute;

    using Omego.Selenium.Extensions;

    using OpenQA.Selenium;

    using Xunit;

    public class WebDriverExtensionsTests
    {
        [CLSCompliant(false)]
        [Theory]
        [MemberData(
            nameof(
                WebDriverExtensionsTestData
                    .SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNullCases), null,
            MemberType = typeof(WebDriverExtensionsTestData))]
        public void SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNull(IWebDriver driver)
        {
            Action saveScreenshotAs = () => driver.SaveScreenshotAs(1, new ImageTarget("", "file", ImageFormat.Bmp));

            saveScreenshotAs.ShouldThrowExactly<ArgumentNullException>().And.ParamName.ShouldBeEquivalentTo("driver");
        }

        private class WebDriverExtensionsTestData
        {
            public static IEnumerable<object[]>
                SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNullCases
                => new List<object[]> { new object[] { null } };
        }

        public interface IMockWebDriver : IWebDriver, ITakesScreenshot
        {
        }

        [Fact]
        public void SaveScreenshotAsShouldThrowExceptionWhenTimeLimitIsReached()
        {
            var driver = Substitute.For<IMockWebDriver>();
            driver.GetScreenshot().Returns(new Screenshot(""));

            Action saveScreenshotAs =
                () => driver.SaveScreenshotAs(500, new ImageTarget("", "test.bmp", ImageFormat.Bmp));

            saveScreenshotAs.ShouldThrow<TimeoutException>()
                .Which.Message.Should()
                .MatchRegex(@"Unable to get screenshot after trying for 50\dms.");
        }
    }
}