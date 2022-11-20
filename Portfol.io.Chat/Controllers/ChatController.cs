using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Portfol.io.Chat.API.Hubs;
using Portfol.io.Chat.Application.Exceptions;
using Portfol.io.Chat.Application.Repository;

namespace Portfol.io.Chat.API.Controllers
{
    [Route("api/chat")]
    public class ChatController : BaseController
    {
        private readonly IChatRepository _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatRepository repository, IHubContext<ChatHub> hub)
        {
            _repository = repository;
            _hubContext = hub;
        }

        [HttpGet("getChat")]
        public async Task<IActionResult> GetChat(Guid chatId)
        {
            try
            {
                if (chatId == Guid.Empty) return BadRequest(new { message = "CompanionId is required" });

                var result = await _repository.GetChat(chatId, UserId);

                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet("getChats")]
        public async Task<IActionResult> GetChats()
        {
            try
            {
                var result = await _repository.GetChats(UserId);

                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPost("createChat")]
        public async Task<IActionResult> CreateChat(Guid companionId)
        {
            if (companionId == Guid.Empty) return BadRequest(new { message = "CompanionId is required" });

            var result = await _repository.CreateChat(UserId, companionId);

            return Ok(new { chatId = result });
        }

        /*[HttpPost("addMessage")]
        public async Task<IActionResult> AddMessage(Guid chatId, string messageText)
        {
            if (chatId == Guid.Empty || messageText is null) return BadRequest(new { message = "All parameters are required" });

            var result = await _repository.AddMessage(chatId, messageText, UserId);

            return Ok(new {messageId = result});
        }*/
        //TODO: Сообщения приходят только одному юзеру
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage(string messageText, Guid chatId)
        {
            if (chatId == Guid.Empty || messageText is null) 
                return BadRequest(new { message = "All parameters are required" });

            var result = await _repository.AddMessage(chatId, messageText, UserId);

            await _hubContext.Clients.Group(chatId.ToString()).SendAsync("Receive", UserId, messageText);

            return Ok(new { messageId = result });
        }
    }
}
