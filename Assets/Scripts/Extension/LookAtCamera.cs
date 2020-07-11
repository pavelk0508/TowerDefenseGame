using UnityEngine;

namespace TowerDefense
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
            
        }
    }
}
