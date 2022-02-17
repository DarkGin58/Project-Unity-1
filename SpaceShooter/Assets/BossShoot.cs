using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);

    public GameObject bulletPrefab;

    public float fireDelay = 0.50f;
    float cooldownTimer = 0;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            Debug.Log("Enemy Pew!");
            cooldownTimer = fireDelay;

            Vector3 offset = transform.rotation * bulletOffset;

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y+0.7f), transform.rotation);
   
            GameObject bulletGO2 = (GameObject)Instantiate(bulletPrefab, new Vector2(transform.position.x+ 0.6f, transform.position.y + 1f), transform.rotation);
            GameObject bulletGO3 = (GameObject)Instantiate(bulletPrefab, new Vector2(transform.position.x - 0.6f, transform.position.y + 1f), transform.rotation);
            bulletGO.layer = gameObject.layer;
        }
    }
}
