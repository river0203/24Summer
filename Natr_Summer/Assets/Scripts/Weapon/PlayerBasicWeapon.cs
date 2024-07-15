using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicWeapon : GameWeapon
{
    private float _speed = 5f;
    private float _lifeTime = 10f;

    private Rigidbody2D rigid;

    private void Start()
    {

    }

    private void Update()
    {
        
    }
    public override void checkAttackCrash()
    {
        //이벤트 발생시 처리할 부분
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("EnemyWeapon") || collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Player : Attack");
        }
    }
}
