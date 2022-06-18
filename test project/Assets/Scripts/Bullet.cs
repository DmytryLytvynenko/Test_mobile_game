using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Explosion explosion;
    public float speed = 30f;
    public Rigidbody2D rb;
    public float destroyTime = 5f;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", destroyTime);
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explosion expl = Instantiate(explosion, transform.position, transform.rotation) as Explosion;
            float radius = Mathf.Pow((transform.localScale.x / 2), 2);
            expl.ExplosionDamage(transform.position, radius);
            DestroyBullet();
        }
    }

    public void Shoot()
    {
        rb.velocity = dir * speed * Time.deltaTime;
    }
    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
