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
    public class ProcessUploadArmy : Web3Process
    {
        ArmyData army;
        public string cidData;
        public string cidMetadata;
        public ProcessUploadArmy(MonoBehaviour routineHolder, ArmyData army) : base(routineHolder)
        {
            this.army = army;
        }
        public override void Start()
        {
            DeclareStart($"Trying to upload your army");
            string json = army.ToJson();
            UploadJson(json, OnFileUploaded);
        }

        void OnFileUploaded(NFTstorage.DataResponse response)
        {
            if (!CheckResponseValidity(response)) return;
            if (!CheckResponseHasValues(response)) return;

            cidData = response.Values[0].cid;
            Debug.Log($"json file uploaded with cid {cidData}");
            CurrentStep = "Uploading metadata";

            NFTstorage.ERC721.NftMetaData metadata = new NFTstorage.ERC721.NftMetaData("playerArmy");
            metadata.description = "army of creatures";
            var att = new NFTstorage.ERC721.Attribute();
            att.trait_type = "Count";
            att.value = army.entities.Count(x=>x.dna.Length>0).ToString();
            metadata.attributes = new List<NFTstorage.ERC721.Attribute>();
            metadata.attributes.Add(att);

            UpdloadMetadata(metadata, cidData, OnMetadataUploaded);
        }

        void OnMetadataUploaded(NFTstorage.DataResponse response)
        {
            if (!CheckResponseValidity(response)) return;
            if (!CheckResponseHasValues(response)) return;

            cidMetadata = response.Values[0].cid;
            string path = CreatePath(cidMetadata);
            DeclareSuccess($"NFT uploaded at {cidMetadata}");
        }
    }
}


