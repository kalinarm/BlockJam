using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Kimeria.Nyx;

namespace Kimeria.Nyx.Tools.SceneManagement
{
    public class LoadSceneUI : MonoBehaviour
    {
        [SerializeField] GameObject activateOnStart;
        [SerializeField] GameObject activateOnFinish;
        [SerializeField] Image progressBar;
        [SerializeField] Text progressText;

        void Start()
        {
            updateObject(false);
        }

        void OnEnable()
        {
            GlobalEventManger.AddListener<Evt.SceneLoad>(OnSceneLoad);
        }

        void OnDisable()
        {
            GlobalEventManger.RemoveListener<Evt.SceneLoad>(OnSceneLoad);
        }

        void OnSceneLoad(Evt.SceneLoad evt)
        {
            switch (evt.status)
            {
                case SceneLoader.LOAD_STATUS.START:
                    updateObject(false);
                    break;
                case SceneLoader.LOAD_STATUS.PROGRESS:
                    break;
                case SceneLoader.LOAD_STATUS.PROGRESS_COMPLETE:
                    updateObject(true);
                    break;
                case SceneLoader.LOAD_STATUS.END:
                    break;
                default:
                    break;
            }
            updateProgress(evt.progress);
        }

        private void updateObject(bool finished)
        {
            if (activateOnStart != null)
            {
                activateOnStart.SetActive(!finished);
            }
            if (activateOnFinish != null)
            {
                activateOnFinish.SetActive(finished);
            }
        }

        private void updateProgress(float progress)
        {
            if (progressBar != null)
            {
                progressBar.fillAmount = progress;
            }
            if (progressText != null)
            {
                progressText.text = ((int)(progress * 100)).ToString();
            }
        }
    }
}
