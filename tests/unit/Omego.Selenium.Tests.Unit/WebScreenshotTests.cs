namespace Omego.Selenium.Tests.Unit
{
    using System;
    using System.Collections.Generic;

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
