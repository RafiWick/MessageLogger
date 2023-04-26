using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageLogger.UnitTests
{
    public class MessageManagerTests
    {
        [Fact]
        public void MessageManager_Constructor_MakesEmptyListUsers()
        {
            var testManager = new MessageManager();

            Assert.Equal(new List<User>(), testManager.Users);
        }
        [Fact]
        public void MessageManager_FindUser_GetsUserFromUsers()
        {
            var testManager = new MessageManager();
            testManager.Users.Add(new User("testname1", "username"));
            testManager.Users.Add(new User("testname2", "search"));

            Assert.Equal("testname1", testManager.FindUser("username").Name);
            Assert.Equal("testname2", testManager.FindUser("search").Name);
        }
        [Fact]
        public void MessageManager_CreateMessage_CreatesMessage()
        {
            var testManager = new MessageManager();
            testManager.Users.Add(new User("testname1", "username"));
            testManager.ActiveUser = testManager.FindUser("username");
            testManager.CreateMessage("test message");
            Assert.Equal("test message", testManager.FindUser("username").Messages[0].Content);
        }
        [Fact]
        public void MessageManager_AllMessages_ReturnsAllMessages()
        {
            var testManager = new MessageManager();
            testManager.Users.Add(new User("testname1", "username"));
            var message1 = new Message("test1", testManager.FindUser("username"));
            message1.CreatedAt = new DateTime(2023, 4, 25, 10, 0, 0);
            var message2 = new Message("test2", testManager.FindUser("username"));
            message2.CreatedAt = new DateTime(2023, 4, 25, 11, 0, 0);
            var message3 = new Message("test3", testManager.FindUser("username"));
            message3.CreatedAt = new DateTime(2023, 4, 25, 12, 0, 0);
            var message4 = new Message("test4", testManager.FindUser("username"));
            message4.CreatedAt = new DateTime(2023, 4, 25, 13, 0, 0);
            testManager.FindUser("username").Messages.Add(message2);
            testManager.FindUser("username").Messages.Add(message4);
            testManager.FindUser("username").Messages.Add(message3);
            testManager.FindUser("username").Messages.Add(message1);
            var testList = new List<Message>();
            testList.Add(message1);
            testList.Add(message2);
            testList.Add(message3);
            testList.Add(message4);
            Assert.Equal(testList, testManager.AllMessages());
        }
        [Fact]
        public void MessageManager_SortByCreatedAt_SortsListByDateTimeProperty()
        {
            var testManager = new MessageManager();
            testManager.Users.Add(new User("testname1", "username"));
            var message1 = new Message("test1", testManager.FindUser("username"));
            message1.CreatedAt = new DateTime(2023, 4, 25, 10, 0, 0);
            var message2 = new Message("test2", testManager.FindUser("username"));
            message2.CreatedAt = new DateTime(2023, 4, 25, 11, 0, 0);
            var message3 = new Message("test3", testManager.FindUser("username"));
            message3.CreatedAt = new DateTime(2023, 4, 25, 12, 0, 0);
            var message4 = new Message("test4", testManager.FindUser("username"));
            message4.CreatedAt = new DateTime(2023, 4, 25, 13, 0, 0);
            testManager.FindUser("username").Messages.Add(message2);
            testManager.FindUser("username").Messages.Add(message4);
            testManager.FindUser("username").Messages.Add(message3);
            testManager.FindUser("username").Messages.Add(message1);
            var testList = new List<Message>();
            testList.Add(message1);
            testList.Add(message2);
            testList.Add(message3);
            testList.Add(message4);
            Assert.Equal(testList, testManager.SortByCreatedAt(testManager.Users[0].Messages));
        }
        [Fact]
        public void MessageManager_RecentMessages_ReturnsNMostRecentMessages()
        {
            var testManager = new MessageManager();
            testManager.Users.Add(new User("testname1", "username"));
            var message1 = new Message("test1", testManager.FindUser("username"));
            message1.CreatedAt = new DateTime(2023, 4, 25, 10, 0, 0);
            var message2 = new Message("test2", testManager.FindUser("username"));
            message2.CreatedAt = new DateTime(2023, 4, 25, 11, 0, 0);
            var message3 = new Message("test3", testManager.FindUser("username"));
            message3.CreatedAt = new DateTime(2023, 4, 25, 12, 0, 0);
            var message4 = new Message("test4", testManager.FindUser("username"));
            message4.CreatedAt = new DateTime(2023, 4, 25, 13, 0, 0);
            testManager.FindUser("username").Messages.Add(message2);
            testManager.FindUser("username").Messages.Add(message4);
            testManager.FindUser("username").Messages.Add(message3);
            testManager.FindUser("username").Messages.Add(message1);
            var testList = new List<Message>();
            testList.Add(message4);
            testList.Add(message3);
            testList.Add(message2);
            Assert.Equal(testList, testManager.RecentMessages(3));
        }
        [Fact]
        public void MessageManager_SearchMessages_ReturnsAllMessagesContainingKeyWord()
        {
            var testManager = new MessageManager();
            testManager.Users.Add(new User("testname1", "username"));
            var message1 = new Message("test keyword djdj", testManager.FindUser("username"));
            message1.CreatedAt = new DateTime(2023, 4, 25, 10, 0, 0);
            var message2 = new Message("test2 nsnns", testManager.FindUser("username"));
            message2.CreatedAt = new DateTime(2023, 4, 25, 11, 0, 0);
            var message3 = new Message("test3 keyword sakj", testManager.FindUser("username"));
            message3.CreatedAt = new DateTime(2023, 4, 25, 12, 0, 0);
            var message4 = new Message("test4", testManager.FindUser("username"));
            message4.CreatedAt = new DateTime(2023, 4, 25, 13, 0, 0);
            testManager.FindUser("username").Messages.Add(message1);
            testManager.FindUser("username").Messages.Add(message2);
            testManager.FindUser("username").Messages.Add(message3);
            testManager.FindUser("username").Messages.Add(message4);
            var testList = new List<Message>();
            testList.Add(message1);
            testList.Add(message3);
            Assert.Equal(testList, testManager.SearchMessages("keyword"));
        }
    }
}
