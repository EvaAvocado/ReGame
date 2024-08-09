using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip one;
        public AudioClip two;
        public AudioClip three;
        public AudioClip four;
        public AudioClip five;
        public AudioSource audioSource;

        public static AudioManager instance = null;
        
        void Start () {
            
            if (instance == null) { 
                instance = this; 
            } else if(instance == this){ 
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
            
            InitializeManager();
        }
        
        private void InitializeManager()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void ChangeClip(int number)
        {
            switch (number)
            {
                case 1:
                    audioSource.clip = one;
                    break;
                case 2:
                    audioSource.clip = two;
                    break;
                case 3:
                    audioSource.clip = three;
                    break;
                case 4:
                    audioSource.clip = four;
                    break;
                case 5:
                    audioSource.clip = five;
                    break;
            }
            
            audioSource.Play();
        }

        public void OffMusic()
        {
            audioSource.Stop();
        }
    }
}
