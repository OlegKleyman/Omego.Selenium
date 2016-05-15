namespace Omego.Selenium.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.IO.Abstractions.TestingHelpers;

    using FluentAssertions;

    using NSubstitute;

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
            var screenshot =
                new WebScreenshot(
                    new Screenshot(
                        "iVBORw0KGgoAAAANSUhEUgAAAAEAAAAECAIAAADAusJtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8Y"
                        + "QUAAAAJcEhZcwAAEnQAABJ0Ad5mH3gAAAAQSURBVBhXY/j//z8C//8PAF+wC/WcqJPUAAAAAElFTkSuQmCC"));

            var fileSystem = new MockFileSystem();
            var imageTarget = new ImageTarget("test", "test.bmp", ImageFormat.Bmp);

            screenshot.SaveTo(fileSystem, imageTarget);

            fileSystem.File.Exists(imageTarget.CombinedPath).Should().BeTrue();
        }

        public static class WebScreenshotTestData
        {
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
