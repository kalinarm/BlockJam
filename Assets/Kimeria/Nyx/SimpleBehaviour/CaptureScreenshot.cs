//#define USE_MOMENT
using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace Kimeria.Nyx.SimpleBehaviours
{
    public class CaptureScreenshot : MonoBehaviour
    {
        public KeyCode keyCapture = KeyCode.P;
        public string baseFolder = "screenshots";
        public string baseName = "game_";
        public string baseExtension = "jpg";
        public int superSampling = 1;

        public bool saveInMyDocument = false;

#if USE_MOMENT
    public KeyCode keyCaptureGif = KeyCode.O;
    public Moments.Recorder recorder;
#endif
        public static string GetUserPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        string pathFolder
        {
            get
            {
                if (saveInMyDocument)
                {
                    return Path.Combine(GetUserPath(), baseFolder);
                }
                return Application.dataPath + "/../" + baseFolder;
            }
        }

        string fileName
        {
            get
            {
                return baseName + DateTime.Now.ToString("yyyy_MM_dd__hh_mm_ss") + "." + baseExtension;
            }
        }

        string path
        {
            get
            {
                return Path.Combine(pathFolder,fileName);
            }
        }

       
        void Start()
        {
#if USE_MOMENT
        if (recorder != null)
        {
            recorder.SaveFolder = pathFolder.Substring(0, pathFolder.Length - 1);
        }
#endif
        }

        void Update()
        {
            if (Input.GetKeyDown(keyCapture))
            {
                capture();
            }
#if USE_MOMENT
        if (recorder != null && Input.GetKeyDown(keyCaptureGif))
        {
            Debug.Log("start record gif to " + recorder.SaveFolder);
            recorder.Record();
        }
#endif
        }

        public void capture()
        {
            string p = path;
            Debug.Log("exporting screenshot : " + p);
            Directory.CreateDirectory(pathFolder);
            ScreenCapture.CaptureScreenshot(p, superSampling);
        }
    }
}