﻿using FluentAssertions;
using Mariowski.Common.DataTypes;
using Mariowski.Common.Exceptions;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
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

        [Theory]
        [InlineData("joe@doe.com", "joe@doe.com", true)]
        [InlineData("joe@doe.com", "doe@joe.com", false)]
        public void Equals_ShouldDetermineEqualityWithOtherEmail(string value, string value2, bool expected)
        {
            var email = new Email(value);
            var email2 = new Email(value2);

            bool areEqual = email.Equals(email2);

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("joe@doe.com", "joe@doe.com")]
        [InlineData("VALID_mail@gmail.com", "valid_mail@gmail.com")]
        [InlineData("plus+trick@GMAIL.com", "plus+trick@gmail.com")]
        public void ToString_ShouldReturnEmailAsString(string value, string expected)
        {
            var email = new Email(value);

            var toString = email.ToString();

            toString.Should().Be(expected);
        }

        [Theory]
        [InlineData("asdf@qwer.com")]
        [InlineData("qwer@asdf.com")]
        [InlineData("test@test.test")]
        public void GetHashCode_ShouldReturnCalculatedHashCode(string value)
        {
            var email = new Email(value);
            int expected = value.GetHashCode();

            int hashCode = email.GetHashCode();

            hashCode.Should().Be(expected);
        }
    }
}