using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

using Kimeria.Nyx;

namespace Evo
{
    public class Battle : MonoBehaviour
    {
        public enum State
        {
            Prepare,
            Launching,
            Running,
            Finished
        }
        public State state = State.Prepare;

        public GameData gameData;
        public BattleArmies dataExchange;

        [Header("Refs")]
        public List<Transform> placesArmyA = new List<Transform>();
        public List<Transform> placesArmyB = new List<Transform>();

        [Header("UI")]
        public GameObject objRootUI;
        public GameObject objBattle;
        public GameObject objCountdown;
        public TMP_Text texCountdown;
        public LevelUi levelUI;

        public BattleEndPanel panelEnd;

        [Header("Fx")]
        public Fx[] fxCountdown;
        public Fx fxGo;
        public Fx fxWin;
        public Fx fxLoose;

        [Header("Fx Battle")]
        public Fx fxLooseLife;
        public Fx fxWinLife;
        public Fx fxEvade;

        [Header("Others")]
        public int StartCountdown = 3;
        public float countdownTime = 1f;

        private void Start()
        {
            levelUI.SetValue(dataExchange.level);
        }

        private void Update()
        {
            switch (state)
            {
                case State.Prepare:
                    break;
                case State.Running:
                    CheckIfBattleFinish();
                    break;
                case State.Finished:
                    break;
            }
        }


        [EditorButton]
        public void CreateArmies()
        {
            //create player army
            List<Creature> allies = Game.Instance.CreateArmy(dataExchange.armyPlayer, placesArmyA, 0);

            if (dataExchange.level == null)
            {
                Debug.LogError("Level was not defined", this);
                return;
            }

            //create npc army
            List<EntitySpecification> entities = new List<EntitySpecification>(dataExchange.level.army.entities);
            if (dataExchange.level.addedRandomCreature > 0)
            {
                Kimeria.Nyx.Modules.Genetic.GeneticCode code = new Kimeria.Nyx.Modules.Genetic.GeneticCode();
                code.Randomize(10);
                EntitySpecification es = new EntitySpecification();
                es.dna = code.AsHexString;
                entities.Add(es);
            }
            List<Creature> ennemies =  Game.Instance.CreateArmy(entities, placesArmyB, 1);

            //apply bonus
            foreach (var item in ennemies)
            {
                item.ApplyBonusProperties(dataExchange.level.bonusProperties);
            }

            //Create hp bar
            foreach (var item in allies)
            {
                HPBar bar = GameObject.Instantiate(gameData.hpBarAlly);
                bar.SetValue(item.hp);
            }
            foreach (var item in ennemies)
            {
                HPBar bar = GameObject.Instantiate(gameData.hpBarEnnemy);
                bar.SetValue(item.hp);
            }

        }

        public void DestroyArmies()
        {
            
        }

        [EditorButton]
        public void ResetBattle()
        {
            state = State.Prepare;

            foreach (var item in CreatureManager.Creatures)
            {
                item.GetComponent<Brain>().enabled = false;
            }

            objBattle?.SetActive(true);
            objCountdown?.SetActive(false);
            panelEnd.Close();
        }

        [EditorButton]
        public void LaunchBattle()
        {
            if (state != State.Prepare) return;
            StartCoroutine(LaunchBattleRoutine());
        }
        public IEnumerator LaunchBattleRoutine()
        {
            objBattle?.SetActive(true);
            objCountdown?.SetActive(false);

            state = State.Launching;
            
            yield return new WaitForSeconds(countdownTime);

            WaitForSeconds wait = new WaitForSeconds(1f);

            objBattle?.SetActive(false);
            objCountdown?.SetActive(true);

            int i = StartCountdown;
            while(i-->0)
            {
                NotifyCountdown(i);
                yield return wait;
            }

            LaunchBattleEffective();
            yield return wait;

            objCountdown?.SetActive(false);
        }

        void LaunchBattleEffective()
        {
            texCountdown.text = "Go";
            fxGo?.Trigger();

            state = State.Running;

            foreach (var item in CreatureManager.Creatures)
            {
                item.GetComponent<Brain>().enabled = true;
            }
        }

        void NotifyCountdown(int number)
        {
            /*if (number == StartCountdown -1)
            {
                texCountdown.text = "Ready";
            }else
            {
                texCountdown.text = (number + 1).ToString();
            }*/

            texCountdown.text = (number + 1).ToString();
            if (number < fxCountdown.Length)
            {
                fxCountdown[number].Trigger();
            }
        }

        public void DisplayUI(bool enabled)
        {
            objRootUI?.SetActive(enabled);
        }

        void CheckIfBattleFinish()
        {
            var listCreat = CreatureManager.CreaturesByTeam;
            if (listCreat.Keys.Count == 0)
            {
                Debug.Log("Tie");
                TeamWin(-1);
                state = State.Finished;
                return;
            }
            if (listCreat.Keys.Count <= 1)
            {
                TeamWin(listCreat.Keys.ToList()[0]);
                state = State.Finished;
            }
        }

        void TeamWin(int teamIndex)
        {
            Debug.Log("Team win " + teamIndex);
            Game.Events.Trigger(new Evt.BattleWin(teamIndex));

            if (teamIndex == -1)
            {
                //Its a tie
                panelEnd.Configure("It's a tie");
                return;
            }

            if (gameData == null) return;
            if (gameData.playerTeamIndex == teamIndex)
            {
                PlayerWin();
                
            }
            else
            {
                PlayerLoose();
                
            }
        }

        void PlayerWin()
        {
            Debug.Log("Player Won");
            fxWin?.Trigger();
            Game.Instance.WinReward(dataExchange.level.reward);
            panelEnd.SetupWin(dataExchange.level.reward);
        }
        void PlayerLoose()
        {
            panelEnd.SetupLoose();
            Debug.Log("Player Loose");
            fxLoose?.Trigger();
        }
    }
}

