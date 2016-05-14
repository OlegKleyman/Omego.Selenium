﻿namespace Omego.Selenium.Extensions
{
    using System;
    using System.Drawing.Imaging;

    using OpenQA.Selenium;

    public static class WebDriverExtensions
    {
        public static void SaveScreenshotAs(this IWebDriver driver, string directory, string fileName, ImageFormat format)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
        }
    }
}
