using Items.Gears;
using UnityEngine;

namespace Gears
{
    public class RestartGears : MonoBehaviour
    {
        public Gear[] gears;
        public PlaceToGear[] places;
        
        public void Restart()
        {
            foreach (var gear in gears)
            {
                gear.transform.position = gear.startPos;
                gear.GetComponent<CircleCollider2D>().isTrigger = false;
                gear.rb.freezeRotation = false;

                foreach (var place in places)
                {
                    place.DeleteGearOutPlace(gear);
                }
            }
        }
    }
}
