using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{
    private int     _hp = 3;
    private string  _moveDir;
    private float   _basicSpeed = 8;
    private float   _basicJumpForce = 8;
    private float   _jumpTime = 0f;
    private float   _jumpTimeLimit = 0.1f;
    private bool    _isJump;

    private Vector3 mPosition;
    private Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public string getMoveDir() { return _moveDir; }

    public override void move()
    {
        float moveSpeed;
        mPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _moveDir = "right";
            moveSpeed = _basicSpeed;
            mPosition += Vector3.right;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _moveDir = "left";
            moveSpeed = _basicSpeed;
            mPosition += Vector3.left;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }
        else
        {
            _moveDir = "None";
            moveSpeed = 0;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.C))
        {
            _moveDir = "up";
            if (_jumpTime == 0)
            {
                Vector3 pos = transform.position;

                pos.y -= 0.5f;
                pos.z = -15;

                //jump effect
                //Instantiate()
            }

            if (!Input.GetKey(KeyCode.C) || _jumpTime >= _jumpTimeLimit) 
            {
                _isJump = false;
                return;
            }

            _rigid.velocity = Vector2.zero;
            _rigid.AddForce(Vector2.up * _basicJumpForce * ((_jumpTime * 10) + 1f), ForceMode2D.Impulse);
            _jumpTime += Time.deltaTime;
        }
        else
        {
            
            moveSpeed = 0;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _jumpTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("EnemyWeapon"))
        {
            Debug.Log("Player : Hit\n");
            Debug.Log($"{_hp}");

            if (_hp <= 0)
            {
                dead(this.gameObject);
            }
            else
            {
                _hp -= 1; // 상대 데미지를 받아와야함

            }
        }
    }
}
