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
    public class ProcessDownloadArmy : Web3Process
    {
        public ArmyData army = null;
        public string cid;
        public ProcessDownloadArmy(MonoBehaviour routineHolder, string cid) : base(routineHolder)
        {
            this.cid = cid;
        }
        public override void Start()
        {
            string path = CreatePath(this.cid);
            DeclareStart($"Trying to download json file from {path}");
            DownloadText(path, OnDownloadCompleted);
        }
        void OnDownloadCompleted(UnityWebRequest request)
        {
            CheckResponseValidity(request);
            Debug.Log(request.downloadHandler.text);
            army = JsonUtility.FromJson<ArmyData>(request.downloadHandler.text);
            DeclareSuccess($"Army downloaded from {this.cid}");
        }
    }
}


