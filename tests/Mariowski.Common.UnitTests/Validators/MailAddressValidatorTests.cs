using FluentAssertions;
using Mariowski.Common.Validators;
using Xunit;

namespace Mariowski.Common.UnitTests.Validators
{
    public class MailAddressValidatorTests
    {
        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_mail@gmail.com")]
        [InlineData("plus+trick@gmail.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void IsValid_ShouldDetectValidMailAddresses(string value)
        {
            bool isValid = MailAddressValidator.IsValid(value);

            isValid.Should().BeTrue("is a valid mail address");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("fake_mail_address")]
        [InlineData("fake_mail_address@")]
        [InlineData("fake_mail_address@.")]
        [InlineData("fake_mail_address@biz")]
        [InlineData("fake_mail_address@.dev")]
        [InlineData("fake_mai,l_address@ea.it")]
        [InlineData("fake_mail_address@.pl.net")]
        [InlineData("fake_mail_address@sd@.org")]
        [InlineData("!#$%^&**[]@sd.com")]
        [InlineData("óąćżź@sd.com")]
        public void IsValid_ShouldDetectInvalidMailAddresses(string value)
        {
            bool isValid = MailAddressValidator.IsValid(value);

            isValid.Should().BeFalse("is a invalid mail address");
        }
    }
}