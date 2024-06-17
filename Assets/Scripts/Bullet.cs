using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float bulletSpeed;
    int hitCount = 0;
    [SerializeField] int maxHitCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float a;
    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * bulletSpeed * Time.deltaTime;
        a = rb.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            hitCount++;
            if(hitCount >= maxHitCount)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
