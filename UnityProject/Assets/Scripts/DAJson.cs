using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class DAJson
{
    public string data;
    public string GetDialogString(string dialog)
    {
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(dialog);
        string rst = getData["dialogAction"]["message"]["data"]["message"].ToString();
        string rst_lines = "";
        for(int i = 0; i < rst.Length; i++)
        {
            if (i % 40 == 0) rst_lines += "\n";
            rst_lines += rst[i];
        }
        return rst_lines;
    }
    public string GetDialogStringOrigin(string dialog)
    {
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(dialog);
        string rst = getData["dialogAction"]["message"]["data"]["message"].ToString();
        return rst;
    }
}
