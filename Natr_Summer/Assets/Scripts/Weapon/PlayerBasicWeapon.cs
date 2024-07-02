using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicWeapon : GameWeapon
{
    private int weaponDamage = 1;
    public override int getWeaponDamage()
    {
        return weaponDamage;
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
