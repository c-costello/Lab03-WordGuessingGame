using System;
using Xunit;
using GuessingGameApp;

namespace GuessingGameTests
{
    public class UnitTest1
    {
        //Pick Handler Test
        [Fact]
        public void CanReturnOne()
        {
            Assert.Equal(1, Program.PickHandler("1"));
        }
        [Fact]
        public void OnlyTakesIntegers()
        {
            Assert.Equal(0, Program.PickHandler("A string"));
        }
        //
    }
}
