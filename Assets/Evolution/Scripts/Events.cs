using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo.Evt
{
    public class CreatureDamaged : IEvent
    {
        public Creature target;
        public int damage;
        public CreatureDamaged(Creature target, int damage)
        {
            this.target = target;
            this.damage = damage;
        }
    }
    public class BattleWin : IEvent
    {
        public int teamWinnerIndex;
        public BattleWin(int teamWinnerIndex)
        {
            this.teamWinnerIndex = teamWinnerIndex;
        }
    }

    public class GameEvent : IEvent
    {
        public GameEventType type;
        public GameEvent(GameEventType type)
        {
            this.type = type;
        }
    }
    public class PlayerDataChanged : IEvent
    {
        public PlayerData data;
        public PlayerDataChanged(PlayerData data)
        {
            this.data = data;
        }
    }
    public class WillQuitScene : IEvent
    {
    }
}

