using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Kimeria.Nyx;

namespace Evo
{
    public class BattleEndPanel : UIGamePanel
    {
        [SerializeField] TMP_Text texTitle;
        [SerializeField] TMP_Text texContent;

        [SerializeField] Fx fxLoose;
        [SerializeField] Fx fxWinStart;
        [SerializeField] Fx fxWinGold;
        [SerializeField] Fx fxStartStep;

        [SerializeField] GameObject lineGold;
        [SerializeField] GameObject[] rewardGold;
        [SerializeField] GameObject buttons;

        public void SetupLoose()
        {
            texTitle.text = "You Lose !";
            texContent.text = string.Empty;

            Reset();
            Open();
            StartCoroutine(routineLoose());
        }
        public void SetupWin(Reward reward)
        {
            texTitle.text = "You win !";
            texContent.text = string.Empty;

            Reset();
            Open();
            StartCoroutine(routineWin(reward));
        }
        public void Configure(string text)
        {
            texTitle.text = text;
            Open();
        }

        private void Reset()
        {
            lineGold?.SetActive(false);
            buttons?.SetActive(false);
            foreach (var item in rewardGold)
            {
                item.SetActive(false);
            }
        }
        IEnumerator routineWin(Reward reward)
        {
            WaitForSeconds wait = new WaitForSeconds(0.8f);
            WaitForSeconds waitSmall = new WaitForSeconds(0.5f);
            yield return wait;

            fxWinStart?.Trigger();
            yield return wait;

            //gold time
            fxStartStep?.Trigger();
            lineGold?.SetActive(true);
            for (int i = 0; i < reward.gold; i++)
            {
                rewardGold[i].SetActive(true);
                fxWinGold?.Trigger();
                yield return waitSmall;
            }

            yield return wait;
            fxStartStep?.Trigger();
            buttons?.SetActive(true);
        }

        IEnumerator routineLoose()
        {
            WaitForSeconds wait = new WaitForSeconds(0.8f);
            yield return wait;

            fxLoose?.Trigger();
            yield return wait;

            fxStartStep?.Trigger();
            buttons?.SetActive(true);
        }
    }
}

