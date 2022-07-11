using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;


namespace Evo.Web3
{
    public class NFTAnswer
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public string balance { get; set; }
    }

    public class Web3Func
    {
        private static string abi = "[ { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"name_\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"symbol_\", \"type\": \"string\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Approval\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Transfer\", \"type\": \"event\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" } ], \"name\": \"allowance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"approve\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"decimals\", \"outputs\": [ { \"internalType\": \"uint8\", \"name\": \"\", \"type\": \"uint8\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"subtractedValue\", \"type\": \"uint256\" } ], \"name\": \"decreaseAllowance\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"addedValue\", \"type\": \"uint256\" } ], \"name\": \"increaseAllowance\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transfer\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transferFrom\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";

        public static string GetABI(ContractData data)
        {
            if (data == null || data.abi.Length==0)
            {
                return abi;
            }
            return data.abi;
        }
        public static async Task<BigInteger> BalanceOf(TokenData token, string account)
        {
            string method = "balanceOf";
            string[] obj = { account };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(token.contract.chain, token.contract.network, token.contract.contract, GetABI(token.contract), method, args, token.contract.rpc);
            try
            {
                return BigInteger.Parse(response);
            }
            catch
            {
                Debug.LogError(response);
                throw;
            }
        }

        public static async Task<string> Claim(TokenData token)
        {
            string method = "claimTokens";
            string[] obj = { };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(token.contract.chain, token.contract.network, token.contract.contract, GetABI(token.contract), method, args, token.contract.rpc);
            return response;
        }
        public static async Task<string> HasClaimed(TokenData token, string account)
        {
            string method = "hasClaimed";
            string[] obj = { account };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(token.contract.chain, token.contract.network, token.contract.contract, GetABI(token.contract), method, args, token.contract.rpc);
            return response;
        }

        public static async Task<string> Burn(TokenData token, string account, string amount)
        {
            string method = "burnFrom";
            string[] obj = { account , amount};
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(token.contract.chain, token.contract.network, token.contract.contract, GetABI(token.contract), method, args, token.contract.rpc);
            Debug.Log("Burn Response: " + response);
            return response;
        }

        public static async Task<Models.CreateMintModel.Response> Mint(ContractData  contract, string account, string cid)
        {
            Models.CreateMintModel.Response nftResponse = await EVM.CreateMint(contract.chain, contract.network, account, account, cid);
            Debug.Log("NFT Response: " + nftResponse);

            try
            {
                string response = await Web3Wallet.SendTransaction(contract.chainId, nftResponse.tx.to, nftResponse.tx.value, nftResponse.tx.data, nftResponse.tx.gasLimit, nftResponse.tx.gasPrice);
                Debug.Log(response);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
            return nftResponse;
        }

        public static async Task<string> Transfer720(TokenData token, string toAccount, string amount)
        {
            string method = "transfer";
            // value in wei
            string value = "0";
            string[] obj = { toAccount, amount };
            string args = JsonConvert.SerializeObject(obj);
            // create data to interact with smart contract
            string data = await EVM.CreateContractData(GetABI(token.contract), method, args);
            // gas limit OPTIONAL
            string gasLimit = "";
            // gas price OPTIONAL
            string gasPrice = "";
            // send transaction
            string response = await Web3Wallet.SendTransaction(token.contract.chainId, token.contract.contract, value, data, gasLimit, gasPrice);
            Debug.Log(response);
            return response;
        }
    }
}


