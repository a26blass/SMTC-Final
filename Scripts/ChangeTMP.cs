using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTMP : MonoBehaviour
{
    public TextMeshProUGUI textbox;

    public void SetText(string s)
    {
        Debug.Log("Recieved: " + s);
        textbox.text = s;
    }
}
