using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _bJump;
    private Player _player;
    void Start()
    {
        _player = GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        _bJump = _player.getIsJump();
    }

    public void fallAni()
    {
        if(_bJump == false)
        {
            _player._animator.SetTrigger("Fall");
        }
    }
}
