using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
   [SerializeField] private List<TurretConfiguration> turretConfigurations;
   [SerializeField] private int amountAmmo = 15;
   private Dictionary<TurretConfiguration, Factory> _turretsFactories;

   private void OnEnable()
   {
      ServiceLocator.Subscribe<AmmoManager>(this);
   }

   private void OnDisable()
   {
      ServiceLocator.Unsubscribe<AmmoManager>();
   }

   private void Start()
   {
      _turretsFactories = new Dictionary<TurretConfiguration, Factory>();
      foreach (var turret in turretConfigurations)
      {
         _turretsFactories.Add(turret,new Factory(turret.AmmoGameObject,amountAmmo,transform));
      }
   }

   public GameObject GetAmmoByTurretType(TurretConfiguration turretConfiguration)
   {
    var returnValue = _turretsFactories[turretConfiguration].Create();
    var ammo =returnValue.GetComponent<IAmmo>();
    if (ammo != null)
    {
       ammo.ParentFactory = _turretsFactories[turretConfiguration];
    }
    return returnValue;
   }
}
