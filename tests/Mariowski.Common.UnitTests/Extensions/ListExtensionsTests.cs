using System;
using System.Collections.Generic;
using FluentAssertions;
using Mariowski.Common.Extensions;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class ListExtensionsTests
    {
        [Fact]
        public void Shuffle_ShouldShuffleList()
        {
            var originalList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var list = new List<int>(originalList);

            list.Shuffle();

            list.Should().NotEqual(originalList, "chance of getting the same order should be small enough.");
        }

        [Fact]
        public void Shuffle_ShouldShuffleTwoElementsList()
        {
            var random = new Random(1);
            var originalList = new List<int> { 0, 1 };
            var list = new List<int>(originalList);

            list.Shuffle(random);

            list.Should().NotEqual(originalList);
        }
    }
}