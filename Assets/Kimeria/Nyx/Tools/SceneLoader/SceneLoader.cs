using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Kimeria.Nyx.Tools.SceneManagement
{
    namespace Evt
    {
        public class SceneLoad : IEvent
        {
            public SceneLoader.LOAD_STATUS status;
            public float progress = 0;
            public SceneLoad(SceneLoader.LOAD_STATUS _status)
            {
                this.status = _status;
            }
            public SceneLoad(float _progress)
            {
                this.status = SceneLoader.LOAD_STATUS.PROGRESS;
                this.progress = _progress;
            }
        }
    }
    /// <summary>
    /// static method to call load scene async
    /// add it to a new scene called LoadingScene by default to have a loading scene
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        public enum LOAD_STATUS
        {
            START,
            PROGRESS,
            PROGRESS_COMPLETE,
            END
        }
        protected static string _sceneToLoad = "";
        public static string LoadingScreenSceneName = "LoadingScene";

        protected AsyncOperation _asyncOperation;
        protected float _fillTarget = 0f;

        public LoadSceneUI uiProgress;

        /// <summary>
        /// Call this static method to load a scene from anywhere
        /// </summary>
        /// <param name="sceneToLoad">Level name.</param>
        public static void LoadScene(string sceneToLoad, string loadingSceneName)
        {
            _sceneToLoad = sceneToLoad;
            Application.backgroundLoadingPriority = ThreadPriority.High;
            string loadingScene = string.IsNullOrEmpty(loadingSceneName) ? LoadingScreenSceneName : loadingSceneName;
            SceneManager.LoadScene(loadingScene);
        }

        public static void LoadScene(string sceneToLoad)
        {
            LoadScene(sceneToLoad, LoadingScreenSceneName);
        }


        void Start()
        {
            if (!string.IsNullOrEmpty(_sceneToLoad))
            {
                StartCoroutine(LoadAsynchronously());
            }
        }

        /// <summary>
		/// Loads the scene to load asynchronously.
		/// </summary>
		protected virtual IEnumerator LoadAsynchronously()
        {
            UpdateProgress(LOAD_STATUS.START);

            // we start loading the scene
            _asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
            _asyncOperation.allowSceneActivation = false;

            // while the scene loads, we assign its progress to a target that we'll use to fill the progress bar smoothly
            while (_asyncOperation.progress < 0.9f)
            {
                _fillTarget = _asyncOperation.progress;
                UpdateProgress(LOAD_STATUS.PROGRESS, _fillTarget);
                yield return null;
            }
            // when the load is close to the end (it'll never reach it), we set it to 100%
            _fillTarget = 1f;


            // the load is now complete, we replace the bar with the complete animation
            UpdateProgress(LOAD_STATUS.PROGRESS_COMPLETE);
            

            // we switch to the new scene
            _asyncOperation.allowSceneActivation = true;
            UpdateProgress(LOAD_STATUS.END);
        }

        void UpdateProgress(LOAD_STATUS state, float fill = 0)
        {
            if (state == LOAD_STATUS.PROGRESS)
            {
                Kimeria.Nyx.GlobalEventManger.Trigger(new Evt.SceneLoad(fill));
            }
            else
            {
                Kimeria.Nyx.GlobalEventManger.Trigger(new Evt.SceneLoad(state));
            }
        }
    }
}