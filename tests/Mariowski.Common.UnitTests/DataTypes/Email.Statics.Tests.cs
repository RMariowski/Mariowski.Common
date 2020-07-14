using FluentAssertions;
using Mariowski.Common.DataTypes;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
{
    public partial class EmailTests
    {
        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_mail@gmail.com")]
        [InlineData("plus+trick@gmail.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void IsValid_ShouldDetectValidMailAddresses(string value)
        {
            bool isValid = Email.IsValid(value);

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
            bool isValid = Email.IsValid(value);

            isValid.Should().BeFalse("is an invalid mail address");
        }

        [Theory]
        [InlineData("abc@def.com", "abc@def.com", true)]
        [InlineData("cba@def.com", "abc@def.com", false)]
        [InlineData("abc@def.com", "cba@def.com", false)]
        public void EqualityOperator_ShouldDetermineEquality(string value, string value2, bool expected)
        {
            var email = new Email(value);
            var email2 = new Email(value2);

            bool areEqual = email == email2;

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("abc@def.com", "abc@def.com", false)]
        [InlineData("cba@def.com", "abc@def.com", true)]
        [InlineData("abc@def.com", "cba@def.com", true)]
        public void InequalityOperator_ShouldDetermineInequality(string value, string value2, bool expected)
        {
            var email = new Email(value);
            var email2 = new Email(value2);

            bool areEqual = email != email2;

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_mail@gmail.COM")]
        [InlineData("plus+TRICK@gmail.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void OperatorEmail_ShouldConvertValidEmailStringToEmailObject(string value)
        {
            Email email = value;

            string emailAsString = email;

            emailAsString.Should().Be(value.ToLowerInvariant());
        }

        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_mail@gmail.COM")]
        [InlineData("plus+TRICK@gmail.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void OperatorString_ShouldConvertEmailToString(string value)
        {
            var email = new Email(value);

            string emailAsString = email;

            emailAsString.Should().Be(value.ToLowerInvariant());
        }
    }
}