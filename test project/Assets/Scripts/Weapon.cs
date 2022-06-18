using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float offset = 0f;
    [SerializeField] private float multiplier = 0.0f;
/*    [SerializeField] private Transform parent;*/
    private float chargeSpeed = 0.5f;
    private Bullet bulletClone;
    private bool isCharging = false;
    private float dieTime = 3f;
    private float time = 0f;
    private float playerSize;

    public Transform firePoint;
    public Bullet bullet;
    public float timeShot;
    public float startTime;

    public static Weapon Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    private void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (!Player.Instance.isMooving)
        {
            FireTimer();
        }
    }
    private void FireTimer()
    { 
        if (Input.GetButton("Fire1"))  
        {
            if (!isCharging)
                CreateBullet();
            else
            {
                if (multiplier < 0.3f)
                {
                    multiplier += (chargeSpeed * Time.deltaTime);
                    bulletClone.transform.localScale = new Vector3(multiplier * playerSize + 1, multiplier * playerSize + 1, 0);
                    Player.Instance.transform.localScale = new Vector3((1 - multiplier) * playerSize, (1 - multiplier) * playerSize, 0);
                }
                else
                {
                    Player.Instance.sprite.color = Color.red;
                    time += Time.deltaTime;
                    if (time >= dieTime)
                    {
                        bulletClone.DestroyBullet();
                        Player.Instance.Die();
                    }
                }
            }
        }
        if (Input.GetButtonUp("Fire1"))  
        {
            Player.Instance.sprite.color = Color.yellow;
            if (bulletClone != null)
                Shoot();
            timeShot = startTime;
            multiplier = 0;
        }
    }
    private void CreateBullet()
    {
        playerSize = Player.Instance.transform.localScale.x;
        bulletClone = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
        isCharging = true;
    }
    private void Shoot()
    {
/*        Player.Instance.transform.localScale *= (1 -  multiplier);*/
        bulletClone.Shoot();
        isCharging = false;
    }
}
