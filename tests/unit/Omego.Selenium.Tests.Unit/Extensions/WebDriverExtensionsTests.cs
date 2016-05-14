namespace Omego.Selenium.Tests.Unit.Extensions
{
    using System;
    using System.Drawing.Imaging;

    using FluentAssertions;

    using Omego.Selenium.Extensions;

    using OpenQA.Selenium;

    using Xunit;

    public class WebDriverExtensionsTests
    {
        [Fact]
        public void SaveScreenshotAsShouldThrowArgumentNullExceptionWhenRequiredArgumentsAreNull()
        {
            Action saveScreenshotAs = () => ((IWebDriver)null).SaveScreenshotAs("", "", ImageFormat.Bmp);

            saveScreenshotAs.ShouldThrowExactly<ArgumentNullException>().And.ParamName.ShouldBeEquivalentTo("driver");
        }
    }
}
