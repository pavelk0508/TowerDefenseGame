using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        public GameObject LastSelectedObject;

        public UnityEventByGameObj OnSelectedObjChange;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null)
            {
                OnSelectedObjChange?.Invoke(LastSelectedObject);
            }
        }

        private void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                LastSelectedObject = hit.collider.gameObject;
            }
        }
    }

}