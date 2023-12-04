using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elipse.Models;
using Elipse.DTO;

namespace Elipse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _context;

        public ChatController(ChatContext context)
        {
            _context = context;
        }

        // GET: api/Chat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatOutputDto>>> GetChatItems()
        {
            var chats = await _context.Chat.ToListAsync();
            var chatOutputDtos = chats.Select(c => new ChatOutputDto
            {
                Id = c.Id,
                Text = c.Text
            }).ToList();

            return chatOutputDtos;
        }

        // GET: api/Chat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatOutputDto>> GetChat(int id)
        {
            var chat = await _context.Chat.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            var chatOutputDto = new ChatOutputDto
            {
                Id = chat.Id,
                Text = chat.Text
            };

            return chatOutputDto;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int id, ChatInputDto chatInput)
        {
            var chat = await _context.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            chat.Text = chatInput.Text;
            // other updates

            _context.Entry(chat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ChatOutputDto>> PostChat(ChatInputDto chatInput)
        {
            var chat = new Chat
            {
                Text = chatInput.Text
            };

            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();

            var chatOutput = new ChatOutputDto
            {
                Id = chat.Id,
                Text = chat.Text
            };

            return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, chatOutput);
        }

        // DELETE: api/Chat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chat = await _context.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chat.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatExists(int id)
        {
            return _context.Chat.Any(e => e.Id == id);
        }
    }
}
