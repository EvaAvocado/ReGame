using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingScreen
{
    public class DialogueLoading : MonoBehaviour
    {
        public LoadingSliderDialogue slider;

        private void OnEnable()
        {
            LoadingSliderDialogue.OnReadySlider += Ready;
        }
        
        private void OnDisable()
        {
            LoadingSliderDialogue.OnReadySlider -= Ready;
        }

        private void Start()
        {
            DialogueController.instance.NewDialogueInstance("Мы вверяем в Ваши руки легендарную [BLUE]синюю колбасу[/BLUE]");
            DialogueController.instance.NewDialogueInstance("Сейчас ее делают из избыточных запасов экранов смерти, оставшихся после концов света 2000-го и 2012-го годов");
            DialogueController.instance.NewDialogueInstance("Иногда добавляют щепотку Нортон Коммандера для красоты");
            DialogueController.instance.NewDialogueInstance("Давайте вначале посмотрим как она должна двигаться", "OK", () => slider.StartSlider(Study));
        }

        private void Study()
        {
            DialogueController.instance.NewDialogueInstance("Будто остановилась. Может проверить? Попробуйте подвести курсор к грани");
        }

        private void Ready()
        {
            DialogueController.instance.NewDialogueInstance("Она всегда так делает. Поскольку она собрана из разнородных элементов, [BLUE]синяя колбаса[/BLUE] — нелинейна", "OK", () => LoadNextScene());
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene("LoadGame");
        }
    }
}
