using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{
    public abstract class TowerBase : MonoBehaviour
    {
        public string Name;

        public string Description;

        public UpgradingPreset[] UpgradingPresets;

        public int Level = 0;

        public int Damage = 1;

        public float ShootSpeed = 1f;

        public float RadiusShooting = 1f;

        private RaycastHit[] _listTargetHits;

        protected IEnumerable<EnemyBase> _listEnemies;

        private float _currentShootingTime = 0f;

        public EnemyBase DetectedEnemy
        {
            get
            {
                return _listEnemies == null || _listEnemies.Count() == 0 ?
                    null :
                    _listEnemies.First();

            }
        }

        private void FixedUpdate()
        {
            DetectEnemies();
        }

        private void Update()
        {
            Shoot();
        }

        private void DetectEnemies()
        {
            _listTargetHits = Physics.SphereCastAll(transform.position, RadiusShooting, Vector3.down);

            if (_listTargetHits == null || _listTargetHits.Length == 0)
            {
                return;
            }

            var targets = _listTargetHits.ToList().FindAll(a => a.collider != null && a.collider.GetComponentInParent<EnemyBase>());

            _listEnemies = targets.Select(a => a.collider?.GetComponentInParent<EnemyBase>());

            if (_listEnemies.Count() != 0)
            {
                _listEnemies.OrderBy(a =>
                    Vector3.Distance(transform.position, a.transform.position));
            }
        }

        protected virtual void Shoot()
        {
            if (!DetectedEnemy)
            {
                _currentShootingTime = 0;
                return;
            }

            _currentShootingTime += Time.deltaTime;

            if (_currentShootingTime >= 1f / ShootSpeed)
            {
                _currentShootingTime = 0;
                DetectedEnemy.HP -= Damage;
            }
        }

        public virtual void Upgrade()
        {
            Level++;

            var preset = GetLevelPreset(Level);

            Damage += preset.Damage;
        }

        public UpgradingPreset GetLevelPreset(int level)
        {
            return UpgradingPresets[Mathf.Clamp(level + 1, 0, UpgradingPresets.Length - 1)];
        }
    }
}
