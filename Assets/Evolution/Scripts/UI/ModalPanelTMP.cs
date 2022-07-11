using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
namespace Kimeria.Nyx.UI
{
    public class ModalPanelTMP : MonoBehaviour
    {
        public TMP_Text tex_question;
        public Image iconImage;
        public Button yesButton;
        public Button noButton;
        public Button cancelButton;
        public GameObject modalPanelObject;

        private static ModalPanelTMP modalPanel;

        public static ModalPanelTMP Instance()
        {
            if (!modalPanel)
            {
                modalPanel = GameObject.FindObjectOfType<ModalPanelTMP>(true);
                if (!modalPanel)
                    Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
            }

            return modalPanel;
        }

        void Awake()
        {
            if (modalPanel == null)
            {
                modalPanel = this;
            }
            ClosePanel();
        }

        void OnEnable()
        {
            transform.SetAsLastSibling();
        }

        // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
        public void ShowInfo(string info, UnityAction yesEvent)
        {
            modalPanelObject.SetActive(true);

            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(yesEvent);
            yesButton.onClick.AddListener(ClosePanel);

            this.tex_question.text = info;

            if (iconImage != null) this.iconImage.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(true);
            if (noButton != null) noButton.gameObject.SetActive(false);
            if (cancelButton != null) cancelButton.gameObject.SetActive(false);
        }

        public void Choice(string question, UnityAction yesEvent, UnityAction noEvent)
        {
            modalPanelObject.SetActive(true);

            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(yesEvent);
            yesButton.onClick.AddListener(ClosePanel);

            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(noEvent);
            noButton.onClick.AddListener(ClosePanel);

            this.tex_question.text = question;

            if (iconImage != null) this.iconImage.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
            if (cancelButton != null) cancelButton.gameObject.SetActive(false);
        }

        // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
        public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent)
        {
            modalPanelObject.SetActive(true);

            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(yesEvent);
            yesButton.onClick.AddListener(ClosePanel);

            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(noEvent);
            noButton.onClick.AddListener(ClosePanel);

            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(cancelEvent);
            cancelButton.onClick.AddListener(ClosePanel);

            this.tex_question.text = question;

            if (iconImage != null) this.iconImage.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
        }

        void ClosePanel()
        {
            modalPanelObject.SetActive(false);
        }

        public static void ShowMessage(string info, UnityAction yesEvent)
        {
            Instance().ShowInfo(info, yesEvent);
        }
        public static void ShowMessage(string info, UnityAction yesEvent, UnityAction noEvent)
        {
            Instance().Choice(info, yesEvent, noEvent);
        }
    }
}

