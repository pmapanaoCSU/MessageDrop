using System.ComponentModel.DataAnnotations;

namespace MessageDrop.EF.Model
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MessageString { get; set; }

        public Message(int id, string messageString)
        {
            this.Id = id;
            this.MessageString = messageString;
        }

        public Message(string messageString)
        {
            this.MessageString = messageString;
        }

    }
}
