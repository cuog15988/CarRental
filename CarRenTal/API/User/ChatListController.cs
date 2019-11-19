using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace CarRenTal.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatListController : ControllerBase
    {

        private readonly RentalCarContext _context;

        public ChatListController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatList>> GetChatList(int id)
        {
            if (id == null)
            { return NotFound(); }

            var chatlist = _context.ChatList.Where(x => x.MyUser == id || x.FromUser == id).OrderByDescending(x => x.Date).ToList();
            if (chatlist.Count == 0)
            {
                return NotFound();
            }

            else
            {
                foreach (var item in chatlist)
                {
                    if (id == item.MyUser)
                    {
                        var user = _context.Users.Find(item.FromUser);
                        item.Name = user.HoTen;
                    }
                    else
                    {
                        var user = _context.Users.Find(id);
                        item.Name = user.HoTen;
                    }
                }
            }

            return Ok(chatlist);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ChatList>> updatems(int id, ChatList chat)
        {
            var chatlist = _context.ChatList.Find(id);
            if (chatlist == null)
            {
                return NotFound();
            }

            else
            {
                if (chat.MyUser != chatlist.MsFrom)
                {
                    chatlist.Status = false;
                    _context.ChatList.Update(chatlist);
                    _context.SaveChanges();
                    return Ok(chatlist);
                }
                else
                {
                    return Ok(chatlist);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ChatList>> getlist(int id)
        {

            var chatlist = _context.ChatList.Find(id);
            if (chatlist == null)
            {
                return NotFound();
            }
            return Ok(chatlist);
        }
        [HttpPost]
        public async Task<ActionResult<ChatList>> PostDonHang(ChatList chat)
        {
            if(chat.MyUser == chat.FromUser)
            {
                return NotFound();
            }
            try
            {
                chat.MyUser = Convert.ToInt32(chat.MyUser);
                chat.FromUser = Convert.ToInt32(chat.FromUser);
            }
            catch
            {
            }

            var li = await _context.ChatList.Where(x => x.MyUser == chat.MyUser && x.FromUser == chat.FromUser || x.MyUser == chat.FromUser && x.FromUser == chat.MyUser).SingleOrDefaultAsync();
            if (li == null)
            {
                //tạo mục liên hệ mới
                chat.Date = DateTime.Now;
                _context.ChatList.Add(chat);
                _context.SaveChanges();
                return chat;

            }
            else
            {
                li.Date = DateTime.Now;
                li.LastMs = chat.LastMs;
                li.MsFrom = chat.MsFrom;
                li.Status = chat.Status;
                _context.ChatList.Update(li);
                _context.SaveChanges();
                return li;
            }

        }

        // GET: api/ChatList
    }
}