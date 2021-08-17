
    using EventBusSystem;
    using UnityEngine;

    public class UpgradeState : State
    {
        private LayerMask _idLayer;
        private InputHandler _inputHandler;

        public override void OnStateEnter()
        {
            _inputHandler.OnMouseDownAction += OnMouseDown;
        }

        public override void OnStateExit()
        {
            _inputHandler.OnMouseDownAction -= OnMouseDown;
        }

        public UpgradeState(InputHandler inputHandler, LayerMask layerMask)
        {
            _idLayer = layerMask;
            _inputHandler = inputHandler;
        }
        private void OnMouseDown()
        {
            var hit = _inputHandler.GetHitPoint();
            if(hit.collider == null || _inputHandler.MoveVector != Vector3.zero) return;
            var upgrader = hit.collider.GetComponent<Upgrader>();
            if (upgrader != null)
            {
                upgrader.OnMouseDownAction();
            }

            var spawnArmy = hit.collider.GetComponent<ArmySpawner>();
            if (spawnArmy)
            {
                spawnArmy.OnClick();
            }
        }
    }