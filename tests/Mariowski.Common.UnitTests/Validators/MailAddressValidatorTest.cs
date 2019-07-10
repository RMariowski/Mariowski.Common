using FluentAssertions;
using Mariowski.Common.Validators;
using Xunit;

namespace Mariowski.Common.UnitTests.Validators
{
    public class MailAddressValidatorTest
    {
        #region Valid

        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_mail@gmail.com")]
        [InlineData("plus+trick@gmail.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void MailAddressValidator_IsValid_DetectValidMailAddresses(string value)
        {
            MailAddressValidator.IsValid(value).Should().BeTrue("is a valid mail address");
        }

        #endregion

        #region Invalid

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
        public void MailAddressValidator_IsValid_DetectInvalidMailAddresses(string value)
        {
            MailAddressValidator.IsValid(value).Should().BeFalse("is a invalid mail address");
        }

        #endregion
    }
}
