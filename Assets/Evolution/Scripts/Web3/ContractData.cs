using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;

using Kimeria.Nyx;

namespace Evo.Web3
{
    [CreateAssetMenu]
    public class ContractData : ScriptableObject
    {
        // Chain == Chain name connecting
        public string chain = "godwoken";
        public string chainId = "868455272153094";

        // Network == The network RPC name
        public string network = "testnet-v1";

        // USDC address == Token Contract Address
        public string contract = "0x069BB45e8DD102aDDcE3aDE9Aea4CF1A6345Adf2";

        // rpc of chain
        public string rpc = "https://godwoken-testnet-v1.ckbapp.dev";

        [Multiline]
        public string abi = "[ { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"previousOwner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"newOwner\", \"type\": \"address\" } ], \"name\": \"OwnershipTransferred\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"owner\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"renounceOwnership\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"newOwner\", \"type\": \"address\" } ], \"name\": \"transferOwnership\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";


    }
}


