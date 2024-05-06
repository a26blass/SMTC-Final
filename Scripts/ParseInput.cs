using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ParseInput : MonoBehaviour
{
    public TMP_InputField input;
    public GPTSender sender;

    private void Start()
    {
        input.onSubmit.AddListener(OnSubmit);
    }

    // Listen for "enter" key
    private void OnSubmit(string s)
    {
        Debug.Log("Submitted " + s);
        sender.SendToChatGPT("[Player]\n" + s);
        input.interactable = false;
    }
    
}
