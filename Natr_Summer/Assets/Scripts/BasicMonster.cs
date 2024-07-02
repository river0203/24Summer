using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonster : Mob
{
    private int     _nextMoveDir = 1;
    private int     _beforeMoveDir;
    private float   _thintTime = 0f;
    private float   _damage;
    private float   _hp;
    private float   _moveSpeed;
    private Rigidbody2D _rigid;
    private RaycastHit  _checkGround;

    // Start is called before the first frame update
    void Start()
    {
        _rigid= GetComponent<Rigidbody2D>();
        think();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    public override void attack(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override int hit(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void move()
    {
        _nextMoveDir = think();
        _rigid.velocity = new Vector2(_nextMoveDir, _rigid.velocity.y);
    }

    public int think()
    {
        _thintTime += Time.deltaTime;  

        if(_thintTime > 5f)
        {
            _nextMoveDir = Random.Range(-1, 2);
            _beforeMoveDir = _nextMoveDir;
            _thintTime = 0;
        }
        return _nextMoveDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Mob : Attack");
        }
    }
}
