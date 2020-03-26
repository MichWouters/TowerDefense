using Assets.Scripts.Tower;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class SelectionPlaneManager : MonoBehaviour
    {
        public Vector3 SelectionPlanePosition { get; private set; }

        private GameObject _gameController;
        private TowerManager _towerManager;
        private GameManager _playerManager;

        private Ray _ray;
        private RaycastHit _hit;
        private int _layerMask;

        private void Start()
        {
            _gameController = GameObject.Find(Tags.GameController);

            _towerManager = _gameController.GetComponent<TowerManager>();
            _playerManager = _gameController.GetComponent<GameManager>();
            _layerMask = 1 << 8;
        }

        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            const float raydistance = 250.0f;

            // Track selection plane
            if (!Physics.Raycast(_ray, out _hit, raydistance, _layerMask))
            {
                return;
            }

            Collider selectedTile = _hit.collider;
            SelectionPlanePosition = new Vector3(selectedTile.transform.position.x, transform.position.y, selectedTile.transform.position.z);
            transform.position = SelectionPlanePosition;
            
            if (selectedTile.tag == Tags.Buildable && Input.GetKeyDown(KeyCode.Mouse0))
            {
                ShowBuildMenu(_towerManager.Cannon, selectedTile);
            }
        }

        private void ShowBuildMenu(Tower.Tower tower, Collider other)
        {
            // TODO: Show Build menu
            if (CanPlaceTower(tower.GetPrice()))
            {
                BuyTower(tower, other);
            }
        }

        private void BuyTower(Tower.Tower tower, Collider other)
        {
            _towerManager.PlaceTower(SelectionPlanePosition, tower);
            _playerManager.UpdateCoins(_towerManager.Cannon.GetPrice(), Operator.Subtract);

            MarkAsOccupied(other);
        }

        private void MarkAsOccupied(Collider other)
        {
            other.tag = Tags.Unbuildable;
        }

        private bool CanPlaceTower(int price)
        {
            if (_playerManager.CanPurchaseTower(price))
            {
                return true;
            }

            Debug.Log("You do not have enough money.");
            return false;
        }
    }
}