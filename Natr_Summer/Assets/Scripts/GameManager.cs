using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//씬이 이동해도 유지해야할 항목, 골드, hp, 아이템 etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;
    public GameObject[] HP = new GameObject[5];

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

    public void UI_player_hp_minus(int p_hp)
    {
        HP[p_hp].SetActive(false);
    }
}
