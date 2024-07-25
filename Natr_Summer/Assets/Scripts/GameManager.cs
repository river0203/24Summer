using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//씬이 이동해도 유지해야할 항목, 골드, hp, 아이템 etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;
    public GameObject[] HP = new GameObject[5];
    private int HP_number = 5;

    public Image img_element;
    public Sprite[] element = new Sprite[5];

    public HP_animation[] hp_Ani = new HP_animation[5];

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
        img_element = GetComponent<Image>();
    }

    public void UI_player_hp_minus(int p_hp)
    {
        //HP 감소가 1인 경우
        if (HP_number - p_hp == 1)
        {
            HP_number = p_hp;

            Debug.Log("애니메이션 출력");
            hp_Ani[HP_number].HP_minus_animaition();

            Invoke("anim_delay", 1.0f);
        }
    }

    private void anim_delay()
    {
        Debug.Log("체력 감소");
        HP[HP_number].SetActive(false);
    }
}
