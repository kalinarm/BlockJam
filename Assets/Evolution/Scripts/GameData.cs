using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx;

namespace Evo
{
    public enum GameEventType
    {
        NONE = 0,
        ASK_START_BATTLE = 1,
        LOAD_SCENE_MENU = 2,
        LOAD_SCENE_DEFAULT = 3,
        LOAD_SCENE_BATTLE = 4,
        LOAD_SCENE_CREATURE_EXPLORER = 5,
    }

    [CreateAssetMenu]
    public class GameData : ScriptableObject
    {
        [Header("Scene")]
        public int sceneConnect = 0;
        public int sceneTrain = 1;
        public int sceneBattle = 2;
        public int sceneCreatureExplorer = 3;

        [Header("Teams")]
        public int playerTeamIndex = 0;

        [Header("Layers")]
        public LayerMask layerCreature;
        public LayerMask layerSlot;
        public LayerMask layerEntity;
        public LayerMask layerMachine;
        public LayerMask layerInteractable;

        [Header("Config")]
        public float minZ = -2f;
        public float maxZ = 2f; 
        public float minX = -10f;
        public float maxX = 10f;

        [Header("Prefabs")]
        public Creature prefabCreature;
        public Mushroom prefabMushroom;
        public HPBar hpBarAlly;
        public HPBar hpBarEnnemy;

        [Header("Data")]
        public Levels levels;
        public Levels levelsDignitas;
        public Levels levelsMultiplayer;

        [Header("Cursor")]
        public Vector2 cursorOffset;
        public Texture2D cursorDefault;
        public Texture2D cursorMove;
        public Texture2D cursorInteractableOn;
        public Texture2D cursorInteractableOff;

        public void LoadMenuScene()
        {
            SceneManager.LoadScene(sceneConnect);
        }
        public void LoadDefaultScene()
        {
            SceneManager.LoadScene(sceneTrain);
        }
        public void LoadBattleScene()
        {
            SceneManager.LoadScene(sceneBattle);
        }
        public void LoadCreatureExplorerScene()
        {
            SceneManager.LoadScene(sceneCreatureExplorer);
        }
    }
}

