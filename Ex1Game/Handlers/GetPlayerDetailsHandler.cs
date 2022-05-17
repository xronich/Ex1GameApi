using Ex1Game.DataBase;
using Ex1Game.DataBase.Entity;
using Ex1Game.Queries;
using Ex1Game.Service.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ex1Game.Handlers
{
    public class GetPlayerDetailsHandler : IRequestHandler<GetPlayerDetailsQuery, Player>
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IGameService gameService;
        public GetPlayerDetailsHandler(IPlayerRepository playerRepository, IGameService gameService)
        {
            this.gameService = gameService;
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<Player> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
        {
           return gameService.TryGetActivePlayer(request.PlayerId) 
                ?? await playerRepository.GetById(request.PlayerId);
        }
    }
}
