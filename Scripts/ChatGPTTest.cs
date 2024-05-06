using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ChatGPTTest : MonoBehaviour
{
    // Method to handle the response from ChatGPT
    public void HandleChatGPTResponse(string response)
    {
        // Do something with the response, such as displaying it in the UI
        Debug.Log("ChatGPT Response: " + response);
    }
}
