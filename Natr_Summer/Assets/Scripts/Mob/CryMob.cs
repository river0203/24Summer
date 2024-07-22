using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryMob : Mob
{
    private int _nextMoveDir = 1;
    private int _beforeMoveDir;
    private int _hp = 3;
    private float _thintTime = 0f;
    private float normalMoveSpeed = 4f;
    private float backMoveSpeed = 10f;
    private float followDist = 10f;
    private float attackDist = 3f;

    private Rigidbody2D rb;
    private Transform targe;
    private Vector2 backPosition = new Vector2(16, 0);
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

        if(_hp <= 0)
        {
            dead(this.gameObject);
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
            //transform.position = Vector2.MoveTowards(transform.position, backPosition, backMoveSpeed * Time.deltaTime);
            move();
        }
    }

    public override void attack()
    {
        if (Vector2.Distance(transform.position, targe.position) < attackDist)
        {
            Debug.Log("Mob : Attack");
        }
    }

    public override void hit()
    {
        Debug.Log("Mob : Hit");
        if (_hp <= 0)
        {
            dead(this.gameObject);
        }
        _hp -= 1; // 플레이어의 공격 정보를 받아와야함
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon"))
        {
            hit();
        }
    }
}
