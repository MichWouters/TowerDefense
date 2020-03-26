using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class TowerManager : MonoBehaviour
    {
        public Tower Cannon;
        public Tower IceTower;
        public Tower FireTower;

        public void PlaceTower(Vector3 position, Tower tower)
        {
            Quaternion rotation = new Quaternion();
            Tower towerToPlace = null;

            switch (tower.GetTowerType())
            {
                case TowerType.Cannon:
                    towerToPlace = Cannon;
                    break;
                case TowerType.Ice:
                    towerToPlace = IceTower;
                    break;
                case TowerType.Fire:
                    towerToPlace = FireTower;
                    break;
                default:
                    Debug.LogError("Tower Type not supported");
                    break;
            }

            Instantiate(towerToPlace, position, rotation);
        }
    }
}