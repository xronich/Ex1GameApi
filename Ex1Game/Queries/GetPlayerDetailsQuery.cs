using Ex1Game.DataBase;
using Ex1Game.DataBase.Entity;
using Ex1Game.Service;
using Ex1Game.Service.Interfaces;
using MediatR;
using System;

namespace Ex1Game.Queries
{
    public class GetPlayerDetailsQuery : IRequest<Player>
    {
        public Guid PlayerId { get; set; }
    }
}
