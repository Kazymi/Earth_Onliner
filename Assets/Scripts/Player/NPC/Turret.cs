using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretConfiguration turretConfiguration;
    [SerializeField] private Transform startPositionAmmo;

    private float _currentTimer;
    private Transform lookAtTransform;
    private AmmoManager _ammoManager;
    private bool _isMine;
    private void Start()
    {
        _ammoManager = ServiceLocator.GetService<AmmoManager>();
    }

    public void RotateToTransform(Transform position)
    {
        lookAtTransform = position;
    }
    private void Update()
    {
        if (lookAtTransform != null)
        {
            Fire();
            var lookDir = lookAtTransform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    public void Initialize(bool isMine)
    {
        _isMine = isMine;
    }
    private void Fire()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer < 0)
        {
            _currentTimer = turretConfiguration.FireRate;
            var newAmmo = _ammoManager.GetAmmoByTurretType(turretConfiguration);
            newAmmo.transform.position = startPositionAmmo.position;
            newAmmo.transform.rotation = startPositionAmmo.rotation;
            if (_isMine)
            {
                newAmmo.gameObject.layer = LayerMask.NameToLayer(LayerType.Friendly.ToString());
            }
            else
            {
                newAmmo.gameObject.layer = LayerMask.NameToLayer(LayerType.Enemy.ToString());
            }
        }
    }
}