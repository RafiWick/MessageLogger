using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger
{
    public class MessageManager
    {
        List<User> Users;
        User ActiveUser;
        bool LoggedIn;
        public MessageManager()
        {
            Users = new List<User>();
        }
        public void RunApp()
        {
            bool run = true;
            while (run)
            {
                LogIn();
                while (LoggedIn)
                {
                    string message = GetMessage();
                    Console.WriteLine();
                    if (message.ToLower() == "quit" || message.ToLower() == "log out")
                    {
                        if (message.ToLower() == "quit") run = false;
                        LoggedIn = false;
                    }
                    else
                    {
                        CreateMessage(message);
                    }
                }
            }
            Summerize();
        }
        
        public void LogIn()
        {
            bool newUser = IsNewUser();
            if (newUser) CreateUser();
            else SetUser();
            Console.WriteLine("To log out of your user profile, enter 'log out'. to quit the application enter 'quit'");
        }

        public void CreateUser()
        {
            Console.WriteLine("Let's create a user profile for you.");
            Console.Write("What is your name? ");
            string name = Console.ReadLine();
            Console.Write("What is your username? ");
            string username = Console.ReadLine().Trim().ToLower();
            Users.Add(new User(name, username));
            ActiveUser = FindUser(username);
            LoggedIn = true;
        }
        public void SetUser()
        {
            Console.Write("What is your username? ");
            string username = Console.ReadLine();
            ActiveUser = FindUser(username);
            if (ActiveUser != null)
            {
                LoggedIn = true;
            }
        }
        public bool IsNewUser()
        {
            bool newUser = true;
            if (Users.Count() != 0)
            {
                Console.Write("Would you like to log into a 'new' or 'existing' user? ");
                if (Console.ReadLine().Trim().ToLower() == "existing")
                {
                    newUser = false;
                }
            }
            return newUser;
        }

        public User FindUser(string username)
        {
            User returnUser = null;
            foreach (User user in Users)
            {
                if (username == user.Username)
                {
                    returnUser = user;
                }
            }
            return returnUser;
        }

        public string GetMessage()
        {
            Console.Write("Add a message: ");
            return (Console.ReadLine());
        }
        public void CreateMessage(string message)
        {
            ActiveUser.CreateMessage(message);
            PrintMessages(ActiveUser.Messages);
        }

        public void Summerize()
        {
            Console.WriteLine("Thanks for using Message Logger!");
            foreach (User user in Users)
            {
                Console.WriteLine($"{user.Name} wrote {user.Messages.Count()} messages.");
            }
            ShowMessages();
        }
        public void PrintMessages(List<Message> messages)
        {
            foreach (Message message in messages)
            {
                Console.WriteLine($"{message.Author.Name} {message.CreatedAt.ToShortTimeString()}: {message.Content}");
            }
        }
        public List<Message> AllMessages()
        {
            var list = new List<Message>();
            foreach (User user in Users)
            {
                foreach (Message message in user.Messages)
                {
                    list.Add(message);
                }
            }
            var returnList = SortByCreatedAt(list);
            return returnList;
        }
        public List<Message> SortByCreatedAt(List<Message> list)
        {
            return list.OrderBy(x => x.CreatedAt).ToList();
        }
        public List<Message> RecentMessages(int num)
        {
            var returnList = new List<Message>();
            foreach (User user in Users)
            {
                for (int i = 0; i < num; i++)
                {
                    if (i + 1 > user.Messages.Count) break;

                    returnList.Add(user.Messages[i]);
                }
            }
            return returnList;
        }
        public void ShowMessages()
        {
            Console.WriteLine("How many messages would you like to see");
            Console.WriteLine("enter a number or anything else to see all messages");
            string input = Console.ReadLine();
            int num = 0;
            bool isNum = int.TryParse(input, out num);
            if (isNum)
            {
                PrintMessages(RecentMessages(num));
            }
            else
            {
                PrintMessages(AllMessages());
            }

        }
    }
}
