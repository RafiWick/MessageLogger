using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger
{
    public class User
    {
        public string Name;
        public string Username;
        public List<Message> Messages;

        public User(string name, string username)
        {
            Name = name;
            Username = username;
            Messages = new List<Message>();
        }
        public void CreateMessage(string message)
        {
            Messages.Add(new Message(message, this));
        }
    }
}
