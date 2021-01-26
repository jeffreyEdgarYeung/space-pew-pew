using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField][Range(0,50)] float moveSpeed = 10f;
    [SerializeField] float xPadding, yPadding;
    [SerializeField] float health = 300;
    [SerializeField] GameObject deathAnimation;
    [SerializeField] GameObject damagedAnimation;
    [SerializeField] float damagedDuration;
    [SerializeField] AudioClip deathSFX;
    [Range(0, 1)] [SerializeField] float deathSFXVolume = 0.7f;
    

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileRate = 1f;                  // Bullets/second
    [SerializeField] AudioClip projectileSFX;
    [Range(0, 1)] [SerializeField] float projectileSFXVolume = 0.7f;

    float xMin, xMax, yMin, yMax;

    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos =Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void SetUpBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            Vector3 laserPos = transform.position;
            laserPos.z = 1;
            GameObject laser = Instantiate
            (
               laserPrefab,
               laserPos,
               Quaternion.identity
            ) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileSFXVolume);
            yield return new WaitForSeconds(1/projectileRate);
        }
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            ProcessHit(damageDealer);
            damageDealer.Hit();
        }
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0){ 
            Die(); 
        }
        else
        {
            StartCoroutine(ShowDamagedSprite());
        }
        
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        FindObjectOfType<Level>().LoadGameOver();
        FindObjectOfType<MusicPlayer>().StopMusic();
    }

    IEnumerator ShowDamagedSprite()
    {
        GameObject dSprite = Instantiate(damagedAnimation, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(damagedDuration);
        Destroy(dSprite);
    }

}
