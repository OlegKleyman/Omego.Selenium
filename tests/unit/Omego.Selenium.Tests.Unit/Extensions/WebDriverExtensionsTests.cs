namespace Omego.Selenium.Tests.Unit.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    using FluentAssertions;

    using Selenium.Extensions;

    using OpenQA.Selenium;

    using Xunit;

    public class WebDriverExtensionsTests
    {
        [CLSCompliant(false)]
        [Theory]
        [MemberData(
                nameof(WebDriverExtensionsTestData.SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNullCases), null,
            MemberType = typeof(WebDriverExtensionsTestData))]
        public void SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNull(IWebDriver driver)
        {
            Action saveScreenshotAs = () => driver.SaveScreenshotAs("", "", ImageFormat.Bmp);

            saveScreenshotAs.ShouldThrowExactly<ArgumentNullException>().And.ParamName.ShouldBeEquivalentTo("driver");
        }

        private class WebDriverExtensionsTestData
        {
            public static IEnumerable<object[]>
                SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNullCases
                => new List<object[]> { new object[] { null } };
        }
    }
}
