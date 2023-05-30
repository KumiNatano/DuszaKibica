using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    public float hitDamage = 5f;
    public float flySpeed = 2f;
    public float flownDistance = 0f;
    public float maxFlyDistance = 10f;
    public Vector3 flyDirection = Vector3.forward;
    public Vector3 hitboxSize = Vector3.one;
    public Vector3 hitboxOffset = Vector3.zero;

    public LayerMask damageLayers = Physics.DefaultRaycastLayers;
    public LayerMask groundedLayers = Physics.DefaultRaycastLayers;
    public float groundedDistance => flySpeed * Time.fixedDeltaTime + 0.025f;

    RaycastHit[] hits = new RaycastHit[8];
    bool dirty = false;


    public void SetDirection(Vector3 newDir)
    {
        flyDirection = newDir.normalized;
        transform.rotation = Quaternion.LookRotation(flyDirection);
    }

    void Fly()
    {
        float w = flySpeed * Time.fixedDeltaTime;
        transform.position += flyDirection * w;
        flownDistance += w;
    }
    void DealDamage()
    {
        int c = Physics.BoxCastNonAlloc(transform.position + Quaternion.LookRotation(flyDirection) * hitboxOffset, hitboxSize*0.5f, flyDirection, hits, Quaternion.identity, 0, damageLayers);
        for (int i = 0; i < c; i++)
        {
            HealthSystem hs;
            Transform t = hits[i].transform;
            // remove magic string
            if (t.TryGetComponent(out hs) && !IsPlayer(t)){
                hs.TakeDamage(Mathf.RoundToInt(hitDamage));
                dirty = true;
                break;
            } 
        }
    }
    void EnsureNotDirty()
    {
        if (dirty)
        {
            Destroy(gameObject);
        }
    }
    void CheckGround()
    {
        int c = Physics.RaycastNonAlloc(transform.position, flyDirection, hits, groundedDistance, damageLayers);
        bool sp = false;
        for (int i = 0; i < c; i++)
        {
            if (!sp)
            {
                sp = IsPlayer(hits[i].transform);
            }
        }
        if (!sp && c > 0)
        {
            dirty = true;
        }
    }
    void CheckOverfly()
    {
        if (flownDistance >= maxFlyDistance)
        {
            dirty = true;
        }
    }
    bool IsPlayer(Transform t)
    {
        return t.CompareTag("player");
    }

    void FixedUpdate()
    {
        EnsureNotDirty();
        CheckGround();
        Fly();
        CheckOverfly();
        DealDamage();
    }
}
