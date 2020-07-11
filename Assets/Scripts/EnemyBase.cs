using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public int HP = 3;

        public int Damage = 1;

        public int CountGold = 100;

        public PathContainer Path;

        public int CurrentPointIndex = 0;

        public float MovingSpeed = 0.1f;

        public float DistanceThreshold = 0.01f;

        public float RotationCoef = 5f;

        public Slider HPIndicator;

        private int _maxHP;

        private void Awake()
        {
            _maxHP = HP;
        }

        private void Update()
        {
            if (HP <= 0)
            {
                Die();
            }

            if (Time.timeScale == 0f)
            {
                return;
            }

            UpdateUI();

            Move();

            CheckPoint();
        }

        private void UpdateUI()
        {
            if (HPIndicator == null)
            {
                return;
            }

            HPIndicator.maxValue = _maxHP;
            HPIndicator.value = HP;
        }

        private void Move()
        {
            if (CurrentPointIndex >= Path.PointList.Length)
            {
                return;
            }

            var directionVector = Path.PointList[CurrentPointIndex] - transform.position;
            transform.forward = Vector3.Slerp(transform.forward, directionVector, Time.deltaTime * RotationCoef);
            transform.Translate(Vector3.forward * MovingSpeed);
        }

        private void CheckPoint()
        {
            if (CurrentPointIndex >= Path.PointList.Length)
            {
                return;
            }

            if (Vector3.Distance(transform.position, Path.PointList[CurrentPointIndex]) < DistanceThreshold)
            {
                CurrentPointIndex++;
            }
        }

        protected virtual void Die()
        {
            PlayerController.Instance.Gold += CountGold;

            PlayerController.Instance.CountKills++;

            Destroy(this.gameObject);
        }
    }
}
