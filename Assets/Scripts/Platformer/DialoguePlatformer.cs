using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class DialoguePlatformer : MonoBehaviour
    {
        public GameObject dialogueBottom;
        public GameObject dialogueBig;
        public GameObject characters;
        public GameObject platformSpawner;
        
        private void OnEnable()
        {
            PointsInPlatformer.OnAllStarsCollected += AllStartCollected;
        }
        
        private void OnDisable()
        {
            PointsInPlatformer.OnAllStarsCollected -= AllStartCollected;
        }
        
        private void Start()
        {
            DialogueController.instance.NewDialogueInstance("По Вашему левому клику мыши будут появляться платформы");
            DialogueController.instance.NewDialogueInstance("Помогите этим беспутным игрокам, которым лишь бы попрыгать с края, собрать все звезды!", "ОК.", (() =>
            {
                platformSpawner.SetActive(true);   
                characters.SetActive(true);
            }));
        }

        private void AllStartCollected()
        {
            dialogueBottom.SetActive(false);
            dialogueBig.SetActive(true);

            characters.SetActive(false);
            platformSpawner.SetActive(false);
            
            DialogueController.instance.NewDialogueInstance("Вы молодец, Агент 0x2A!");
            DialogueController.instance.NewDialogueInstance("Не зря я, Шеймус Утка, выбрал именно Вас для этого задания! Вы сделали так, что игроки снова могут наслаждаться своими играми!");
            DialogueController.instance.NewDialogueInstance("Но мы всё ещё в опасности");
            DialogueController.instance.NewDialogueInstance(
                "То что Бюро Контроль+Альт+Делита подверглось [RED]саботажу[/RED] — дурной знак.\nИ мы обязаны докопаться до истины как можно скорее!",
                "ОК!", LoadNextLevel);
            
        }

        private void LoadNextLevel()
        {
            SceneManager.LoadScene("Shovel");
        }
    }
}
