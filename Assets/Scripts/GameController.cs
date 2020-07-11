using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    public class GameController : MonoBehaviour
    {
        public GamePreset Preset;

        private string _pathToData = "config.json";

        public DemoEnemiesController DemoEnemiesController;

        public PlayerController PlayerController;

        private void Awake()
        {
            var pathToSettings = Path.Combine(Application.persistentDataPath, _pathToData);

            try
            {
                Preset = JsonConvert.DeserializeObject<GamePreset>(File.ReadAllText(pathToSettings));
            }    
            catch(Exception ex)
            {
                Debug.LogWarning("Error loading settings. Loaded default");
                Preset = new GamePreset();
                File.WriteAllText(pathToSettings, JsonConvert.SerializeObject(Preset));
            }

            SetupPreset();

            DemoEnemiesController.StartGenerating();
        }

        private void SetupPreset()
        {
            DemoEnemiesController.CountOffsetInWave = Preset.OffsetGeneratesEnemies;
            DemoEnemiesController.TimeBetweenWaves = Preset.TimeBetweenWaves;

            PlayerController.HP = PlayerController.MaxHP = Preset.StartPlayerHP;
            PlayerController.Gold = Preset.StartPlayerGold;

        }


    }
}
