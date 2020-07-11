using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class GameOverWindow : MonoBehaviour
    {
        public Text KillCountText;

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        public void ShowWindow()
        {
            gameObject.SetActive(true);
            KillCountText.text = $"Kills : {PlayerController.Instance.CountKills}";
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
