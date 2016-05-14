namespace Omego.Selenium.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    using FluentAssertions;

    using Xunit;

    public class ImageTargetTests
    {
        [CLSCompliant(false)]
        [Theory]
        [MemberData(nameof(ImageTargetTestData.ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases), null,
            MemberType = typeof(ImageTargetTestData))]
        public void ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            string directory,
            string fileName,
            string expectedMessage,
            string expectedParameterName,
            Type expectedException)
        {
            Action constructor = () => new ImageTarget(directory, fileName, default(ImageFormat));

            constructor.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedException);
        }

        [Fact]
        public void ConstructorShouldConstructObject()
        {
            var directoryName = "test";
            var fileName = "test.bmp";
            var imageFormat = ImageFormat.Gif;

            var target = new ImageTarget(directoryName, fileName, imageFormat);

            target.Directory.ShouldBeEquivalentTo(directoryName);
            target.FileName.ShouldBeEquivalentTo(fileName);
            target.Format.ShouldBeEquivalentTo(imageFormat);
        }

        private class ImageTargetTestData
        {
            public static IEnumerable<object[]> ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases
                =>
                    new List<object[]>
                        {
                            new object[]
                                {
                                    null, "test.bmp", "Value cannot be null.\r\nParameter name: directory",
                                    "directory", typeof(ArgumentNullException)
                                },
                            new object[]
                                {
                                    ".", null, "Value cannot be null.\r\nParameter name: fileName",
                                    "fileName", typeof(ArgumentNullException)
                                },
                            new object[]
                                {
                                    ".", "", "Value cannot be empty.\r\nParameter name: fileName",
                                    "fileName", typeof(ArgumentException)
                                },
                            new object[]
                                {
                                    @"\", "test.bmp", @"Directory ""\"" contains invalid characters.\r\nParameter name: directory",
                                    "directory", typeof(ArgumentException)
                                },
                            new object[]
                                {
                                    "", @"\", @"File name ""\"" contains invalid characters.\r\nParameter name: fileName",
                                    "fileName", typeof(ArgumentException)
                                }
                        };
        }
    }
}
