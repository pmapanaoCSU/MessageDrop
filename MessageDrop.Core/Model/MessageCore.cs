namespace MessageDrop.Core.Model
{
    public class MessageCore
    {
        public int Id { get; set; }
        public string MessageString { get; set; }


        public MessageCore(int id, string messageString)
        {
            this.Id = id;
            this.MessageString = messageString;
        }

    }
}
