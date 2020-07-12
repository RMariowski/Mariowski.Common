using FluentAssertions;
using Mariowski.Common.DataTypes;
using Mariowski.Common.Exceptions;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
{
    public class EmailTests
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
        public void Ctor_ShouldThrowInvalidEmailException_WhenValueIsNotEmail(string value)
        {
            void Act() => _ = new Email(value);

            Assert.Throws<InvalidEmailException>(Act);
        }

        [Theory]
        [InlineData("joe@doe.com")]
        [InlineData("valid_mail@gmail.COM")]
        [InlineData("plus+TRICK@gmail.com")]
        [InlineData("super.long.mail.address@that.is.valid")]
        public void OperatorPercent_ShouldConvertValidByteValueToPercent(string value)
        {
            Email email = value;

            ((string)email).Should().Be(value.ToLowerInvariant());
        }

        [Fact]
        public void OperatorString_ShouldConvertEmailToString()
        {
            const string value = "qwer@asdf.com";
            var email = new Email(value);

            string emailAsString = email;

            emailAsString.Should().Be(value);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenBothEmailsAreTheSame()
        {
            const string value = "asdf@qwer.com";
            var email = new Email(value);
            var email2 = new Email(value);

            bool areEqual = email.Equals(email2);

            areEqual.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenBothEmailsAreNotTheSame()
        {
            var email = new Email("asdf@qwer.com");
            var email2 = new Email("qwer@asdf.com");

            bool areEqual = email.Equals(email2);

            areEqual.Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenEmailAndObjectAreTheSame()
        {
            const string value = "asdf@qwer.com";
            var email = new Email(value);
            var email2 = (object)new Email(value);

            bool areEqual = email.Equals(email2);

            areEqual.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenEmailAndObjectAreNotTheSame()
        {
            var email = new Email("asdf@qwer.com");
            var email2 = (object)new Email("qwer@asdf.com");

            bool areEqual = email.Equals(email2);

            areEqual.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCodeAsString()
        {
            const string value = "asdf@qwer.com";
            var email = new Email(value);

            int hashCode = email.GetHashCode();

            hashCode.Should().Be(value.GetHashCode());
        }
        
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

            isValid.Should().BeFalse("is a invalid mail address");
        }
    }
}