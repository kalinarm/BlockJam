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
    public class Web3Process
    {
        public enum State
        {
            None,
            Running,
            Failed,
            Success
        }
        [SerializeField] protected string currentStep = "None";
        [SerializeField] protected string error = "";
        [SerializeField] protected string result = "";
        [SerializeField] protected State state = State.None;

        protected MonoBehaviour routineHolder;

        public Action<Web3Process> CallbackSucceed;
        public Action<Web3Process> CallbackFinished;

        public Web3Process(MonoBehaviour routineHolder)
        {
            this.routineHolder = routineHolder;
            currentStep = "";
            error = "";
            state = State.None;
        }
        public bool IsSuccess { get => state == State.Success; }
        public bool IsFinished { get => state == State.Success || state == State.Failed; }
        public string Error { get => error; protected set => error = value; }
        public string Result { get => result; protected set => result = value; }
        public State CurrentState { get => state; }
        public string CurrentStep { get => currentStep; protected set
            {
                currentStep = value;
                Debug.Log(currentStep);
            }
        }

        #region core
        protected void DeclareStart(string message = "")
        {
            state = State.Running;
            CurrentStep = message;
        }
        protected void DeclareSuccess(string message = "")
        {
            result = message;
            state = State.Success;
            Debug.Log($"Process successful : {result}");

            if (CallbackSucceed != null)
            {
                CallbackSucceed(this);
            }
            if (CallbackFinished != null)
            {
                CallbackFinished(this);
            }
        }
        protected void DeclareFailed(string message = "")
        {
            result = message;
            error = message;
            state = State.Failed;
            Debug.Log($"Process failed : {result}");

            if (CallbackFinished != null)
            {
                CallbackFinished(this);
            }
        }
        #endregion

        #region virtual
        public virtual void Start()
        {

        }
        #endregion

        #region helper
        protected string CreatePath(string cid)
        {
            return NFTstorage.Helper.GenerateGatewayPath(cid, NFTstorage.Constants.GatewaysSubdomain[0], true);
        }
        protected void UploadJson(string json, Action<NFTstorage.DataResponse> callback)
        {
            routineHolder.StartCoroutine(NFTstorage.NetworkManager.UploadObject(callback, Encoding.ASCII.GetBytes(json)));
        }
        protected void UpdloadMetadata(NFTstorage.ERC721.NftMetaData metadata, string attachementCID, Action<NFTstorage.DataResponse> callback)
        {
            string path = CreatePath(attachementCID);
            metadata.SetIPFS(attachementCID);
            byte[] dataToBytes = NFTstorage.Helper.ERC721MetaDataToBytes(metadata);
            routineHolder.StartCoroutine(NFTstorage.NetworkManager.UploadObject(callback, dataToBytes));
        }
        protected bool CheckResponseValidity(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log($"error while {currentStep}");
                if (response.Error != null && response.Error.message != null) Error = response.Error.message;
                state = State.Failed;
                return false;
            }
            return true;
        }
        protected bool CheckResponseHasValues(NFTstorage.DataResponse response)
        {
            if (response.Values == null)
            {
                Error = "No values returned";
                state = State.Failed;
                return false;
            }
            return true;
        }
        protected bool CheckResponseValidity(UnityWebRequest response)
        {
            if (response.result != UnityWebRequest.Result.Success)
            {
                Error = response.error;
                state = State.Failed;
                return false;
            }
            return true;
        }
        protected bool CheckResponseValidity(string response)
        {
            if (response.ToLower() != "true")
            {
                Error = response;
                state = State.Failed;
                return false;
            }
            return true;
        }
        public void DownloadText(string path, Action<UnityWebRequest> callback)
        {
            routineHolder.StartCoroutine(DownloadTextRoutine(path, callback));
        }
        static IEnumerator DownloadTextRoutine(string path, Action<UnityWebRequest> callback)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();
            callback(www);
        }
        public void DownloadImage(string cid, Action<Sprite> callback)
        {
            string path = CreatePath(cid);
            routineHolder.StartCoroutine(DownloadImageRoutine(path, callback));
        }
        static IEnumerator DownloadImageRoutine(string path, Action<Sprite> callback)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                callback(null);
            }
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), UnityEngine.Vector2.one * 0.5f);
                callback(sprite);
            }
        }
        #endregion
    }
}


