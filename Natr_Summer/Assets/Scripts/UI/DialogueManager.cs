using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum SceneState
{
    Intro,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
}

public class DialogueManager : Singleton
{
    protected DialogueManager() { }

    private List<Dictionary<string, object>> data_DialogCustomer = null;
    //private List<Dictionary<string, object>> data_DialogOwner = null;
         
    private enum Type
    {
        Number,
        CustomerID,
        State,
        Title,
        Content
    }

    void Awake()
    {
        data_DialogCustomer = CSVreader.Read("scripts_intro");
    }

    /// <summary>
    /// �մ� id�� ���¸� �Ű������� �Է��ϸ� �ش� ��ũ��Ʈ�� ���ڿ��� ��ȯ�˴ϴ�.
    /// </summary>
    /// <param name="customerID">ã���� �ϴ� ID</param>
    /// <param name="state">���� ����</param>
    /// <returns></returns>
    public string DialogueToString(int number, int customerID, SceneState state, int type)
    {
        //if (data_DialogCustomer[number] == null)
        //    return null;

        if (data_DialogCustomer.Count <= number)
            return null;

        string temp = null;
        int id;

        id = SearchCustomerID(customerID);

        if (id == -1)
        {
            Debug.LogError("Customer id that does not exist");
        }

        for (int i = id; i < data_DialogCustomer.Count; i++)
        {
            if (data_DialogCustomer[i][Type.State.ToString()].Equals((string)state.ToString()))
            {
                id = i;
                break;
            }
        }

        if (type == (int)Type.Title)
            temp = (string)data_DialogCustomer[number][Type.Title.ToString()];

        else if (type == (int)Type.Content)
            temp = (string)data_DialogCustomer[number][Type.Content.ToString()];

        return temp;
    }

    /// <summary>
    /// �Ľ̵� csv������ ��ȸ�ϸ� �ش� id�� ã���ϴ�.
    /// </summary>
    /// <param name="id">�˻��� �մ� id</param>
    /// <returns></returns>
    private int SearchCustomerID(int id)
    {
        for (int i = 0; i < data_DialogCustomer.Count; i++)
        {
            if ((int)data_DialogCustomer[i][Type.CustomerID.ToString()] == id)
            {
                return i; 
            }
        }

        return -1;
    }
}
