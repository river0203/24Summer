using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    public GameObject[] HP = new GameObject[5];
    public HP_animation[] hp_Ani = new HP_animation[5];
    private int HP_number = 4;

    public void UI_player_hp_minus(int p_hp)
    {
        //HP ���Ұ� 1�� ���
        HP_number = p_hp;

        Debug.Log("�ִϸ��̼� ���");
        hp_Ani[HP_number].HP_minus_animaition();

        Invoke("anim_delay", 1.0f);
    }

    private void anim_delay()
    {
        Debug.Log("ü�� ����");
        HP[HP_number].SetActive(false);
    }
}
