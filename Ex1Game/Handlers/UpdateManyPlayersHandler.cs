using Ex1Game.Commands;
using Ex1Game.DataBase;
using Ex1Game.DataBase.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ex1Game.Handlers
{
    public class UpdateManyPlayersHandler : IRequestHandler<UpdateManyPlayersCommand, Unit>
    {
        private readonly IPlayerRepository playerRepository;
        public UpdateManyPlayersHandler(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<Unit> Handle(UpdateManyPlayersCommand request, CancellationToken cancellationToken)
        {
            await playerRepository.UpdateMany(request.Players);

            return Unit.Value;
        }
    }
}
