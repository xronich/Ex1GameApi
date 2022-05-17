using Ex1Game.DataBase.Entity;
using System;

namespace Ex1Game.Service.Interfaces
{
    public interface IGameService
    {
        void JoinGame(Player player);
        void CreateGame(Player player);
        Player TryGetActivePlayer(Guid playerId);
    }
}
