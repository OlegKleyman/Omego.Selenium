namespace Omego.Selenium.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.IO.Abstractions;
    using System.IO.Abstractions.TestingHelpers;

    using FluentAssertions;

    using OpenQA.Selenium;

    using Xunit;

    public class WebScreenshotTests
    {
        [CLSCompliant(false)]
        [Theory]
        [MemberData(nameof(WebScreenshotTestData.ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid),
            MemberType = typeof(WebScreenshotTestData))]
        public void ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            Screenshot screenshot,
            string expectedParameterName,
            string expectedMessage,
            Type expectedException)

        {
            Action constructor = () => new WebScreenshot(screenshot);

            constructor.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedException);
        }

        [Fact]
        public void SaveToShouldSaveToTheFileSystem()
        {
            var screenshot = GetWebScreenshot("iVBORw0KGgoAAAANSUhEUgAAAAEAAAAECAIAAADAusJtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8Y"
                                              + "QUAAAAJcEhZcwAAEnQAABJ0Ad5mH3gAAAAQSURBVBhXY/j//z8C//8PAF+wC/WcqJPUAAAAAElFTkSuQmCC");

            var fileSystem = new MockFileSystem();
            var imageTarget = new ImageTarget("test", "test.bmp", ImageFormat.Bmp);

            var result = screenshot.SaveTo(fileSystem, imageTarget);

            result.Should().NotBeNull();
            result.Exists.ShouldBeEquivalentTo(true);
            result.FullName.ShouldBeEquivalentTo(imageTarget.CombinedPath);
        }

        [Fact]
        public void AsByteArrayShouldReturnTheSerializedImageArray()
        {
            var screenshot = GetWebScreenshot("test");

            screenshot.AsByteArray.ShouldBeEquivalentTo(new byte[] { 181, 235, 45 });
        }

        [CLSCompliant(false)]
        [Theory]
        [MemberData(nameof(WebScreenshotTestData.SaveToShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases),
            MemberType = typeof(WebScreenshotTestData))]
        public void SaveToShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            IFileSystem fileSystem,
            ImageTarget target,
            string expectedParameterName,
            string expectedMessage,
            Type expectedException)
        {
            var webScreenshot = GetWebScreenshot("");

            Action saveTo = () => webScreenshot.SaveTo(fileSystem, target);

            saveTo.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedException);
        }

        private static WebScreenshot GetWebScreenshot(string base64EncodedScreenshot)
        {
            return new WebScreenshot(new Screenshot(base64EncodedScreenshot));
        }
        
        public static class WebScreenshotTestData
        {
            public static IEnumerable<object[]> SaveToShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases
                =>
                    new List<object[]>
                        {
                            new object[]
                                {
                                    null, null, "fileSystem",
                                    "Value cannot be null.\r\nParameter name: fileSystem",
                                    typeof(ArgumentNullException)
                                },
                            new object[]
                                {
                                    new MockFileSystem(), null, "target",
                                    "Value cannot be null.\r\nParameter name: target",
                                    typeof(ArgumentNullException)
                                }
                        };

            public static IEnumerable<object[]> ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid
                =>
                    new List<object[]>
                        {
                            new object[]
                                {
                                    null, "screenshot", "Value cannot be null.\r\nParameter name: screenshot",
                                    typeof(ArgumentNullException)
                                }
                        };
        }
    }
}
