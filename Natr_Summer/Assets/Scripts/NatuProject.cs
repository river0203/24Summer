using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NatuProject : MonoBehaviour
{
    Player mPlayer;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        mPlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        mPlayer.move();
    }
}
