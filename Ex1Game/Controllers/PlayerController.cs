using Ex1Game.Commands;
using Ex1Game.DataBase.Entity;
using Ex1Game.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ex1Game.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IMediator _mediator;

        public PlayerController(ILogger<GameController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("details")]
        [HttpGet]
        public async Task<Player> GetDetails(Guid playerId)
        {
            return await _mediator.Send(new GetPlayerDetailsQuery { PlayerId = playerId });
        }
    }
}
