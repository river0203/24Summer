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
    public int count_data;

    private enum Type
    {
        Number,
        CustomerID,
        Title,
        Content,
        dialoguetype
    }

    public enum dialogueType
    {
        text, select, end
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

        count_data = data_DialogCustomer.Count;
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

            else
                temp = (string)data_DialogCustomer[number][Type.dialoguetype.ToString()];
        }

        else
            return null;

        return temp;
    }

    public string checkDialogueType(int number)
    {
        string temp = null;

        if ((string)data_DialogCustomer[number][Type.dialoguetype.ToString()] == "text")
            temp = "text";

        else if ((string)data_DialogCustomer[number][Type.dialoguetype.ToString()] == "select")
            temp = "select";

        else
            temp = "end";

        return temp;
    }

    private int SearchCustomerID(int id)
    {
        for (int i = 0; i < data_DialogCustomer.Count; i++)
        {
            if ((int)data_DialogCustomer[i][Type.CustomerID.ToString()] == id)
            {
                return id;
            }
        }

        return -1;
    }
}
