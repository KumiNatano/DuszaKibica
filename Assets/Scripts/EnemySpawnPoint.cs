using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Enemy prefab => _prefab;
    public Enemy instance => _instance;
    public bool hasInstance => _instance != null;

    public void Respawn()
    {
        if (hasInstance)
        {
            Destroy(instance.gameObject);
            _instance = null;
        }
        _instance = Instantiate(prefab, transform.position, Quaternion.identity, null);
    }

    [SerializeField] Enemy _prefab;
    [SerializeField] Enemy _instance;
}
