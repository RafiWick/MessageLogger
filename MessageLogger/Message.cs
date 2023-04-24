using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger
{
    public class Message
    {
        public string Content;
        public DateTime CreatedAt;
        public User Author;

        public Message(string content, User author)
        {
            Content = content;
            CreatedAt = DateTimeRound(DateTime.Now);
            Author = author;
        }

        public DateTime DateTimeRound(DateTime DT)
        {
            return new DateTime(DT.Year, DT.Month, DT.Day, DT.Hour, DT.Minute, DT.Second);
        }
    }
}
