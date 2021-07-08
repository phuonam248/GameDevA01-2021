using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerShooting : MonoBehaviour {
    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    public Transform firePoint;
    public float bulletForce = 15f;
    private bool canFire;
    private float delayFireTimer = 0.35f;
    public float fireTimer;
    public bool isPause;
    // Start is called before the first frame update
    void Start() {
        fireTimer = delayFireTimer;
        isPause = true;
    }

    // Update is called once per frame
    void Update() {
        if (isPause)
            Fire();
    }

    private void Fire() {
        fireTimer += Time.deltaTime;
        if (fireTimer > delayFireTimer) {
            canFire = true;
        }
        if (Input.GetMouseButtonDown(0)) {
            if (canFire) {
                
                gameObject.GetComponent<AudioSource>().Play();
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            }
        }
    }
}