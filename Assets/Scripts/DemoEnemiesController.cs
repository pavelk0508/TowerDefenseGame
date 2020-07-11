using System.Collections;
using UnityEngine;

namespace TowerDefense
{
    public class DemoEnemiesController : MonoBehaviour
    {
        public EnemyBase EnemyBasePrefab;

        public int NumWave = 0;

        public float TimeBetweenWaves = 5f;

        public int CountOffsetInWave = 3;

        public Transform PointSpawn;

        public PathContainer Path;

        public void StartGenerating()
        {
            StartCoroutine(Generate());
        }

        private IEnumerator Generate()
        {
            NumWave = 0;

            while (true)
            {
                for (int i = 0; i < NumWave + CountOffsetInWave; i++)
                {
                    var enemy = Instantiate(EnemyBasePrefab, PointSpawn.position, Quaternion.identity);
                    enemy.Path = Path;
                    enemy.Damage += NumWave * 2;
                    enemy.HP += NumWave ;

                    yield return new WaitForSeconds(1f);
                }

                NumWave++;
                yield return new WaitForSeconds(TimeBetweenWaves);
            }

        }
    }
}
