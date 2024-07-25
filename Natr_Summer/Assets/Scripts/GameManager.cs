using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//씬이 이동해도 유지해야할 항목, 골드, hp, 아이템 etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;
    public GameObject[] HP = new GameObject[5];
    private int HP_number = 0;

    private float delaytime = 0.0f;
    private float anim_endtime = 60.0f;
    private bool anim_state_hit = false;
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
    private void Update()
    {
        if(anim_state_hit)
        {
            delaytime += 0.1f;
            Debug.Log("딜레이 시간 증가");
        }

        if (delaytime > anim_endtime)
        {
            HP[HP_number].SetActive(false);

            delaytime = 0.0f;

            Debug.Log("체력 감소");
        }
    }

    public void UI_player_hp_minus(int p_hp)
    {
        HP_number = p_hp;
        anim_state_hit = true;

        Debug.Log("애니메이션 출력");
        hp_Ani[HP_number].HP_minus_animaition();
    }
}
