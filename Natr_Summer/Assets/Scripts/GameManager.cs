using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� �̵��ص� �����ؾ��� �׸�, ���, hp, ������ etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(gameManager != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void Start()
    {

    }
    private void Update()
    {
        
    }
}
