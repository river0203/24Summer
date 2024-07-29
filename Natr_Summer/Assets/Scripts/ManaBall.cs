using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ManaBall : MonoBehaviour
{
    private float launchPower = 10f;
    private float dir;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        for(int i = 0; i < 3; i++)
        {
            Fire();

        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Fire()
    {
        dir = UnityEngine.Random.Range(-2, 2);
        Vector2 force = this.transform.position * launchPower * Time.deltaTime * dir;
        _rb.AddForce(force, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
