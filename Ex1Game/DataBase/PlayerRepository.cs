using Ex1Game.DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ex1Game.DataBase
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayerDBContext playerDBContext;

        public PlayerRepository(PlayerDBContext playerDBContext)
        {
            this.playerDBContext = playerDBContext
                ?? throw new ArgumentNullException(nameof(playerDBContext));
        }

        public async Task<Player> Add(Player player)
        {
            await playerDBContext.Player.AddAsync(player);
            playerDBContext.SaveChanges();

            return player;
        }

        public async Task<Player> GetById(Guid id)
        {
            return await playerDBContext.Player.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateMany(params Player[] players)
        {
            playerDBContext.Player.UpdateRange(players);
            await playerDBContext.SaveChangesAsync();
        }
    }
}
