using System;
using System.Collections;
using Core;
using LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Shovel
{
    public class DialogueShovel : MonoBehaviour
    {
        public GameObject slider;
        public Shovel shovel;
        public Animator exitAnimator;
        public VideoPlayer player;

        private float _videoDuration;
        
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
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "video.mp4");
            
            AudioManager.instance.OffMusic();
            DialogueController.instance.NewDialogueInstance("Поэтому, Агент 0x2A, вот Вам лопата!");
            DialogueController.instance.NewDialogueInstance("Копайте!");
            DialogueController.instance.NewDialogueInstance("За мир во всем Игромире! ");
            DialogueController.instance.NewDialogueInstance("Вперед!\nА вернее — вниз!");
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
            DialogueController.instance.NewDialogueInstance("Кликайте, Агент!\n", () =>
            {
                
            });
        }
        
        private void Ready()
        {
            exitAnimator.Play("exit");
        }

        public void EndAnimation()
        {
            shovel.isCanDigging = false;
            
            player.gameObject.SetActive(true);
            player.Play();
            print(player.clip.length);
            
            _videoDuration = (float) player.clip.length;

            StartCoroutine(TimerToGoStartGame());
        }

        private IEnumerator TimerToGoStartGame()
        {
            yield return new WaitForSeconds(_videoDuration);
            SceneManager.LoadScene("StartScene");
        }
    }
}
