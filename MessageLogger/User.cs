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
        public bool Blue;
        public List<Message> Messages;

        public User(string name, string username)
        {
            Name = name;
            Username = username;
            Blue = false;
            Messages = new List<Message>();
        }
        public void CreateMessage(string message)
        {
            Messages.Add(new Message(message, this));
        }
        public void DeleteMessage(int index)
        {
            var deletedMessage = Messages[index];
            Messages.Remove(deletedMessage);
        }
        public bool EditMessage(int index)
        {
            bool returnBool = false;
            if (!Blue)
            {
                Console.WriteLine("\nThis is a premium feture please sign up for MessageManager Blue");
                Console.WriteLine("please enter 'y' for yes or 'n' for no");
                string input = "";
                while (input != "y" && input != "n")
                {
                    input = Console.ReadLine().Trim().ToLower();
                    if (input != "y" && input != "n") Console.WriteLine("please only enter 'y' or 'n'");
                }
                if (input == "y")
                {
                    SignUpForBlue();
                }
            }
            if (Blue)
            {
                Console.WriteLine("what would you like it to say instead of");
                Console.WriteLine(Messages[index].Content);
                string input = Console.ReadLine();
                Messages[index].Edit(input);
                returnBool = true;
            }

            return returnBool;
        }
        public void SignUpForBlue()
        {
            Console.WriteLine("That will be $8");
            string input = "";
            while (input != "$8")
            {
                input = Console.ReadLine();
                if (input != "$8") Console.WriteLine("try again");
            }
            Blue = true;
            Console.WriteLine("Congradulations you are now 'verified'");
        }
    }
}
