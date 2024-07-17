using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicWeapon : GameWeapon
{
    private float _speed = 7f;
    private Player _player;
    private Rigidbody2D rigid;

    private void Start()
    {
        Invoke("DestroytBullet", 2);
    }

    private void Update()
    {
        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * _speed * Time.deltaTime);

        }
        else // 작동 안함
        {
            transform.Translate(transform.right * -1 * _speed * Time.deltaTime);

        }
    }
    public void DestroytBullet()
    {
        Destroy(gameObject);
    }
    public override void checkAttackCrash()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeapon") || collision.CompareTag("Enemy"))
        {
            Debug.Log("Player : Attack");
            checkAttackCrash();
        }
    }
}
