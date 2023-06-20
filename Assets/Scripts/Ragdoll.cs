using UnityEngine;
using System.Linq;

public class Ragdoll : MonoBehaviour
{
    Animator animator;

    public void Activate()
    {
        animator.enabled = false;
        GetComponentsInChildren<Rigidbody>().ToList().ForEach(x => x.isKinematic = false);
        GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = true);
    }
    public void Deactivate()
    {
        animator.enabled = true;
        GetComponentsInChildren<Rigidbody>().ToList().ForEach(x => x.isKinematic = true);
        GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = false);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
