using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class Locker : Button
    {
        public Machine.Cost cost = new Machine.Cost();
        public string lockId;

        public PlayerData playerData;

        private void Start()
        {
            Check();
        }
        void Check()
        {
            SetLocked(!playerData.IsLockerUnlock(lockId));
        }

        void SetLocked(bool locked)
        {
            gameObject.SetActive(locked);
            if (machine != null)
            {
                machine.SetLocked(locked);
            }
        }
        void Unlock()
        {
            playerData.Unlock(lockId);
            SetLocked(false);
        }

        public override bool CanBeActivated()
        {
            return true;
        }

        public override void Activate()
        {
            if (playerData.Gold < cost.gold)
            {
                Kimeria.Nyx.UI.ModalPanelTMP.ShowMessage($"You need at least {cost.gold} gold to unlock this",delegate { });
                return;
            }

            Kimeria.Nyx.UI.ModalPanelTMP.ShowMessage($"Do you want to unlock this for {cost.gold} gold", UnlockEffective, delegate { });
        }

        void UnlockEffective()
        {
            Debug.Log("Unlock effective");
            Unlock();
            playerData.RemoveCost(cost);
        }
    }
}

