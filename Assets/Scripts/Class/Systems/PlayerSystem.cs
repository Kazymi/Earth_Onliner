using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] private LayerMask buildMask;
    [SerializeField] private LayerMask earthMask;
    private StateMachine _stateMachine;
    private BuildState _buildState;
    private UpgradeState _upgradeState;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<PlayerSystem>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<PlayerSystem>();
    }

    private void Start()
    {
        var inputHandler = ServiceLocator.GetService<InputHandler>();
        var emptyState = new EmptyState();
        _buildState = new BuildState(inputHandler,buildMask,earthMask,ServiceLocator.GetService<CameraMovementChecker>());
        _upgradeState = new UpgradeState(inputHandler,buildMask);

        _stateMachine = new StateMachine(emptyState);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public void BuildComplete()
    {
        _stateMachine.SetState(_upgradeState);
    }

    public void StartBuild(NewBuilding buildGameObject)
    {
        _buildState.Initialize(buildGameObject);
       _stateMachine.SetState(_buildState);
    }
}