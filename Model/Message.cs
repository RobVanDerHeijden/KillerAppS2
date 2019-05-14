using System;

namespace Model
{
    public class Message
    {
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateSend { get; set; }
        public Player PlayerSendTo { get; set; }

        public Message()
        {

        }
        public Message(bool isRead, string title, string body, DateTime dateSend, Player playerSendTo)
        {
            IsRead = isRead;
            Title = title;
            Body = body;
            DateSend = dateSend;
            PlayerSendTo = playerSendTo;
        }
    }
}