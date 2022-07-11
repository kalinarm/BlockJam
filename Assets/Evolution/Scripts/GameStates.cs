using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Helpers;
using Kimeria.Nyx.Tools.FSM;

namespace Evo.GameStates
{
    public class GameState : BaseState
    {
        protected Game parent;

        public GameState(string name, Game stateMachine) : base(name, stateMachine)
        {
            parent = (Game) this.stateMachine;
        }

        public virtual Game.State GetState()
        {
            return Game.State.Undefined;
        }
    }

    public class Train : GameState
    {
        public Train(string name, Game stateMachine) : base(name, stateMachine)
        {

        }
        public override Game.State GetState()
        {
            return Game.State.Train;
        }

        public override void Enter()
        {
            Debug.Log("Enter train state");
            parent.battle?.ResetBattle();
        }

        public override void UpdateLogic()
        {

        }
    }

    public class Battle : GameState
    {
        bool autoExitAfterTimer = false;
        float timeFinished = 0f;
        float timeAfterBattler = 3f;

        public Battle(string name, Game stateMachine) : base(name, stateMachine)
        {

        }

        public override Game.State GetState()
        {
            return Game.State.Battle;
        }

        public override void Enter()
        {
            Debug.Log("Enter battle state");
            parent.battle.DisplayUI(true);
            parent.battle.CreateArmies();
            parent.battle.ResetBattle();

            //parent.battle.LaunchBattle();
            timeFinished = 0f;
        }
        public override void Exit()
        {
            parent.battle.DestroyArmies();
            parent.battle.DisplayUI(false);
        }
        public override void UpdateLogic()
        {
            if (!autoExitAfterTimer) return;
            if (parent.battle.state == Evo.Battle.State.Finished)
            {
                timeFinished += Time.deltaTime;
                if (timeFinished > timeAfterBattler)
                {
                    timeFinished = 0;
                    parent.SwitchToTrainMode();
                }
            }
        }
    }


}

