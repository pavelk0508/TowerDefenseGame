using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class AboutTowerPanel : MonoBehaviour
    {
        public Text NameField;

        public Text DamageField;

        public Text DescriptionField;

        private TowerBase _selectedTower; 

        public void OnSelected(GameObject obj)
        {
            var tower = obj.GetComponentInParent<TowerBase>();
            if (tower == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
            _selectedTower = tower;
                               
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            NameField.text = $"Name : {_selectedTower.Name}. Lvl: {_selectedTower.Level}";

            DescriptionField.text = _selectedTower.Description;

            DamageField.text = $"Damage: {_selectedTower.Damage}";
        }

        public void UpgradeClick()
        {
            PlayerController.Instance.UpgradeTower(_selectedTower);    
        }
    }
}
