using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Enemy prefab => _prefab;
    public Enemy instance => _instance;
    public bool hasInstance => _hasInstance;

    public void Respawn()
    {
        if (hasInstance)
        {
            Destroy(instance.gameObject);
            _instance = null;
            _hasInstance = false;
        }
        _instance = Instantiate(prefab, transform.position, Quaternion.identity, null);
        _hasInstance = true;
    }

    [SerializeField] bool _hasInstance = false;
    [SerializeField] Enemy _prefab;
    [SerializeField] Enemy _instance = null;
}
