
    using EventBusSystem;
    using UnityEngine;

    public class UpgradeState : State
    {
        private LayerMask _idLayer;
        private InputHandler _inputHandler;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            EventBus.RaiseEvent<IBuildEvent>(h => h.OnUpgrade());
        }

        public UpgradeState(InputHandler inputHandler, LayerMask layerMask)
        {
            _idLayer = layerMask;
            _inputHandler = inputHandler;
        }
        public override void MouseDrag()
        {
            var hit = _inputHandler.GetHitPoint(_idLayer);
            if(hit.collider == null || _inputHandler.MoveVector != Vector3.zero) return;
            var upgrader = hit.collider.GetComponent<Upgrader>();
            if (upgrader == false)
            {
                return;
            }
            upgrader.MouseDrag();
        }
    }