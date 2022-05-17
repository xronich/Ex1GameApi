using Ex1Game.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ex1Game.DataBase.Entity
{
    public record Player
    {
        [Key]
        public Guid Id { get; init; }
        public int Health { get; private set; }

        public PlayerStatus Status
        {
            get { return Health <= 0 ? PlayerStatus.Dead : PlayerStatus.Alive; }
        }
        public Player(int health = 10)
        {
            Id = Guid.NewGuid();
            Health = health;
        }
        public bool TryDamage(int value)
        {
            Health = Health < value ? 0 : Health -= value;

            return Status == Enum.PlayerStatus.Alive;
        }

        public void Restore()
        {
            Health = 10;
        }
    }
}
