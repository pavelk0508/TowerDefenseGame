using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TowerDefense
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public Transform FinishObject;

        public int HP = 100;

        public int Gold = 1000;

        public Slider HPIndicator;

        public Text GoldField;

        public int MaxHP;

        public int CountKills = 0;

        public UnityEvent OnHPEqualZero;

        private void Awake()
        {
            Instance = this;
            MaxHP = HP;
        }

        private void Update()
        {
            if (HP <= 0)
            {
                OnHPEqualZero?.Invoke();
                return;
            }

            CheckEnemyCollision();

            UpdateUI();
        }

        private void UpdateUI()
        {
            HPIndicator.maxValue = MaxHP;
            HPIndicator.value = HP;

            GoldField.text = $"${Gold}";
        }

        private void CheckEnemyCollision()
        {
            var raycastHits = Physics.SphereCastAll(FinishObject.transform.position, 0.1f, Vector3.up);

            if (raycastHits == null || raycastHits.Length == 0)
            {
                return;
            }

            var enemies = raycastHits.ToList().FindAll(a => a.collider.gameObject.GetComponentInParent<EnemyBase>());
            if (enemies == null || enemies.Count == 0)
            {
                return;
            }

            foreach (var enemy in enemies)
            {
                var enemyBase = enemy.collider.GetComponentInParent<EnemyBase>();
                HP -= enemyBase.Damage;
                Destroy(enemyBase.gameObject);
            }
        }

        public void UpgradeTower(TowerBase tower)
        {
            var preset = tower.GetLevelPreset(tower.Level + 1);
            if (Gold - preset.Gold < 0)
            {
                return;
            }

            Gold -= preset.Gold;
            tower.Upgrade();
        }
    }
}
