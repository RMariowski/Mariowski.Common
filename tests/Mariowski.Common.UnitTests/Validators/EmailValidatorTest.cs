using FluentAssertions;
using Mariowski.Common.Validators;
using Xunit;

namespace Mariowski.Common.UnitTests.Validators
{
    public class EmailValidatorTest
    {
        #region Valid

        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_email@gmail.com")]
        [InlineData("plus+trick@gmail.com")]
        [InlineData("super.long.email.address@that.is.valid")]
        public void EmailValidator_IsValid_DetectValidEmails(string value)
        {
            EmailValidator.IsValid(value).Should().BeTrue("is a valid e-mail address");
        }

        #endregion

        #region Invalid

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("fake_email_address")]
        [InlineData("fake_email_address@")]
        [InlineData("fake_email_address@.")]
        [InlineData("fake_email_address@biz")]
        [InlineData("fake_email_address@.dev")]
        [InlineData("fake_emai,l_address@ea.it")]
        [InlineData("fake_email_address@.pl.net")]
        [InlineData("fake_email_address@sd@.org")]
        [InlineData("!#$%^&**[]@sd.com")]
        [InlineData("óąćżź@sd.com")]
        public void EmailValidator_IsValid_DetectInvalidEmails(string value)
        {
            EmailValidator.IsValid(value).Should().BeFalse("is a invalid e-mail address");
        }

        #endregion
    }
}
