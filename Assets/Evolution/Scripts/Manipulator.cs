using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Helpers;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class Manipulator : StateMachine
    {
        [Header("Refs")]
        public GameData gameData;
        public CreaturePanel creaturePanel;
        [Header("Params")]
        public float radiusDetection = 1f;
        [Header("Fx")]
        public Fx fxClickForbidden;
        public Fx fxDragCreature;
        public Fx fxDropCreature;

        [Header("Debug")]
        public Entity focusedEntity;
        public Creature focusedCreature;
        public Interactable focusedInteractable;
        public Slot focusedSlot;

        public Entity selectedEntity;
        public Vector3 posCursor;

        private Vector3 screenPoint;
        private Vector3 offset;
        private Vector3 bakedPosition;

        ManipStates.DefaultState stateDefault;

        void Awake()
        {
            stateDefault = new ManipStates.DefaultState("default", this);
        }

        protected override BaseState GetInitialState()
        {
            return stateDefault;
        }

        public void SwitchToDefaultState()
        {
            ChangeState(stateDefault);
        }

        public void DetectUnderCursor()
        {
            focusedEntity = PhysicsHelper.DetectUnderMouse2D<Entity>(gameData.layerEntity);
            focusedSlot = PhysicsHelper.DetectUnderMouse2D<Slot>(gameData.layerSlot);
            focusedInteractable = PhysicsHelper.DetectUnderMouse2D<Interactable>(gameData.layerInteractable);

            focusedCreature = focusedEntity == null ? null : focusedEntity.GetComponent<Creature>();
            creaturePanel.Creature = focusedCreature;

            SetCursor();
        }

        void SetCursor()
        {
            if (selectedEntity)
            {
                Cursor.SetCursor(gameData.cursorMove, gameData.cursorOffset, CursorMode.Auto);
                return;
            }
            if (focusedInteractable ==null)
            {
                Cursor.SetCursor(gameData.cursorDefault, gameData.cursorOffset, CursorMode.Auto);
                return;
            }
            if (focusedInteractable.CanBeActivated())
            {
                Cursor.SetCursor(gameData.cursorInteractableOn, gameData.cursorOffset, CursorMode.Auto);
                return;
            }
            Cursor.SetCursor(gameData.cursorInteractableOff, gameData.cursorOffset, CursorMode.Auto);
        }
    }
}

