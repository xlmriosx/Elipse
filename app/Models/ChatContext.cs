using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Elipse.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }

        public DbSet<Chat> Chat { get; set; } = null!;
    }
}
