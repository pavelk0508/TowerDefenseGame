using UnityEngine;

namespace TowerDefense
{
    public class PathContainer : MonoBehaviour
    {
        public Vector3[] PointList;

        private void OnDrawGizmos()
        {
            if (PointList == null || PointList.Length == 0)
            {
                return;
            }

            foreach(var point in PointList)
            {
                Gizmos.DrawSphere(point, 1f);
            }
        }      
    }
}
