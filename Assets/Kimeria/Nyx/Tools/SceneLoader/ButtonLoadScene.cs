using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Kimeria.Nyx.Tools.SceneManagement
{
    public class ButtonLoadScene : MonoBehaviour
    {
        public enum SCENE_LOADER
        {
            UNITY,
            NYX
        }
        public SCENE_LOADER mode = SCENE_LOADER.NYX;
        public string sceneToLoad;
        public string loadingScene;

        public void LoadScene()
        {
            LoadScene(sceneToLoad);
        }

        public void LoadScene(string sceneName)
        {
            switch (mode)
            {
                case SCENE_LOADER.UNITY:
                    SceneManager.LoadScene(sceneToLoad);
                    break;
                case SCENE_LOADER.NYX:
                    SceneLoader.LoadScene(sceneToLoad, loadingScene);
                    break;
                default:
                    break;
            }
        }

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}