namespace MessageDrop.Core.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageString { get; set; }


        public Message(int id, string messageString)
        {
            this.Id = id;
            this.MessageString = messageString;
        }

    }
}
