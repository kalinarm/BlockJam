using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Helpers;
using Kimeria.Nyx.Tools.FSM;

namespace Evo.ManipStates
{
    public class DefaultState : BaseState
    {
        protected Manipulator parent;

        public DefaultState(string name, Manipulator stateMachine) : base(name, stateMachine)
        {
            parent = (Manipulator) this.stateMachine;
        }

        public override void Enter()
        {
            //Debug.Log("Enter default state");
        }

        public override void UpdateLogic()
        {
            parent.DetectUnderCursor();

            if (Input.GetMouseButtonDown(0))
            {
                if (parent.focusedInteractable != null)
                {
                    if (!parent.focusedInteractable.CanBeActivated())
                    {
                        parent.fxClickForbidden?.Trigger(parent.focusedInteractable.transform.position);
                        return;
                    }
                    Game.Instance.ActivateInteractable(parent.focusedInteractable);
                    return;
                }

                if (parent.focusedEntity != null)
                {
                    parent.ChangeState(new MoveState("Move", parent, parent.focusedEntity));
                }
            }
        }
    }


    public class MoveState : BaseState
    {
        protected Manipulator parent;
        Entity objectToMove;

        private Vector3 screenPoint;
        private Vector3 offset;
        private Vector3 bakedPosition;
        public MoveState(string name, Manipulator stateMachine, Entity entity) : base(name, stateMachine)
        {
            parent = (Manipulator)this.stateMachine;
            objectToMove = entity;
        }

        public override void Enter()
        {
            parent.selectedEntity = objectToMove;

            bakedPosition = objectToMove.transform.position;
            screenPoint = Camera.main.WorldToScreenPoint(objectToMove.transform.position);
            offset = objectToMove.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            //Debug.Log("enter move state " + bakedPosition);
            parent.fxDragCreature?.Trigger(objectToMove.transform.position);
        }
        public override void Exit()
        {
            //Debug.Log("exit move state " + bakedPosition);
            parent.fxDropCreature?.Trigger(objectToMove.transform.position);
            parent.selectedEntity = null;
        }
        public override void UpdateLogic()
        {
            parent.DetectUnderCursor();

            if (Input.GetMouseButtonUp(0))
            {
                var sourceSlot = objectToMove.slotAttached;

                if (parent.focusedSlot != null && parent.focusedSlot.IsCompatible(objectToMove))
                {
                    if (parent.focusedSlot.HasFreeSlot())
                    {
                        if (sourceSlot != null)
                        {
                            sourceSlot.DetachCharacter();
                        }
                        parent.focusedSlot.AttachCharacter(objectToMove);
                    }
                    else
                    {
                        //swap the character;
                        var c = parent.focusedSlot.linkedCharacter;
                        parent.focusedSlot.DetachCharacter();
                        if (sourceSlot != null)
                        {
                            sourceSlot.DetachCharacter();
                            sourceSlot.AttachCharacter(c);
                        }else
                        {
                            c.transform.position = bakedPosition;
                        }
                        parent.focusedSlot.AttachCharacter(objectToMove);

                    }
                }else
                {
                    if (sourceSlot != null)
                    {
                        sourceSlot.DetachCharacter();
                    }
                }

                parent.SwitchToDefaultState();
                return;
            }

            //do the move
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            objectToMove.transform.position = curPosition;
        }
    }
}

