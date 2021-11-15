using MessageDrop.EF.Model;

namespace MessageDrop.Core.Interface
{
    public interface IMessageData
    {
        Task<IEnumerable<Message>> GetAll();
        Task<Message> Get(int id);

        Task<bool> Insert(Message message);

        Task<bool> Save();
    }
}
