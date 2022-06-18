using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radiuss;
    public void ExplosionDamage(Vector3 center, float radius)
    {
        radiuss = radius;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<Enemy>().isInfected = true;
            }
        }
        Destroy(this.gameObject);
    }
}
