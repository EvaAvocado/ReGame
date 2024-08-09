using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EvMeshPro.Scripts.Dialogues
{
    public class StartDialogue : MonoBehaviour
    {
        public GameObject bigDialogue;
        public GameObject bigDialogue2;
        public GameObject bigDialogue3;
        public GameObject bigDialogue4;
        public GameObject topDialogue;
        public GameObject bottomDialogue;
        public Image cat1;
        public Image cat2;
        
        private void Start() 
        {
            DialogueController.instance.NewDialogueInstance("Агент 0x2A,\nВам письмо от Шеймуса Утки!\n\nОткрыть?", "Да!", "Нет...", () => OpenLetter0(), () => NotOpenLetter());
        }

        private void OpenLetter0()
        {
            cat1.gameObject.SetActive(false);
            cat2.gameObject.SetActive(false);
            
            bigDialogue.SetActive(false);
            bigDialogue2.SetActive(false);
            topDialogue.SetActive(false);
            bottomDialogue.SetActive(false);
            bigDialogue3.SetActive(true);

            AudioManager.instance.ChangeClip(1);
            //GetComponent<AudioSource>().Play();     
            
            DialogueController.instance.NewDialogueInstance("Приветствую, Агент 0x2A.");
            DialogueController.instance.NewDialogueInstance("Во время летних каникул и отпусков количество пользователей существенно возросло. Штатные сотрудники не справляются");
            DialogueController.instance.NewDialogueInstance("Мы даже не успеваем [WAVE]ВОЗВРАЩАТЬ ИГРЫ В ИСХОДНОЕ СОСТОЯНИЕ![/WAVE] А открывать игру и видеть, что ее уже кто-то прошел — совсем не весело.");
            DialogueController.instance.NewDialogueInstance("Мы срочно нуждаемся в Вашей помощи!");
            DialogueController.instance.NewDialogueInstance("От этого зависит судьба Игромира.");
            DialogueController.instance.NewDialogueInstance("Искренне Ваш,\nШ. Утка,\nДиректор Федерального Бюро Контроль+Альт+Делита", "ОК", () => FirstTask());
        }

        private void OpenLetter()
        {
            cat1.gameObject.SetActive(false);
            cat2.gameObject.SetActive(false);
            
            bigDialogue.SetActive(false);
            bigDialogue2.SetActive(false);
            topDialogue.SetActive(false);
            bottomDialogue.SetActive(false);
            bigDialogue3.SetActive(true);
            
            DialogueController.instance.NewDialogueInstance("Ну вот и славненько!\nОткрываю письмо от Шеймуса Утки.", () => OpenLetter0());
        }

        private void FirstTask()
        {
            bigDialogue3.SetActive(false);
            bigDialogue4.SetActive(true);
            
            DialogueController.instance.NewDialogueInstance("Ваше первое задание:\n\nЗагрузить загрузку!");
            DialogueController.instance.NewDialogueInstance("Работа нелегкая, поэтому вначале — небольшой разогрев.\n\nНачнем с экрана одного пользователя.", "ОК", () => OpenBlueSausage());
        }

        private void OpenBlueSausage()
        {
            SceneManager.LoadScene("LoadingScreen");
            //DialogueController.instance.NewDialogueInstance("Мы вверяем в Ваши руки легендарную [BLUE]синюю колбасу[/BLUE]\n\nСейчас ее делают из избыточных запасов экранов смерти, оставшихся после концов света 2000-го и 2012-го годов. Иногда добавляют щепотку Нортон Коммандера для красоты.");
        }

        private void NotOpenLetter()
        {
            DialogueController.instance.NewDialogueInstance("Вы уверены?", "ДА", "Нет", () => NotOpenLetter2(), () => OpenLetter());
        }

        private void NotOpenLetter2()
        {
            DialogueController.instance.NewDialogueInstance("Точно уверены?", "ДА!!!", "Нет", () => NotOpenLetter3(), () => OpenLetter());
        }

        private void NotOpenLetter3()
        {
            bigDialogue.SetActive(false);
            bigDialogue3.SetActive(false);
            bottomDialogue.SetActive(true);

            cat1.gameObject.SetActive(true);
            DialogueController.instance.NewDialogueInstance("Но тогда Тыква будет плакать! Посмотрите какая она хорошенькая!");
            DialogueController.instance.NewDialogueInstance("Расстроить Тыкву?", "ДА, я ненавижу милых кошечек", "НЕТ!!!", () => CryCat(), () => OpenLetter());
        }

        private void CryCat()
        {
            bigDialogue3.SetActive(false);
            
            cat1.gameObject.SetActive(false);
            cat2.gameObject.SetActive(true);
            
            DialogueController.instance.NewDialogueInstance("Ну вот, Тыковка плачет.");
            DialogueController.instance.NewDialogueInstance("Разве так можно?", "Ну, а что поделать?", "Нет.", () =>  AfterCryCat(), () => OpenLetter());
        }

        private void AfterCryCat()
        {
            topDialogue.SetActive(false);
            bottomDialogue.SetActive(false);
            bigDialogue3.SetActive(false);
            bigDialogue2.SetActive(true);
            
            cat2.gameObject.SetActive(false);
            
            DialogueController.instance.NewDialogueInstance("Ну и то верно...");
            DialogueController.instance.NewDialogueInstance("Стоп! Мне казалось, что это я задавал вопросы, а не наоборот.", "Вы уверены?", () => print(10));
            DialogueController.instance.NewDialogueInstance("Да... Нет... Не знаю.\nЯ уже ничего не знаю.\nВсе так странно сегодня.", "И что Вы будете с этим делать?", () => print(20));
            DialogueController.instance.NewDialogueInstance("Ммм, не уверен. Заварю чаёк, поглажу кошку, поиграю во что-нибудь немного. Может быть почту проверю. Вроде неплохой план?", "Да, неплохой", "Нет, не плохой", () =>  OpenLetter(), () => OpenLetter());
        }
    }
}
