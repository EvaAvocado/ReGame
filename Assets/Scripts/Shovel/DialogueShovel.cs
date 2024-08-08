using System;
using LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shovel
{
    public class DialogueShovel : MonoBehaviour
    {
        public GameObject slider;
        public Shovel shovel;
        
        private void OnEnable()
        {
            ScreensCounter.OnAllCount += Ready;
        }

        private void OnDisable()
        {
            ScreensCounter.OnAllCount -= Ready;
        }
        
        private void Start()
        {
            DialogueController.instance.NewDialogueInstance("Поэтому, Агент 0x2A, вот Вам лопата!");
            DialogueController.instance.NewDialogueInstance("Копайте!");
            DialogueController.instance.NewDialogueInstance("За мир во всем Игромире! ");
            DialogueController.instance.NewDialogueInstance("Вперед!");
            DialogueController.instance.NewDialogueInstance("А вернее — вниз!");
            DialogueController.instance.NewDialogueInstance("Да пребудет с Вами масса помноженная на ускорение!", "Копать? Я не хочу копать!", "Так точно, Сэр!", IDontWantDigging, Digging);
        }

        private void IDontWantDigging()
        {
            DialogueController.instance.NewDialogueInstance("Агент, а напомните мне — Вы сразу открыли мое письмо или вначале отказались?", "Да, я рвусь в бой!", "Нет, я расстроил Тыковку", IDontWantDiggingYES, IDontWantDiggingNO);
        }

        private void IDontWantDiggingYES()
        {
            DialogueController.instance.NewDialogueInstance("Тогда тем более идите и копайте!", Digging);
        }

        private void IDontWantDiggingNO()
        {
            DialogueController.instance.NewDialogueInstance("Тогда вам ли не знать, что выбор — это иллюзия?", "Готов копать!", "Всегда копать!", Digging, Digging);
        }

        private void Digging()
        {
            slider.SetActive(true);
            shovel.isCanDigging = true;
            DialogueController.instance.NewDialogueInstance("Вперед! Кликайте!", () =>
            {
                
            });
        }
        
        private void Ready()
        {
            SceneManager.LoadScene("Exit");
        }
    }
}
