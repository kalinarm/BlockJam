using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;

using Kimeria.Nyx;

namespace Evo.Web3
{
    public class TestWeb3 : MonoBehaviour
    {
        public ContractData data;

        public string tokenID;

        public string account;
        [EditorButton]
        async void TestERC20()
        {
            Debug.Log("TestERC20");
            BigInteger supply = await ERC20.TotalSupply(data.chain, data.network, data.contract, data.rpc);
            Debug.Log("Total supply :" + supply);

            string symbol = await ERC20.Symbol(data.chain, data.network, data.contract, data.rpc);
            Debug.Log("Symbol :" + symbol);

            string name = await ERC20.Name(data.chain, data.network, data.contract, data.rpc);
            Debug.Log("name :" + name);

            BigInteger balanceOf = await ERC20.BalanceOf(data.chain, data.network, data.contract, account, data.rpc);
            Debug.Log("Balance :" + balanceOf);
        }

        [EditorButton]
        async void TestERC721()
        {
            Debug.Log("TestERC721");
            string result = await ERC721.OwnerOf(data.chain, data.network, data.contract, tokenID, data.rpc);
            Debug.Log("OwnerOf :" + result);
        }
    }
}


