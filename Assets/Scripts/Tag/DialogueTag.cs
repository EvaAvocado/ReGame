using System;
using System.Collections;
using NULLcode_Studio._15Puzzle.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tag
{
    public class DialogueTag : MonoBehaviour
    {
        public GameObject solution;
        public GameObject dialogueLeft;
        public GameObject dialogueLeft2;

        public GameObject dialogueBottom;
        public GameObject dialogueBottom2;
        public GameObject dialogueBottom3;

        public GearInTag gear;

        public Button restartButton;

        private bool _isGetSolution;
        private bool _isSabotage;

        public static event Action<bool> OnTagGame;

        private void OnEnable()
        {
            ElementWithGear.OnReadyElement += Sabotage;
            GameControl.OnReady += Ready;
        }

        private void OnDisable()
        {
            ElementWithGear.OnReadyElement -= Sabotage;
            GameControl.OnReady -= Ready;
        }

        private void Start()
        {
            DialogueController.instance.NewDialogueInstance("Вот и головоломка!");
            DialogueController.instance.NewDialogueInstance(
                "Что тут у нас? Английская H? Русская Н? Дядюшка Рисёч их разберет!");
            DialogueController.instance.NewDialogueInstance("Вам нужно вернуть ее в разобранное состояние");
            DialogueController.instance.NewDialogueInstance(
                "Кликайте на элементы рядом с пустыми ячейками для их перемещения");
            DialogueController.instance.NewDialogueInstance("Не буду мешать", "ОК", () =>
            {
                OnTagGame?.Invoke(true);
                restartButton.interactable = true;

                StartCoroutine(TimerToGetSolution());
            });
        }

        public void Sabotage()
        {
            //print(111);

            _isSabotage = true;

            dialogueLeft2.SetActive(true);
            dialogueLeft.SetActive(false);
            dialogueBottom.SetActive(false);
            dialogueBottom2.SetActive(false);
            dialogueBottom3.SetActive(false);

            OnTagGame?.Invoke(false);
            restartButton.interactable = false;

            StopAllCoroutines();

            DialogueController.instance.NewDialogueInstance("Что это?");
            DialogueController.instance.NewDialogueInstance("Шестеренка с экранов загрузки!");
            DialogueController.instance.NewDialogueInstance(
                "Тут побывал наш [RED]саботажник[/RED]! Это повод для тревоги");
            DialogueController.instance.NewDialogueInstance("Продолжайте пока работу, Агент!", "OK!", () =>
            {
                gear.PlayAnim();
                OnTagGame?.Invoke(true);
                restartButton.interactable = true;
                if (!_isGetSolution) StartCoroutine(TimerToGetSolution());
                _isSabotage = false;
            });
        }

        private IEnumerator TimerToGetSolution()
        {
            yield return new WaitForSeconds(5f);
            if (!_isGetSolution && !_isSabotage)
            {
                _isGetSolution = true;
                GiveSolutionQuest();
            }
        }

        private void GiveSolutionQuest()
        {
            dialogueBottom2.SetActive(true);
            dialogueLeft2.SetActive(false);
            dialogueLeft.SetActive(false);
            dialogueBottom.SetActive(false);
            dialogueBottom3.SetActive(false);

            OnTagGame?.Invoke(false);
            restartButton.interactable = false;

            DialogueController.instance.NewDialogueInstance(
                "Ах, точно, я же не показал как именно надо ее разобрать!", (() =>
                {
                    solution.SetActive(true);

                    DialogueController.instance.NewDialogueInstance(
                        "Вот нотариально заверенный скриншот из старой документации", () =>
                        {
                            restartButton.interactable = true;
                            OnTagGame?.Invoke(true);
                        });
                }));
        }

        private void Ready()
        {
            dialogueLeft.SetActive(false);
            dialogueBottom.SetActive(true);

            DialogueController.instance.NewDialogueInstance("Прекрасно! Мы на финишной прямой", () =>
            {
                DialogueController.instance.NewDialogueInstance(
                    "След [RED]саботажа[/RED] ведет нас дальше. Стали пропадать элементы уровней, и некоторые игры теперь [WAVE]непроходимы[/WAVE]!",
                    () => SceneManager.LoadScene("Platformer"));
            });
        }
    }
}