using Ex1Game.DataBase.Entity;
using System;
using System.Threading.Tasks;

namespace Ex1Game.DataBase
{
    public interface IPlayerRepository
    {
        Task<Player> Add(Player product);
        Task<Player> GetById(Guid id);
        Task UpdateMany(params Player[] players);
    }
}
