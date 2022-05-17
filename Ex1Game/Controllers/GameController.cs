using Ex1Game.DataBase;
using Ex1Game.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ex1Game.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameService _gameService;
        private readonly IMediator _mediator;

        public GameController(
            IPlayerRepository playerRepository, 
            ILogger<GameController> logger,
            IGameService gameService,
            IMediator mediator)
        {
            _mediator = mediator;
            _gameService = gameService;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        [Route("create-game")]
        [HttpPost]
        public async Task CreateGame(Guid playerId)
        {
            var player = await _playerRepository.GetById(playerId);

            _gameService.CreateGame(player);
        }

        [Route("join-game")]
        [HttpPost]
        public async Task JoinGame(Guid playerId)
        {
            var player = await _playerRepository.GetById(playerId);

            _gameService.JoinGame(player);
        }
    }
}
