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
    public class ProcessDownloadNFT : Web3Process
    {
        public string cidNFT;
        public NFTstorage.ERC721.NftMetaData metadata;
        public Sprite sprite;

        public ProcessDownloadNFT(MonoBehaviour routineHolder, string cid) : base(routineHolder)
        {
            this.cidNFT = cid;
        }
        public override void Start()
        {
            //string path = CreatePath(this.cidNFT);
            string path = this.cidNFT;
            DeclareStart($"Download metadata");
            Debug.Log($"Download Metadata on {path}");
            DownloadText(path, OnDownloadCompleted);
        }
        void OnDownloadCompleted(UnityWebRequest request)
        {
            if (!CheckResponseValidity(request)) return;
            Debug.Log(request.downloadHandler.text);
            metadata = JsonUtility.FromJson<NFTstorage.ERC721.NftMetaData>(request.downloadHandler.text);
            
            //getimage path
            string pathImage = CreatePath(metadata.image);
            CurrentStep = "Downloading Image";
            Debug.Log($"download image from {pathImage}");

            DownloadImage(pathImage, ImageDownloaded);
        }

        void ImageDownloaded(Sprite sprite)
        {
            this.sprite = sprite;
            if (sprite == null)
            {
                DeclareFailed("Cannot download image");
                return;
            }
            DeclareSuccess($"NFT metatadata downloaded");
        }
    }
}


