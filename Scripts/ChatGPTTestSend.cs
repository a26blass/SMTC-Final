using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGPTTestSend : MonoBehaviour
{
    public StringEvent onResponseReceived;

    // Your existing code here...
    private void Start()
    {
        Debug.Log("Starting conversation");
        SendToChatGPT("Test! can you hear me?");
    }
    // Method to send a prompt to ChatGPT
    public void SendToChatGPT(string prompt)
    {
        // Your existing code to send prompt to ChatGPT

        // Once you receive a response, invoke the UnityEvent with the response
        Debug.Log("Sending " + prompt);
        onResponseReceived.Invoke(prompt); // Assuming 'response' is the string you receive from ChatGPT
    }
}
