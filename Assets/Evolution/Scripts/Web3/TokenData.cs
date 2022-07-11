using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


using Kimeria.Nyx;

namespace Evo.Web3
{
    [CreateAssetMenu]
    public class TokenData : ScriptableObject
    {
        public enum VariableState
        {
            Unknown,
            Updating,
            Updated,
            Error
        }
        public ContractData contract;
        [SerializeField] string id;
        [SerializeField] BigInteger value;
        [Header("Debug")]
        [SerializeField] VariableState state = VariableState.Unknown;
        [SerializeField] ulong valueInt;

        public BigInteger Value { get => value; set
            {
                this.value = value;
                RefreshDebug();
            }
        }
        public string Id { get => id; }
        public VariableState State { get => state; set => state = value; }

        public void Reset()
        {
            state = VariableState.Unknown;
            value = 0;
            valueInt = 0;
        }

        public ulong GetIntValue()
        {
            //BigInteger v = value / ((new BigInteger(10))^18);
            string s = value.ToString();
            return ulong.Parse(s.Substring(0, s.Length - 18));
        }

        void RefreshDebug()
        {
            valueInt = GetIntValue();
        }

        public void SetStateUpdating()
        {
            State = VariableState.Updating;
        }
        public void SetStateUpdated()
        {
            State = VariableState.Updated;
        }
        public void SetStateError()
        {
            State = VariableState.Error;
        }

        public void SetValue(BigInteger val)
        {
            Value = val;
            State = VariableState.Updated;
        }
    }
}


