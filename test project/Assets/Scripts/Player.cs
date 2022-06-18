using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f; // moovement speed
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Joystick joystick;
    private GameObject exit;
    private float distanceToExit;

    public SpriteRenderer sprite;
    public bool isMooving = false;
    public static Player Instance { get; set; }

    private void Awake()
    {
        exit = GameObject.Find("Exit");
        Instance = this;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        distanceToExit = Vector3.Distance(exit.transform.position, transform.position);
        if (distanceToExit <= 3)
        {
            exit.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        else
        {
            exit.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        if (transform.localScale.x < 2)
        {
            Die();
        }
        if (joystick.Direction != new Vector2(0, 0))
        {
            isMooving = true;
            Move();
        }
        else
            isMooving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (!winScreen.activeSelf)
            {
                GameObject weapon = GameObject.Find("Weapon");
                weapon.SetActive(false);
                winScreen.SetActive(true);
                speed = 0;
            }
        }
    }

    private void Move()
    {
        Vector3 dir = joystick.Direction;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }
    public void Die()
    {
        if (!deathScreen.activeSelf)
        {
            deathScreen.SetActive(true);
            speed = 0;
        }
        Destroy(this.gameObject);
    }
}
