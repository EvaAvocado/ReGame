using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LoadingScreen
{
    public class Dialogue2Loading : MonoBehaviour
    {
        public ScreensCounter screensCounter;
        public GameObject startSlider;
        public GameObject secondSliders4;
        public LoadByPlayer slider1;
        public LoadByPlayer[] sliders4;

        private int _readyCount;
        
        private void OnEnable()
        {
            ScreensCounter.OnAllCount += PlayerReady;
        }
        
        private void OnDisable()
        {
            ScreensCounter.OnAllCount -= PlayerReady;
        }
        
        private void Start()
        {
            DialogueController.instance.NewDialogueInstance("Теперь попробуйте сами!");
            DialogueController.instance.NewDialogueInstance("Используйте массу помноженную на ускорение, Люк...");
            DialogueController.instance.NewDialogueInstance("Эээ, в смысле просто кликайте по полоске.", "ОК", () => slider1.isInteractable = true);
        }

        private void PlayerReady()
        {
            _readyCount++;
            switch (_readyCount)
            {
                case 1:
                    Phrase1();
                    break;
                case 3: 
                    Phrase2();
                    break;
            }
        }

        private void Phrase1()
        {
            DialogueController.instance.NewDialogueInstance("Уверен, что игрок доволен!", NewEpisode1);
        }
        
        private void NewEpisode1()
        {
            screensCounter.neededCount = 4;
            screensCounter.count = 0;
            screensCounter.isReady = false;
            startSlider.SetActive(false);
            secondSliders4.SetActive(true);
            
            DialogueController.instance.NewDialogueInstance("Но их у нас тысячи! Надо поднабрать обороты!", () =>
            {
                foreach (var slider in sliders4)
                {
                    slider.isInteractable = true;
                }
            });
        }

        private void Phrase2()
        {
            DialogueController.instance.NewDialogueInstance("Феноменально!", NewEpisode2);
        }
        
        private void NewEpisode2()
        {
            SceneManager.LoadScene("Gears");
            //DialogueController.instance.NewDialogueInstance("Но и этого мало!");
        }
    }
}
