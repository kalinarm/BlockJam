using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;
namespace Evo
{
    public class Game : StateMachine
    {
        #region static
        static Game instance;
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<Game>();
                }
                return instance;
            }
        }
        public EventManager evtManager = new EventManager();

        public static EventManager Events
        {
            get
            {
                return Instance.evtManager;
            }
        }

        public State StartState { get => startState; set => startState = value; }
        #endregion

        public enum State
        {
            Undefined,
            Train,
            Battle
        }

        public GameData data;
        public BattleArmies battleData;
        public PlayerData playerData;
        public Battle battle;
        public MachineStockCreature stock;

        GameStates.GameState stateTrain;
        GameStates.GameState stateBattle;

        [SerializeField] State startState = State.Train;
        [SerializeField] Machine[] machinesToSerialize;

        [Header("Fx")]
        public Fx fxButtonActivate;
        public Fx fxCantPayCost;
        public Fx fxWinGold;


        void Awake()
        {
            stateTrain = new GameStates.Train("train", this);
            stateBattle = new GameStates.Battle("battle", this);
        }
        private void OnEnable()
        {
            evtManager.AddListener<Evt.GameEvent>(OnGameEvent);
        }
        private void OnDisable()
        {
            evtManager.RemoveListener<Evt.GameEvent>(OnGameEvent);
        }
        protected override BaseState GetInitialState()
        {
            if (startState == State.Undefined) return new GameStates.GameState("wait", this);
            if (startState == State.Train) return stateTrain;
            return stateBattle;
        }

        public State GetCurrentState()
        {
            return ((GameStates.GameState)currentState).GetState();
        }

        protected override void Update()
        {
            evtManager.Step(Time.deltaTime);
            base.Update();
        }

        [EditorButton]
        public void StartBattle()
        {
            SaveData();
            //define the ennemy army
            battleData.levels = data.levels;
            battleData.level = data.levels.GetLevel(playerData.level);
            SceneManager.LoadScene(data.sceneBattle);
        }
        public void StartBattle(Levels levels)
        {
            SaveData();
            //define the ennemy army
            battleData.levels = levels;
            battleData.level = levels.GetLevel(playerData.levelDignitas);
            SceneManager.LoadScene(data.sceneBattle);
        }
        public void StartBattleAgainst(ArmyData army)
        {
            SaveData();
            //define the ennemy army
            battleData.levels = data.levelsMultiplayer;
            battleData.level = ScriptableObject.CreateInstance<LevelData>();
            battleData.level.army = army;
            SceneManager.LoadScene(data.sceneBattle);
        }

        public void SaveData()
        {
            playerData.Save();
            //serialize all player data
            foreach (var item in machinesToSerialize)
            {
                item.SaveData();
            }
        }

        [EditorButton]
        public void SwitchToTrainMode()
        {
            SceneManager.LoadScene(data.sceneTrain);
        }


        public Creature CreateCreature(GeneticCode code, int teamIndex = 0)
        {
            if (code.Length == 0) return null;
            Creature c = GameObject.Instantiate(data.prefabCreature);
            c.teamIndex = teamIndex;
            c.SetupReference();
            c.genetic = code.Clone();
            c.Generate();
            return c;
        }
        public Creature CreateRandomCreature(int teamIndex = 0)
        {
            Creature c = GameObject.Instantiate(data.prefabCreature);
            c.teamIndex = teamIndex;
            c.SetupReference();
            c.Randomize();
            return c;
        }
        public Creature CreateCreature(EntitySpecification spec, int teamIndex = 0)
        {
            if (spec == null || !spec.IsDefined()) return null;
            GeneticCode code = new GeneticCode(spec.dna);
            return CreateCreature(code, teamIndex);
            
        }
        public Mushroom CreateMushroom(string dna)
        {
            GeneticCode code = new GeneticCode();
            code.AsHexString = dna;
            return CreateMushroom(code);
        }
        public Mushroom CreateMushroom(GeneticCode code)
        {
            Mushroom c = GameObject.Instantiate(data.prefabMushroom);
            c.genetic = code.Clone();
            c.Generate();
            return c;
        }
        public Mushroom CreateMushroom(EntitySpecification spec)
        {
            if (spec == null || !spec.IsDefined()) return null;
            return CreateMushroom(spec.dna);
        }

        public List<Creature> CreateArmy(ArmyData armyData, int teamIndex)
        {
            List<Creature> r = new List<Creature>(armyData.entities.Count);
            Creature c = null;
            foreach (var item in armyData.entities)
            {
                c = CreateCreature(item, teamIndex);
                if (c == null) continue;
                c.teamIndex = teamIndex;
                c.Generate();
                r.Add(c);
            }
            return r;
        }
        public List<Creature> CreateArmy(List<EntitySpecification> entities, List<Transform> places, int teamIndex)
        {
            List<Creature> r = new List<Creature>(entities.Count);
            Creature c = null;
            int index = 0;
            foreach (var item in entities)
            {
                c = CreateCreature(item, teamIndex);
                if (c == null) continue;
                c.transform.position = places[index++].position;
                c.SetupReference();
                c.Generate();
                r.Add(c);
            }
            return r;
        }
        public List<Creature> CreateArmy(ArmyData armyData, List<Transform> places, int teamIndex)
        {
            List<Creature> r = new List<Creature>(armyData.entities.Count);
            Creature c = null;
            int index = 0;
            foreach (var item in armyData.entities)
            {
                c = CreateCreature(item, teamIndex);
                if (c == null) continue;
                c.transform.position = places[index++].position;
                c.SetupReference();
                c.Generate();
                r.Add(c);
            }
            return r;
        }
        public List<Creature> CreateRandomArmy(int count, List<Transform> places, int teamIndex)
        {
            List<Creature> r = new List<Creature>(count);
            Creature c = null;
            for (int i = 0; i < count; i++)
            {
                c = CreateRandomCreature(teamIndex);
                if (c == null) continue;
                c.transform.position = places[i].position;
                r.Add(c);
            }
            return r;
        }
        public List<Creature> AddRandomCreature(int count, List<Transform> places, int teamIndex, int placeIndexStart)
        {
            List<Creature> r = new List<Creature>(count);
            Creature c = null;
            for (int i = 0; i < count; i++)
            {
                c = CreateRandomCreature(teamIndex);
                if (c == null) continue;
                c.transform.position = places[i + placeIndexStart].position;
                r.Add(c);
            }
            return r;
        }

        public List<Creature> CreateArmyStock(ArmyData armyData, int teamIndex, Slot[] slots)
        {
            List<Creature> r = new List<Creature>(armyData.entities.Count);
            Creature c = null;
            int index = 0;
            foreach (var item in armyData.entities)
            {
                if (index >= slots.Length) break;
                if (item == null)
                {
                    index++;
                    continue;
                }
                c = CreateCreature(item);
                if (c == null) continue;
                c.team.teamIndex = teamIndex;
                c.Generate();
                r.Add(c);
                slots[index++].AttachCharacter(c.Entity);
            }
            return r;
        }

        public List<Mushroom> CreateMushrooms(ArmyData armyData)
        {
            List<Mushroom> r = new List<Mushroom>(armyData.entities.Count);
            Mushroom c = null;
            foreach (var item in armyData.entities)
            {
                c = CreateMushroom(item);
                r.Add(c);
            }
            return r;
        }

        public void ActivateInteractable(Interactable inter)
        {
            if (inter == null)
            {
                return;
            }
            if (inter is not Button)
            {
                inter.Activate();
                return;
            }
            Button button = (Button)inter;

            if (!button.CanBeActivated())
            {
                Debug.Log("button can't be activated");
                return;
            }

            if (inter is Locker)
            {
                inter.Activate();
                return;
            }

            Machine machine = button.machine;
            if (machine == null)
            {
                button.Activate();
                return;
            }

            if(!playerData.CanPayCost(machine.cost))
            {
                fxCantPayCost?.Trigger(inter.transform.position);
                Debug.Log("Can't pay cost");
                return;
            }

            playerData.RemoveCost(machine.cost);
            Debug.Log("cost paid");
            button.Activate();
            fxButtonActivate?.Trigger(inter.transform.position);
            Events.Trigger(new Evt.PlayerDataChanged(playerData));
        }

        public void WinReward(Reward reward)
        {
            switch(battleData.Campaign)
            {
                case Levels.Campaign.Dignitas: playerData.levelDignitas++; break;
                default: playerData.level++; break;
            }
            WinGold(reward.gold);
        }
        public void WinGold(int gold)
        {
            playerData.WinGold(gold);
            Events.Trigger(new Evt.PlayerDataChanged(playerData));
            fxWinGold?.Trigger();
        }
        [EditorButton]
        public void ResetPlayerData()
        {
            playerData.Reset();
            battleData.Reset();
            TutorialPanel.ClearPlayerPrefs();
        }

        public void BackToMenu(bool save = true)
        {
            if (save)
            {
                SaveData();
            }
            data.LoadMenuScene();
        }
        public void LoadCreatureExplorer()
        {
            data.LoadCreatureExplorerScene();
        }

        public void QuitGame()
        {
            Kimeria.Nyx.UI.ModalPanelTMP.ShowMessage("Are you sure you want to exit ?", QuitGameEffective, delegate { });
        }
        public void QuitGameEffective()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        #region callback
        void OnGameEvent(Evt.GameEvent evt)
        {
            switch (evt.type)
            {
                case GameEventType.ASK_START_BATTLE:
                    StartBattle();
                    break;
                case GameEventType.LOAD_SCENE_MENU:data.LoadMenuScene();break;
                case GameEventType.LOAD_SCENE_CREATURE_EXPLORER:data.LoadCreatureExplorerScene();break;
                case GameEventType.LOAD_SCENE_DEFAULT:data.LoadDefaultScene();break;
                case GameEventType.LOAD_SCENE_BATTLE:data.LoadBattleScene();break;
                default:
                    break;
            }
        }
        #endregion
    }
}

