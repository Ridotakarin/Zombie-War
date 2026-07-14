using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] private GameObject explosionVFX;

    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 15f;
    [SerializeField] private float upwardsModifier = 1f;

    [Header("Affected Layers")]
    [SerializeField] private LayerMask affectedLayers = ~0;

    private bool exploded;

    private void OnCollisionEnter(Collision collision)
    {
        if (exploded)
            return;
        exploded = true;
        Explode();
    }

    public void Explode()
    {
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }

        //AudioManager.Instance?.Explosion();

        Collider[] victims = Physics.OverlapSphere(transform.position,explosionRadius,affectedLayers);

        foreach (Collider victim in victims)
        {
            if (victim.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(100);
            }
            if (victim.attachedRigidbody != null)
            {
                victim.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius,upwardsModifier,ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}