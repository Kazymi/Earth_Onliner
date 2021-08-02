using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GeneratorResource : MonoBehaviour
{
    [SerializeField] private List<ResourceGenerate> resourceGenerates;

    private PlayerResources _playerResources;
    private Dictionary<ResourceGenerate, float> _timerStartTime;
    private PhotonView _photonView;
    public void Start()
    {
        _playerResources = ServiceLocator.GetService<PlayerResources>();
        _timerStartTime = new Dictionary<ResourceGenerate, float>();
        foreach (var resourceGenerate in resourceGenerates)
        {
            _timerStartTime.Add(resourceGenerate, resourceGenerate.GenerateTimer);
        }
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (_photonView.ViewID == 0) return;
        if(_photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber) Destroy(this);
        foreach (var resourceGenerate in resourceGenerates)
        {
            resourceGenerate.GenerateTimer -= Time.deltaTime;
            if (resourceGenerate.GenerateTimer <= 0)
            {
                resourceGenerate.GenerateTimer = _timerStartTime[resourceGenerate];
                _playerResources.AddResource(resourceGenerate.GenerationResource.TypeResource,
                    resourceGenerate.GenerationResource.Amount);
            }
        }
    }
}