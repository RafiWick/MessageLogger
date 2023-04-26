using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace MessageLogger.UnitTests
{
    public class UserTests
    {
        [Fact]
        public void User_Constructor_CreatesUser()
        {
            var testUser = new User("John", "johndoe");

            Assert.Equal("John", testUser.Name);
            Assert.Equal("johndoe", testUser.Username);
            Assert.Equal(new List<Message>(), testUser.Messages);
        }
        [Fact]
        public void User_CreateMessage_CreatesMessageInMessages()
        {
            var testUser = new User("John", "johndoe");
            testUser.CreateMessage("abcdefg");

            var DT = DateTime.Now;

            Assert.Equal("abcdefg", testUser.Messages[0].Content);
            Assert.Equal(new DateTime(DT.Year, DT.Month, DT.Day, DT.Hour, DT.Minute, DT.Second), testUser.Messages[0].CreatedAt);
            Assert.Single(testUser.Messages);
        }
        [Fact]
        public void User_DeleteMessage_RemovesMessageFromMessages()
        {
            var testUser = new User("John", "johndoe");
            testUser.CreateMessage("abcdefg");
            testUser.DeleteMessage(0);
            Assert.Empty(testUser.Messages);
        }
    }
}
