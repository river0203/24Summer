using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour 
{
    public abstract void move(); //return ���Ͱ��� ���� �̵�
    public abstract void attack(); //return oppnent hp
    public abstract void hit(); // return Oneself hp
    protected void dead(GameObject thisObject)
    {
        //after Dead animation
        Destroy(thisObject);
    }
}
