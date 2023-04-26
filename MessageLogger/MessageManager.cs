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
        public List<User> Users;
        public User ActiveUser;
        public bool LoggedIn;
        public MessageManager()
        {
            Users = new List<User>();
        }

        // Main program
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
        

        // sets ActiveUser
        public void LogIn()
        {
            bool newUser = IsNewUser();
            if (newUser) CreateUser();
            else SetUser();
            Console.WriteLine();
            Console.WriteLine("To log out of your user profile, enter 'log out'. to quit the application enter 'quit'");
        }

        // creates a new user and sets it to active user
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

        // takes username from user input and finds the user object with that username and sets it to active user
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

        // Deturmines if a new user needs to becreated
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

        // takes the username andreturns the user with that username
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


        // propmts user to add message and returns the string they typed
        public string GetMessage()
        {
            Console.Write("Add a message: ");
            return (Console.ReadLine());
        }

        // takes the string and creates a message in the active user
        public void CreateMessage(string message)
        {
            ActiveUser.CreateMessage(message);
            PrintMessages(ActiveUser.Messages);
        }

        // prints each message in the list given to it with the author and the time of creation
        public void PrintMessages(List<Message> messages)
        {
            foreach (Message message in messages)
            {
                Console.WriteLine($"{message.Author.Name} {message.CreatedAt.ToShortTimeString()}: {message.Content}");
            }
        }


        // gives a summary of the messages posted and calls for the ShowMessages method
        public void Summerize()
        {
            Console.WriteLine("Thanks for using Message Logger!");
            Console.WriteLine();

            foreach (User user in Users)
            {
                Console.WriteLine($"{user.Name} wrote {user.Messages.Count()} messages.");
            }
            Console.WriteLine();
            ShowMessages();
        }

        // prompts the user to say how many messages they'd like to see and calls the appropriate method to do that
        public void ShowMessages()
        {
            Console.WriteLine("How many messages would you like to see");
            Console.WriteLine("enter a number to get that many of the most recent messages");
            Console.WriteLine("enter 'search' to search messages for a keyword");
            Console.WriteLine("enter anything else to see all messages");
            string input = Console.ReadLine();
            Console.WriteLine();
            int num = 0;
            bool isNum = int.TryParse(input, out num);
            if (isNum)
            {
                PrintMessages(RecentMessages(num));
            }
            else if (input.ToLower().Trim() == "search")
            {
                Console.WriteLine("enter the keyword you'd like to search for");
                string keyword = Console.ReadLine();
                PrintMessages(SearchMessages(keyword));
            }
            else
            {
                PrintMessages(AllMessages());
            }

        }

        // gets the full list of all messages and runs it through the sort method
        // to return the messages sorted by when they were created
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

        // takes a list of messages and sorts them by the time they were created
        public List<Message> SortByCreatedAt(List<Message> list)
        {
            var returnList = new List<Message>();
            while (list.Count > 0)
            {
                Message next = list[0];
                foreach (Message message in list)
                {
                    var test = DateTime.Compare(message.CreatedAt, next.CreatedAt);
                    if (DateTime.Compare(message.CreatedAt, next.CreatedAt) < 0) next = message;
                }
                returnList.Add(next);
                list.Remove(next);
            }


            return returnList;
        }

        // returns the last num messages, is only called if num is less than the total number of messages
        public List<Message> RecentMessages(int num)
        {
            var list = AllMessages();
            var returnList = new List<Message>();
            int count = list.Count - 1;
            for (int i = count; i > count - num; i--)
            {
                returnList.Add(list[i]);
            }

            return returnList;
        }

        // returns a list of all messages that contain the keyword or string given to it
        public List<Message> SearchMessages(string keyword)
        {
            var returnList = new List<Message>();
            var fullList = AllMessages();
            foreach (Message message in fullList)
            {
                if (message.Content.Contains(keyword))
                {
                    returnList.Add(message);
                }
            }

            return returnList;
        }
    }
}
