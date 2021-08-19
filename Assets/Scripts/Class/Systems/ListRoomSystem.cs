using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRoomSystem
{
    private Transform _roomListContent;
    private GameObject _roomListItemPrefab;
    private Launcher _launcher;
    
    public ListRoomSystem(Transform roomListContent, GameObject roomListItemPrefab)
    {
        _roomListContent = roomListContent;
        _roomListItemPrefab = roomListItemPrefab;
        _launcher = ServiceLocator.GetService<Launcher>();
        _launcher.OnRoomListUpdateAction += OnRoomListUpdate;
    }
    
    private void OnRoomListUpdate()
    {
        foreach (Transform trans in _roomListContent)
        {
            GameObject.Destroy(trans.gameObject);
        }

        foreach (var roomInfo in _launcher.NewRoom)
        {
            if (roomInfo.RemovedFromList)
            {
                continue;
            }

            GameObject.Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomInfo);
        }
    }
}