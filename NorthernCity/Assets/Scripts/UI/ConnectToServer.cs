using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectToServer : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField ipField;

    [SerializeField]
    public TMP_InputField passwordField;

    public void TryEstablishingConnectionWithServer()
    {
        Client.Setup(ipField.text, 5060, passwordField.text);
    }
}
