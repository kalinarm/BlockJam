using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;


using Kimeria.Nyx;

namespace Evo.Web3
{
    [System.Serializable]
    public class AnswerAllToken
    {
        public Token[] tokens;
    }
    [System.Serializable]
    public class Token
    {
        public string tokenId;
        public string uri;
        public string contract;
        public int balance;
    }
    [System.Serializable]
    public class Trait
    {
        public string trait_type;
        public string value;
        public string trait_count;
        public int balance;
    }
    [System.Serializable]
    public class Metadata
    {
        public string image;
        public string name;
        public string description;
        public string external_link;
        public List<Trait> traits = new List<Trait>();
    }
    public class MushroomNFT : MonoBehaviour
    {
        public string gamename = "mushroom";

        [Header("Config")]
        public string account = "0xdDA39123625E17Cd717F24B3f49dCA440545C62b";

        public string chain = "Polygon";
        public string network = "mainnet";
        public string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963";
        public string tokenId = "100250137315638478648027743950476059781631224595317640807290582774264274878465";
        public string[] tokenIds = new string[]{"100250137315638478648027743950476059781631224595317640807290582774264274878465"};

        public int pageAmount = 500;
        public int pageSkip = 0;
        
        [Header("Debug")]
        public AnswerAllToken answer;
        public Metadata metadata;


        [EditorButton]
        async void RetrieveBalance()
        {
            BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
            print($"Balance of {gamename} token for this account : {balanceOf}");
        }

        [EditorButton]
        async void RetrieveBalanceBatch()
        {
            List<BigInteger> batchBalances = await ERC1155.BalanceOfBatch(chain, network, contract, new string[] { account }, tokenIds);
            foreach (var balance in batchBalances)
            {
                print("BalanceOfBatch: " + balance);
            }
        }

        [EditorButton]
        async void RetrieveUri()
        {
            string uri = await ERC1155.URI(chain, network, contract, tokenId);
            print($"Uri of {gamename} token for this account : {uri}");

            string metaDataUrl = uri.Replace("0x{id}", tokenId);
            print("metadata url " + metaDataUrl);

            StartCoroutine(DownloadAndParseJson<Metadata>(metaDataUrl, metadata));
        }

        [EditorButton]
        async void GetAllTokens()
        {
            string response = await EVM.AllErc1155(chain, network, account, contract, pageAmount, pageSkip);
            print($"All {gamename} token for this account : {response}");

            string json = "{\"tokens\":" + response + "}";
            JsonUtility.FromJsonOverwrite(json, answer);
        }


        IEnumerator DownloadAndParseJson<T>(string url, T obj)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error + "when downloading from " + url);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                JsonUtility.FromJsonOverwrite(www.downloadHandler.text, obj);
            }
        }
    }
}

