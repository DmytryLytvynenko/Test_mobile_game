using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isInfected = false;
    private float dieTime = 2f;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (isInfected)
        {
            sprite.color = Color.gray;
            Invoke("Die", dieTime);
            isInfected = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.Die();
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isInfected = true;
        }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
