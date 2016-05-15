namespace Omego.Selenium.Tests.Unit
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using NSubstitute;

    using OpenQA.Selenium;

    using Xunit;

    public class ScreenshotManagerTests
    {
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

        [CLSCompliant(false)]
        [Theory]
        [MemberData(
            nameof(ScreenshotManagerTestData.ConstructorShouldThrowArgumentExceptionWhenRequiredArgumentsNotValidCases),
            null, MemberType = typeof(ScreenshotManagerTestData))]
        public void ConstructorShouldThrowArgumentExceptionWhenRequiredArgumentsNotValid(
            IWebDriver driver,
            string expectedParameterName,
            string expectedMessage,
            Type expectedException)
        {
            Action constructor = () => new ScreenshotManager(null);

            constructor.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedException);
        }

        private IScreenshotManager GetScreenshotManager(IWebDriver driver)
        {
            return new ScreenshotManager(driver);
        }

        public interface IMockWebDriver : IWebDriver, ITakesScreenshot
        {
        }

        private static class ScreenshotManagerTestData
        {
            public static IEnumerable<object[]> SaveScreenshotAsShouldThrowExceptionWhenTimeLimitIsReachedCases
                => new List<object[]> { new object[] { new Screenshot(string.Empty) }, new object[] { null } };

            public static IEnumerable<object[]>
                ConstructorShouldThrowArgumentExceptionWhenRequiredArgumentsNotValidCases
                =>
                    new List<object[]>
                        {
                            new object[]
                                {
                                    null, "driver", "Value cannot be null.\r\nParameter name: driver",
                                    typeof(ArgumentNullException)
                                }
                        };
        }

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
    }
}