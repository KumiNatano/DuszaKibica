using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Ragdoll : MonoBehaviour
{
    Animator animator;

    public void Activate()
    {
        animator.enabled = false;
        rigidbodies.ForEach(x => x.isKinematic = false);
        colliders.ForEach(x => x.enabled = true);
    }
    public void Deactivate()
    {
        animator.enabled = true;
        rigidbodies.ForEach(x => x.isKinematic = true);
        colliders.ForEach(x => x.enabled = false);
    }
    public void Push(Vector3 force)
    {
        rigidbodies.ForEach(x => x.AddForce(force));
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        colliders = GetComponentsInChildren<Collider>().ToList();
    }

    List<Rigidbody> rigidbodies;
    List<Collider> colliders;
}
