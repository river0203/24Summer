using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryMob : Mob
{
    private int _nextMoveDir = 1;
    private int _beforeMoveDir;
    private float _thintTime = 0f;
    [SerializeField]
    private float normalMoveSpeed = 4f;
    private float followDist = 10f;

    private Rigidbody2D rb;
    private Transform targe;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targe = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targe == null || targe != null)
        {
            think();
        }
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, targe.position) < followDist)
        {
            FollowTarget();
        }
        else
        {
            move();
        }
    }
    public override void move()
    {
        _nextMoveDir = think();
        rb.velocity = new Vector2(_nextMoveDir, rb.velocity.y * normalMoveSpeed * Time.deltaTime);
    }
    public int think()
    {
        _thintTime += Time.deltaTime;

        if (_thintTime > 5f)
        {
            _nextMoveDir = Random.Range(-1, 2);
            _beforeMoveDir = _nextMoveDir;
            _thintTime = 0;
        }
        return _nextMoveDir;
    }
    public void FollowTarget()
    {
        if (Vector2.Distance(transform.position, targe.position) < followDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, targe.position, /*normalMoveSpeed **/ Time.deltaTime);
        }
        else
        {
            move();
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
}
