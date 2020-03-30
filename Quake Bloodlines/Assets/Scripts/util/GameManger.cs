using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private string playerPrefab;
    [SerializeField]
    private Transform spawnPoint;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        PhotonNetwork.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
