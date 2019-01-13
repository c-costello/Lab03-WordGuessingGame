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
  
        [Fact]
        public void CanUpdateFile()
        {
            string input = "WORD";
            string[] expected = { "DOG", "CAT", "SILLY", "FUNNY", "WORD" };
            Assert.Equal(expected, Program.AppendToFile(Program.wordPath, input));
        }
    }
}
