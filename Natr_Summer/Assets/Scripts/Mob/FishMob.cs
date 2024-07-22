using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMob : Mob
{
    private Rigidbody2D rb;
    private Transform targe;

    private float moveSpeed = 3f;
    private float contactDistance = 2f;
    private float followDist = 10f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targe = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }
    public void FollowTarget()
    {
        if(Vector2.Distance(transform.position, targe.position) < followDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, targe.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public override void attack()
    {
        throw new System.NotImplementedException();
    }

    public override void hit()
    {
        throw new System.NotImplementedException();
    }

    public override void move()
    {
        throw new System.NotImplementedException();
    }

}
