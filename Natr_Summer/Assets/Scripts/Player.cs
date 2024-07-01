using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{
    private Vector3 mPosition;

    //temp value shame
    private int _hp = 3;
    private int _damage = 1;
    private float _basicSpeed = 8;
    private float _basicJumpForce = 8;
    private bool _isJump;
    private float _jumpTime = 0f;
    private float _jumpTimeLimit = 0.1f;
    [SerializeField]
    private Rigidbody2D _rigid;

    public override int attack(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override int hit(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void move()
    {
        float moveSpeed;
        mPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveSpeed = _basicSpeed;
            mPosition += Vector3.right;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveSpeed = _basicSpeed;
            mPosition += Vector3.left;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.C))
        {
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
}
