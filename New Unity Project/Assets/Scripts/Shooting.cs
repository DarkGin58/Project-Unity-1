using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform posBull;
    public float bulletForce = 10.0f;

    public int maxAmmo = 0;
    public int currentAmmo;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    public float reloadTime = 2;
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if(isReloading)
        {
            return;
        }
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            --currentAmmo;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, posBull.position, Quaternion.identity);
        bulletObj.transform.forward = posBull.forward;
        //bulletObj.transform.position = posBull.transform.position + posBull.transform.forward;
        bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * bulletForce, ForceMode.Impulse);
       
    }
}
