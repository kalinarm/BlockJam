using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo.Web3
{
    public class Web3Storage : MonoBehaviour
    {
        string CreatePath(string cid)
        {
            return NFTstorage.Helper.GenerateGatewayPath(cid, NFTstorage.Constants.GatewaysSubdomain[0], true);
        }

        public void Download(Web3StorageData data)
        {
            string path = CreatePath(data.cid);
            Debug.Log($"Download from {path}");
            StartCoroutine(DownloadText(path));
            //StartCoroutine(NFTstorage.NetworkManager.GetObjectByCid(DownloadCompleted, data.cid));
        }

        void DownloadCompleted(NFTstorage.DataResponse response)
        {
            if (!response.Success)
            {
                Debug.Log("error downloading : " + response.Error.ToString(), this);
                return;
            }
            Debug.Log("Downlad successful", this);
            foreach (var item in response.Values)
            {
                Debug.Log(item.name, this);
                Debug.Log(item.deals, this);
                Debug.Log(item.type, this);
            }
        }

        public void DownloadImage(Web3StorageData data)
        {
            string path = CreatePath(data.cid);
            Debug.Log($"Download image from {path}");
            StartCoroutine(NFTstorage.NetworkManager.DownloadImage(DownloadImageCompleted, path));
        }
        void DownloadImageCompleted(NFTstorage.DownloadResponse response)
        {
            if (!response.IsSuccess)
            {
                Debug.Log("error downloading image : " + response.ErrorMessage, this);
                return;
            }
            Debug.Log("Downlad successful", this);
            Texture2D texture = response.Texture2D;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new UnityEngine.Vector2(0.5f,0.5f));
        }


        public void Updload(Web3StorageData data, string content)
        {
            StartCoroutine(NFTstorage.NetworkManager.UploadObject(UploadCompleted, Encoding.ASCII.GetBytes(content)));
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
            if (response.Values == null)
            {
                return;
            }

            string path = CreatePath(response.Values[0].cid);
            Debug.Log($"uploaded at {path}");
        }

        public void UpdloadMetadata(NFTstorage.ERC721.NftMetaData metadata, string attachementCID)
        {
            string path = CreatePath(attachementCID);
            metadata.SetIPFS(attachementCID);
            byte[] dataToBytes = NFTstorage.Helper.ERC721MetaDataToBytes(metadata);
            StartCoroutine(NFTstorage.NetworkManager.UploadObject(UploadMetadataCompleted, dataToBytes));
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
            Debug.Log($"NFT found at {path}");
        }
        #region specific
        public void UpdloadPlayerArmy(ArmyData army)
        {
            string json = army.ToJson();
            //store the json with army content
            StartCoroutine(NFTstorage.NetworkManager.UploadObject(UploadPlayerArmyCompleted, Encoding.ASCII.GetBytes(json)));
        }
        void UploadPlayerArmyCompleted(NFTstorage.DataResponse response)
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

            string cid = response.Values[0].cid;
            //create metadata
            NFTstorage.ERC721.NftMetaData metadata = new NFTstorage.ERC721.NftMetaData("playerArmy");
            metadata.description = "army of creatures";
            UpdloadMetadata(metadata, cid);
        }

        public void UpdloadCreature(Creature creature)
        {
            if (creature == null) return;
            var entity = new EntitySpecification(creature);
            string json = entity.ToJson();
            //create and upload the image of creature
        }
        #endregion
        #region helper
        public static IEnumerator DownloadText(string path)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                yield break;
            }

            Debug.Log("downloaded : " + www.downloadHandler.text);
        }
        #endregion
    }
}


