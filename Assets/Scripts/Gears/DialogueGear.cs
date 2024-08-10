using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gears
{
    public class DialogueGear : MonoBehaviour
    {
        public GameObject dialogue1;
        public GameObject dialogue2;
        public GameObject checkbox;
        public GameObject gearsMechanism;
        public GameObject otherGears;
        public List<LoadByPlayer> sliders;
        public GameObject buttonRestart;
        
        private void OnEnable()
        {
            ScreensCounter.OnAllCount += GearsReady;
        }
        
        private void OnDisable()
        {
            ScreensCounter.OnAllCount -= GearsReady;
        }
        
        private void Start()
        {
            DialogueController.instance.NewDialogueInstance("Но и этого мало!", () =>
            {
                StartCoroutine(TimerAfterStart());
                foreach (var slider in sliders)
                {
                    slider.isInteractable = true;
                }
            });
        }

        private IEnumerator TimerAfterStart()
        {
            yield return new WaitForSeconds(4.5f);
            foreach (var slider in sliders)
            {
                slider.isInteractable = false;
            }
            
            DialogueController.instance.NewDialogueInstance("М-да, тут уже нужна бы автоматизация", () => { });
            DialogueController.instance.NewDialogueInstance("Агент, даю разрешение на открытие наладочной панели. Действуйте!", "Ок", () =>
            {
                checkbox.SetActive(true);
            });
        }

        public void CheckBoxOn(bool status)
        {
            if (status)
            {
                AudioManager.instance.ChangeClip(2);
                checkbox.SetActive(false);
                gearsMechanism.SetActive(true);
                StartCoroutine(TimerAfterCheckBoxOn());
            }
        }
        
        private IEnumerator TimerAfterCheckBoxOn()
        {
            yield return new WaitForSeconds(4f);
            DialogueController.instance.NewDialogueInstance("Этот механизм кто-то намерено [RED]саботировал[/RED]", () => { });
            DialogueController.instance.NewDialogueInstance("Но на это сейчас нет времени — надо все вернуть на круги своя!",
                () =>
                {
                    buttonRestart.SetActive(true);
                    otherGears.SetActive(true);
                });
            StopAllCoroutines();
        }

        private void GearsReady()
        {
            dialogue1.SetActive(false);
            dialogue2.SetActive(true);
            
            DialogueController.instance.NewDialogueInstance("Отлично сработано, Агент 0x2A!");
            DialogueController.instance.NewDialogueInstance("С [RED]саботажем[/RED] разберемся позже. А сейчас — нарушена система резета в одном из наших пазлов!");
            DialogueController.instance.NewDialogueInstance("Слава Линусу, что в них теперь почти никто не играет!", () => { });
            DialogueController.instance.NewDialogueInstance("Поэтому в резет нужно отправить только одну инстанцию этой программы", "ОК!", (
                () =>
                {
                    SceneManager.LoadScene("TagGame");
                }));
        }
    }
}
