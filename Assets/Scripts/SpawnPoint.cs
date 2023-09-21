using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public SpawnPointInfo spawnInfo;

    public void SpawnPlayer()
    {
        Player.main.SetPosition(transform.position + spawnInfo.position);
        Player.main.living.Revive();
    }
}

[Serializable]
public struct SpawnPointInfo
{
    public Vector3 position;
    public float angle;
}
