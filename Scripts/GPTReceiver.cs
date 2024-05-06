using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTRec : MonoBehaviour
{
    public StringEvent recieveEvent;

    public void RecieveFromGPT(string response)
    {
        Debug.Log(response);
        recieveEvent.Invoke(response);
    }
}
