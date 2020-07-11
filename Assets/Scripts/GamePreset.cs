using System;

namespace TowerDefense
{
    [Serializable]
    public class GamePreset
    {
        public float TimeBetweenWaves = 5f;

        public int StartPlayerHP = 10;

        public int StartPlayerGold = 100;

        public int OffsetGeneratesEnemies = 4;

    }
}
