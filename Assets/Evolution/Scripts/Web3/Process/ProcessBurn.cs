using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo.Web3
{
    [System.Serializable]
    public class ProcessBurn : Web3Process
    {
        string account;
        BigInteger amount;
        public ProcessBurn(MonoBehaviour routineHolder, string account, BigInteger amount) : base(routineHolder)
        {
            this.account = account;
            this.amount = amount;
        }
        public override void Start()
        {
            DeclareStart($"Spending JCE");
            StartAsync();

        }

        async void StartAsync()
        {
            string r = await Web3Func.Burn(Web3Hub.Instance.GoldToken, account, amount.ToString());
            Debug.Log($"process burn return {r}");
            if (!CheckResponseValidity(r))
            {
                DeclareFailed(r);
            }
            DeclareSuccess("JCE burnt");
        }
    }
}


