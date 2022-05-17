using Ex1Game.DataBase;
using Ex1Game.DataBase.Entity;
using Ex1Game.Exception;
using Ex1Game.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Ex1Game.Tests
{
    public class Tests
    {
        [Timeout(5000)]
        [Test]
        [NonParallelizable]
        public void CreateGame_Test()
        {
            var gameService = new GameService(GetServiceProvider());

            var player = new Player();

            gameService.CreateGame(player);

            Assert.IsNotNull(gameService.TryGetActivePlayer(player.Id));
        }

        [Timeout(5000)]
        [Test]
        [NonParallelizable]
        public void CreateGame_StartAgain_ThrowTest()
        {
            var gameService = new GameService(GetServiceProvider());

            var player = new Player();

            gameService.CreateGame(player);

            Assert.Throws<GameAlreadyCreatedException>(() => gameService.CreateGame(player));
        }

        [Timeout(5000)]
        [Test]
        [NonParallelizable]
        public void JoinGame_WithoutCreated_ThrowTest()
        {
            var gameService = new GameService(GetServiceProvider());

            var player = new Player();

            Assert.Throws<GameDontCreateException>(() => gameService.JoinGame(player));
        }

        [Timeout(5000)]
        [Test]
        [NonParallelizable]
        public void JoinGame_MoreTwoPlayers_ThrowTest()
        {
            var gameService = new GameService(GetServiceProvider());

            var player = new Player();
            var player2 = new Player();
            var player3 = new Player();

            gameService.CreateGame(player);
            gameService.JoinGame(player2);

            Assert.Throws<GameInProgressException>(() => gameService.JoinGame(player3));
        }

        [Timeout(5000)]
        [Test]
        [NonParallelizable]
        public void JoinGame_HostCantJoin_ThrowTest()
        {
            var gameService = new GameService(GetServiceProvider());

            var player = new Player();

            gameService.CreateGame(player);

            Assert.Throws<HostCantJoinException>(() => gameService.JoinGame(player));
        }

        [Timeout(5000)]
        [Test]
        [NonParallelizable]
        public void CreateGame_JoinGame_Test()
        {
            var gameService = new GameService(GetServiceProvider());

            var player = new Player();
            var player2 = new Player();

            gameService.CreateGame(player);
            gameService.JoinGame(player2);

            while(gameService.Active) { }

            Assert.IsTrue(player.Status == Enum.PlayerStatus.Dead || player2.Status == Enum.PlayerStatus.Dead);
            Assert.IsTrue(player.Health == 0 || player2.Health == 0);
            Assert.IsTrue(player.Health > 0 || player2.Health > 0);
        }

        #region ServiceProvider
        private IServiceProvider GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(PlayerDBContext)))
                .Returns(new PlayerDBContext(new DbContextOptions<PlayerDBContext>()));

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            return serviceProvider.Object;
        }

        #endregion ServiceProvider
    }
}