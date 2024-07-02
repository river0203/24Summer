using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour 
{
    public abstract void move(); //return 백터값을 통한 이동
    //public abstract int attack(); //return oppnent hp
    //public abstract int hit(int damage); // return Oneself hp
    protected void dead(GameObject thisObject)
    {
        //after Dead animation
        Destroy(thisObject);
    }
}
