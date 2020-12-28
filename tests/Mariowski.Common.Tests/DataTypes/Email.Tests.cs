using FluentAssertions;
using Mariowski.Common.DataTypes;
using Mariowski.Common.Exceptions;
using Xunit;

namespace Mariowski.Common.Tests.DataTypes
{
    public partial class EmailTests
    {
        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("VALID_mail@gmail.com")]
        [InlineData("plus+trick@GMAIL.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void Ctor_ShouldAcceptValidEmail(string value)
        {
            var email = new Email(value);

            email.Should().NotBeNull();
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
        public void Ctor_ShouldThrowInvalidEmailException_WhenValueIsNotValidEmail(string value)
        {
            void Act() => _ = new Email(value);

            Assert.Throws<InvalidEmailException>(Act);
        }
    }
}