using Kimeria.Nyx.Tools;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

namespace Evo.Web3
{
    public class Web3Hub : MonoBehaviour
    {
        public static Web3Hub Instance;

        public enum Mode
        {
            WebGL,
            Desktop
        }
        [Header("Config")]
        [SerializeField] Mode mode = Mode.WebGL;
        [SerializeField] bool rememberMe = true;
        [SerializeField] TokenData[] tokens;
        [SerializeField] ContractData contractDignitas;
        [SerializeField] Web3StorageData storageDataTest;

        [Header("Refs")]
        public Web3Storage storage;
#if UNITY_WEBGL
        public WebLogin webLogin;
#endif
        public WalletLogin desktopLogin;

        [Header("Variables")]
        [SerializeField] bool isConnected;
        public string account;

        EventManager evtManager = new EventManager();

        public static EventManager Events { get => Instance.evtManager; }

        public TokenData GoldToken
        {
            get => tokens[0];
        }

#region unity
        private void Awake()
        {
            if (Instance != null)
            {
                GameObject.Destroy(gameObject);
            }
            Debug.Log("Init Web3 Hub");
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (mode == Mode.WebGL)
            {
#if UNITY_WEBGL
                webLogin.gameObject.SetActive(true);
#endif
            }
            else
            {
                desktopLogin.gameObject.SetActive(true);
                TryAutoLogin();
            }

            //reset tokens state
            foreach (var item in tokens)
            {
                item.Reset();
            }
        }
#endregion

#region accessors
        public bool IsConnected()
        {
            return isConnected;
        }
#endregion

#region core
        [EditorButton]
        async void RetrieveVariables()
        {
            foreach (var item in tokens)
            {
                Task<BigInteger> taskBalance = GetBalance(item);
                BigInteger result = await taskBalance;
                evtManager.Trigger(new Evt.VariableChanged(item));
            }
        }
#endregion

#region callback

        void LoginDone()
        {
            account = PlayerPrefs.GetString("Account");
            isConnected = account.Length > 0;
            evtManager.Trigger(new Evt.ConnectionStatusChanged(isConnected));
            Debug.Log("Login done on account " + account);

            RetrieveVariables();


        }
#endregion


#region blockchain
        public async Task<BigInteger> GetBalance(TokenData token)
        {
            token.SetStateUpdating();
            //BigInteger balanceOf = await ERC20.BalanceOf(variable.contract.chain, variable.contract.network, variable.contract.contract, account, variable.contract.rpc);
            BigInteger balanceOf = await Web3Func.BalanceOf(token, account);
            token.SetValue(balanceOf);
            Debug.Log($"Balance of {token.Id} : {balanceOf}");
            return balanceOf;
        }
        public async Task ClaimToken(TokenData variable)
        {
            string response = await Web3Func.Claim(variable);
            Debug.Log($"ClaimToken  {response}");
        }
        public async Task<bool> HasClaimToken(TokenData variable)
        {
            string response = await Web3Func.HasClaimed(variable, account);
            Debug.Log($"ClaimToken  {response}");
            return response.ToLower() == "true";
        }
        async Task TransferToken(TokenData variable, string toAccount, int amount)
        {
            string response = await Web3Func.Transfer720(variable, toAccount, IntToString(amount));
            Debug.Log($"TransferToken  {response}");
        }
        public async Task BurnToken(TokenData variable, string toAccount, int amount)
        {
            string response = await Web3Func.Burn(variable, toAccount, IntToString(amount));
            Debug.Log($"BurnToken  {response}");
        }
        public async Task Mint(TokenData variable, string account, string cid)
        {
            Models.CreateMintModel.Response response = await Web3Func.Mint(variable.contract, account, cid);
            Debug.Log($"Mint  {response}");
        }
        async Task GetAll721(ContractData contractData, string _account)
        {
            int first = 500;
            int skip = 0;
            string response = await EVM.AllErc721(contractData.chain, contractData.network, _account, contractData.contract, first, skip);
            try
            {
                NFTAnswer[] erc721s = JsonConvert.DeserializeObject<NFTAnswer[]>(response);
                Debug.Log("ERC721 : " + erc721s[0].contract);
                Debug.Log(erc721s[0].uri);
                Debug.Log(erc721s[0].balance);
            }
            catch
            {
                Debug.LogWarning("Error: " + response);
            }
        }

#endregion

#region login
        public void TryLogin()
        {
            Debug.Log("Login web3");
            if (mode == Mode.WebGL)
            {
#if UNITY_WEBGL
                webLogin.OnLogin();
#endif
            }
            else
            {
                desktopLogin.OnLogin();
                LoginDone();
            }
        }

        private void TryAutoLogin()
        {
            if (rememberMe && PlayerPrefs.GetString("Account") != "")
            {
                LoginDone();
            }
        }

        public void OnSkip()
        {
            Debug.Log("Login skipped");
            if (mode == Mode.WebGL)
            {
#if UNITY_WEBGL
                webLogin.OnSkip();
#endif
            }
            else
            {
                LoadNextScene();
            }
        }

        private static void LoadNextScene()
        {
            SceneManager.LoadScene(1);
        }
#endregion

#region helper
        public static BigInteger IntToBigInteger(BigInteger input)
        {
            return (BigInteger)input;
        }
        public static string BigIntegerToString(BigInteger input)
        {
            return input.ToString();
        }
        public static string IntToString(BigInteger input)
        {
            return BigIntegerToString(IntToBigInteger(input));
        }
#endregion

#region test
        [EditorButton]
        void TestRetrieve()
        {
            GetBalance(GoldToken);
        }
        [EditorButton]
        void TestHasClaimToken()
        {
            HasClaimToken(GoldToken);
        }
        [EditorButton]
        void TestClaimToken()
        {
            ClaimToken(GoldToken);
        }
        [EditorButton]
        void TestTransferToSelf()
        {
            TransferToken(GoldToken, account, 75);
        }
        [EditorButton]
        void TestBurnToken()
        {
            BurnToken(GoldToken, account, 10);
        }
        [EditorButton]
        async void TestRetrieveDignitas()
        {
            string ownerAccount = "0xc1B8E35bfa4bF0f77FAE424F1B2EFFC713189c0c";
            await GetAll721(contractDignitas, ownerAccount);

        }
        [EditorButton]
        async void TestDownloadStorage()
        {
            storage.Download(storageDataTest);
        }
        [EditorButton]
        async void TestStorageUpload()
        {
            storage.Updload(storageDataTest, "{\"content\"=\"test\"}");
        }

        [SerializeField] BattleArmies armies;
        [SerializeField] string armyCid;
        [EditorButton]
        async void TestUploadPlayerArmy()
        {
            var playerArmy = armies.armyPlayer;
            var process = new ProcessUploadArmy(this, playerArmy);
            process.Start();
        }
        [EditorButton]
        async void TestDownloadPlayerArmy()
        {
            var process = new ProcessDownloadArmy(this, armyCid);
            process.Start();
        }
        [EditorButton]
        async void TestMint()
        {
            Mint(GoldToken, account, armyCid);
        }
#endregion
    }
}


