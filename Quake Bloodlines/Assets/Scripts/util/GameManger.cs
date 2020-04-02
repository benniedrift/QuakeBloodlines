using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManger : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private string playerPrefab;
    [SerializeField]
    private Transform[] spawnPoints;

    #endregion

    #region Monobehaviour Callbacks

    private void Start()
    {
        Spawn();
    }

    #endregion

    #region Internal and Public methods

    internal void Spawn()
    {
        //spawns random
        Transform temp_spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        PhotonNetwork.Instantiate(playerPrefab, temp_spawn.position, temp_spawn.rotation);
    }

    #endregion
}
