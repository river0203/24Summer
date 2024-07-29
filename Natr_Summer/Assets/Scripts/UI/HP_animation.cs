using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_animation : MonoBehaviour
{
    private Animator HP_ani;

    // Start is called before the first frame update
    private void Awake()
    {
        HP_ani = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    public void HP_minus_animaition()
    {
        HP_ani.SetTrigger("isHit");
    }
}
