using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject fireObject;
    [SerializeField] GameObject gunRootObject;
    float fireTimer = 0f;
    float cooldownTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && fireTimer > cooldownTime)
        {
            Fire();
            fireTimer = 0f;
        }
        fireTimer += Time.deltaTime;
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, fireObject.transform.position, gunRootObject.transform.rotation);
    }
}
