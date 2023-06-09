using UnityEngine;

public class PlayerItemPickuper : PlayerModule
{
    public const int checkRate = 24;
    public Vector3 checkOffset;
    public float checkRadius = 0.5f;
    public LayerMask checkLayers = Physics.DefaultRaycastLayers;

    Collider[] cols = new Collider[16];

    public override void OnInit()
    {
        InvokeRepeating(nameof(Check), 0, 1f/checkRate);
    }

    void Check()
    {
        int c = Physics.OverlapSphereNonAlloc(GetCheckPosition(), checkRadius, cols, checkLayers);
        for (int i = 0; i < c; i++)
        {
            DropItem dropItem;
            if (cols[i].TryGetComponent(out dropItem))
            {
                DoPickup(dropItem);
            }
        }
    }
    void DoPickup(DropItem dropItem)
    {
        dropItem.OnPickup(parent);
        int ci = dropItem.pickupSounds.Length;
        ci = Random.Range(0, ci);
        AudioSource.PlayClipAtPoint(dropItem.pickupSounds[ci], parent.playerCamera.GetPosition());
    }
    Vector3 GetCheckPosition()
    {
        return parent.GetPosition() + Quaternion.Euler(0, parent.playerCamera.viewAngles.y, 0) * checkOffset;
    }
}
