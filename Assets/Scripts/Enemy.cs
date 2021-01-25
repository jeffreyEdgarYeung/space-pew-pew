using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCountDown;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject deathAnimation;

    [Header("Enemy Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        shotCountDown = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCountDown -= Time.deltaTime;
        if( shotCountDown <= 0f) { 
            Fire();
            shotCountDown = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate
        (
            laserPrefab,
            transform.position,
            laserPrefab.transform.rotation
        ) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if( damageDealer )
        {
            ProcessHit(damageDealer);
            damageDealer.Hit();
        }
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
        }

    }
}
