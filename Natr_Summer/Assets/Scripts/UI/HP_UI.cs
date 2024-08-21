using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    public GameObject[] HP = new GameObject[5];
    public HP_animation[] hp_Ani = new HP_animation[5];
    private int HP_number = 4;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        for (int i = 0; i < HP.Length; i++)
        {
            HP[i].SetActive(false);
        }

        int currentHP = _gameManager.get_playercurrentHP();
        
        for (int i = 0; i < currentHP; i++)
        {
            HP[i].SetActive(true);
        }
    }

    public void UI_player_hp_minus(int p_hp)
    {
        //HP 감소가 1인 경우
        HP_number = p_hp;

        Debug.Log("애니메이션 출력");
        hp_Ani[HP_number].HP_minus_animaition();

        Invoke("anim_delay", 1.0f);
    }

    private void anim_delay()
    {
        Debug.Log("체력 감소");

        for (int i = 4; i >= HP_number; i--)
        {
            HP[i].SetActive(false);
        }
    }
}
