using UnityEngine;

namespace Tag
{
    public class GearInTag : MonoBehaviour
    {
        public void PlayAnim()
        {
            GetComponent<Animator>().Play("fall");
        }
    }
}
