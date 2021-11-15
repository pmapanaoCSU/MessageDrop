using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MessageDrop.API.Models;

namespace MessageDrop.API.Data
{
    public class MessageDropAPIContext : DbContext
    {
        public MessageDropAPIContext (DbContextOptions<MessageDropAPIContext> options)
            : base(options)
        {

        }

        public DbSet<MessageDrop.API.Models.Message> Message { get; set; }
    }
}
