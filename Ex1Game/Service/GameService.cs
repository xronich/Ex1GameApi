using Ex1Game.Service.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using MediatR;
using Ex1Game.Commands;
using Ex1Game.DataBase.Entity;
using Ex1Game.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace Ex1Game.Service
{
    public class GameService : IGameService
    {
        private bool gameStart, gameCreated;
        private Player hostPlayer;
        private readonly Random RandomDamage = new Random();
        private static object locker = new object();

        private ConcurrentDictionary<Guid, Player> playersMap = new ConcurrentDictionary<Guid, Player>();
        private readonly IServiceProvider _serviceProvider;

        public bool Active => gameStart;

        public GameService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void CreateGame(Player player)
        {
            if (hostPlayer != null)
                throw new GameAlreadyCreatedException();

            lock (locker)
            {
                hostPlayer = player;
                playersMap.TryAdd(player.Id, player);
                gameCreated = true;
            }
        }

        public Player TryGetActivePlayer(Guid playerId) => playersMap.TryGetValue(playerId, out Player value) ? value : value;

        public void JoinGame(Player player)
        {
            if (!gameCreated)
                throw new GameDontCreateException();

            if (hostPlayer == player)
                throw new HostCantJoinException();

            lock (locker)
            {
                if (gameStart)
                    throw new GameInProgressException();

                gameStart = true;

                Task.Factory.StartNew(async () => await StartGame(player)).ConfigureAwait(false);
            }
        }

        private async Task StartGame(Player player)
        {
            playersMap.TryAdd(player.Id, player);

            while (true)
            {
                if (!hostPlayer.TryDamage(GetDamage()))
                    break;

                if (!player.TryDamage(GetDamage()))
                    break;

                await Task.Delay(2000);
            }

            try
            {
                await UpdateDBPlayersStore(hostPlayer, player);
            }
            finally
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            playersMap.Clear();
            hostPlayer = null;
            gameStart = false;
        }
        private int GetDamage() => RandomDamage.Next(0, 3);

        private async Task UpdateDBPlayersStore(params Player[] players)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await _mediator.Send(new UpdateManyPlayersCommand { Players = players });
            }
        }
    }
}
