using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� �̵��ص� �����ؾ��� �׸�, ���, hp, ������ etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;

    public GameObject[] HP = new GameObject[5];
    public HP_animation[] hp_Ani = new HP_animation[5];
    private int HP_number = 5;

    public Image img_element;
    public Sprite[] element = new Sprite[5];

    public Image img_script;
    public Text titleText;
    public Text mainText;

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
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (!img_script.gameObject.activeInHierarchy)
                img_script.gameObject.SetActive(true);

            else
                img_script.gameObject.SetActive(false);
        }
    }

    public void UI_player_hp_minus(int p_hp)
    {
        //HP ���Ұ� 1�� ���
        if (HP_number - p_hp == 1)
        {
            HP_number = p_hp;

            Debug.Log("�ִϸ��̼� ���");
            hp_Ani[p_hp].HP_minus_animaition();

            Invoke("anim_delay", 1.0f);
        }
    }

    private void anim_delay()
    {
        if (HP_number < 4 && HP[HP_number + 1].activeInHierarchy)
        {
            Debug.Log("ü�� ����1");
            HP[HP_number + 1].SetActive(false);
        }

        else
        {
            Debug.Log("ü�� ����2");
            HP[HP_number].SetActive(false);
        }
    }
}
