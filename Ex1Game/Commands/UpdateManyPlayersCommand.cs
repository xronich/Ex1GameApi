using Ex1Game.DataBase.Entity;
using MediatR;

namespace Ex1Game.Commands
{
    public class UpdateManyPlayersCommand : IRequest<Unit>
    {
        public Player[] Players { get; set; }
    }
}
