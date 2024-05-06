using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTSender : MonoBehaviour
{
    public StringEvent sendEvent;

    public void SendToChatGPT(string prompt)
    {
        Debug.Log("Sending " + prompt);
        sendEvent.Invoke(prompt); 
    }
}
