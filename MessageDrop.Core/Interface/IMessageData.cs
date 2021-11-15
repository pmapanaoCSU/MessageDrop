﻿using MessageDrop.Core.Model;

namespace MessageDrop.Core.Interface
{
    public interface IMessageData
    {
        Task<IEnumerable<Message>> GetAll();
        Task<Message> Get(int id);
    }
}