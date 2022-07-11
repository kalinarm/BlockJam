using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace Evo
{
    public class TutorialPanel : UIGamePanel
    {
        [System.Serializable]
        public class TutorialStep
        {
            //public string title;
            [Multiline]
            public string description;
            public GameObject pointer;
            public bool setPosition = true;
        }

        public int currentIndex = 0;
        public TutorialStep[] steps;

        [Header("Refs")]
        public Transform player;
        public TMP_Text texDescription;
        public TMP_Text texPage;
        public UnityEngine.UI.Button butPrevious;
        public UnityEngine.UI.Button butNext;

        public Fx fxChangeStep;

        TutorialStep currentStep;

        const string tutorial_key = "tuto";
        const string tutorial_index = "tuto_index";

        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteKey(tutorial_key);
            PlayerPrefs.DeleteKey(tutorial_index);
        }

        private void Start()
        {
            butPrevious.onClick.AddListener(DecrementStep);
            butNext.onClick.AddListener(IncrementStep);

            ChangeStep(0);

            if (PlayerPrefs.HasKey(tutorial_index))
            {
                int index = PlayerPrefs.GetInt(tutorial_index);
                ChangeStep(index);
            }else
            {
                ChangeStep(0);
            }

            if (PlayerPrefs.HasKey(tutorial_key))
            {
                if (PlayerPrefs.GetInt(tutorial_key) > 0)
                {
                    Close();
                }
            }
        }

        void SetPlayerPosition(Transform target)
        {
            Vector3 p = player.position;
            p.x = target.position.x;
            player.position = p;
        }

        protected override void Opened()
        {
            PlayerPrefs.SetInt(tutorial_key, 0);
            ChangeStep(currentIndex);
        }
        protected override void Closed()
        {
            CloseStep(currentStep);
            PlayerPrefs.SetInt(tutorial_key, 1);
        }

        public void OpenTutorial()
        {

        }
        public void CloseTutorial()
        {
            Close();
        }

        void ChangeStep(int index)
        {
            currentIndex = index;
            CloseStep(currentStep);
            currentStep = steps[index];
            OpenStep(currentStep);

            butPrevious.gameObject.SetActive(index > 0);
            butNext.gameObject.SetActive(index < steps.Length - 1);
            texPage.text = $"{(currentIndex + 1).ToString()} / {steps.Length}";
        }
        private void CloseStep(TutorialStep step)
        {
            if (step == null) return;
            if (step.pointer != null)
            {
                step.pointer.SetActive(false);
            }
        }
        private void OpenStep(TutorialStep step)
        {
            texDescription.text = step.description;
            if (step.pointer != null)
            {
                step.pointer.SetActive(true);
                if (step.setPosition)
                {
                    SetPlayerPosition(step.pointer.transform);
                }
            }
            fxChangeStep?.Trigger();
        }

        public void IncrementStep()
        {
            if (currentIndex >= steps.Length -1)
            {
                CloseTutorial();
                return;
            }
            ChangeStep(++currentIndex);
        }
        public void DecrementStep()
        {
            if (currentIndex <= 0)
            {
                CloseTutorial();
                return;
            }
            ChangeStep(--currentIndex);
        }
    }
}

