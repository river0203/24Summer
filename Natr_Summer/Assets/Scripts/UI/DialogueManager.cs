using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


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

    public void readCSV(SceneState state)
    {
        switch (state)
        {
            case SceneState.INTRO:
                data_DialogCustomer = CSVreader.Read("scripts_intro");
                break;

            case SceneState.STAGE1:
                data_DialogCustomer = CSVreader.Read("scripts_stage1");
                break;

            case SceneState.BOSS:
                data_DialogCustomer = CSVreader.Read("scripts_boss");
                break;
        }
    }
    public string DialogueToString(int number, int customerID, int type)
    {
        if (data_DialogCustomer.Count <= number)
            return null;

        string temp = null;
        int id;

        id = SearchCustomerID(customerID);

        if (id == -1)
        {
            return null;
        }

        if ((int)data_DialogCustomer[number][Type.CustomerID.ToString()] == id)
        {
            if (type == (int)Type.Title)
                temp = (string)data_DialogCustomer[number][Type.Title.ToString()];

            else if (type == (int)Type.Content)
                temp = (string)data_DialogCustomer[number][Type.Content.ToString()];
        }

        else
            return null;

        return temp;
    }

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

// �� �� ���� ����...
// Scene ���� �о�ͼ� > CSV ���� � �� ������ ����
// customerID�� event number�� �о�� �� ��Ʈ�� ��� �����ϰ� ����
