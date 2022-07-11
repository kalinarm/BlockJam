using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo.Web3
{
    public class Web3StorageOld : MonoBehaviour
    {
        public Web3StorageData data;

        [SerializeField]
        SetMetaData setMetadata;

        [SerializeField]
        NFTstorage.ERC721.NftMetaData metadata;
        
        [EditorButton]
        void TestUpload()
        {
            GeneticCode c = new GeneticCode();
            c.Randomize(10);
            Store(c);
        }
        [EditorButton]
        void TestDownload()
        {
            Download();
        }

        [EditorButton]
        public void TakeScreenshot()
        {
            NFTstorage.Helper.TakeScreenShot(CallbackScreenshot);
        }

        string CreatePath(string cid)
        {
            return NFTstorage.Helper.GenerateGatewayPath(cid, NFTstorage.Constants.GatewaysSubdomain[0], true);
        }

        void CallbackScreenshot(byte[] bytes)
        {
            StartCoroutine(NFTstorage.NetworkManager.UploadObject(UploadCompleted2, bytes));
        }

        public void Store(GeneticCode code)
        {
            StartCoroutine(NFTstorage.NetworkManager.UploadObject(UploadCompleted2, code.bytes));
        }
        public void Download()
        {
            string path = CreatePath(data.cid);
            StartCoroutine(NFTstorage.NetworkManager.GetObjectByCid(DownloadCompleted, data.cid));
        }

        void UploadCompleted(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log($"error uploading ", this);
                if (response.Error != null && response.Error.message != null) Debug.Log($"error : {response.Error.message.ToString()}", this);
                return;
            }
            Debug.Log("Upload successful", this);
        }

        void UploadMetadataCompleted(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log($"error uploading metadata", this);
                if (response.Error != null && response.Error.message != null) Debug.Log($"error : {response.Error.message.ToString()}", this);
                return;
            }

            Debug.Log("Upload metadata successful", this);
            if (response.Values == null)
            {
                return;
            }

            string path = CreatePath(response.Values[0].cid);
            Debug.Log($"found at {path}");
        }

        void DownloadCompleted(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log("error downloading : " + response.Error.ToString(), this);
                return;
            }

            Debug.Log("Downlad successful", this);

        }

        void UploadCompleted2(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log($"error uploading ", this);
                if (response.Error != null && response.Error.message != null) Debug.Log($"error : {response.Error.message.ToString()}", this);
                return;
            }

            Debug.Log("Upload successful", this);
            if (response.Values == null)
            {
                return;
            }

            string path = CreatePath(response.Values[0].cid);
            Debug.Log($"found at {path}");

            metadata.SetIPFS(response.Values[0].cid);
            byte[] dataToBytes = NFTstorage.Helper.ERC721MetaDataToBytes(metadata);
            StartCoroutine(NFTstorage.NetworkManager.UploadObject(UploadMetadataCompleted, dataToBytes));

        }

        [EditorButton]
        void GetAllObjects()
        {
            StartCoroutine(NFTstorage.NetworkManager.GetAllObjects(GetAllObjectsCallback));
        }
        void GetAllObjectsCallback(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log($"error GetAllObjects ", this);
                if (response.Error != null && response.Error.message != null) Debug.Log($"error : {response.Error.message.ToString()}", this);
                return;
            }

            Debug.Log("GetAllObjects successful", this);
            if (response.Values == null)
            {
                return;
            }

            string path = CreatePath(response.Values[0].cid);
            Debug.Log($"value[0] {response.Values[0]}");

        }
    }
}


