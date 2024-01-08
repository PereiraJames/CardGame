using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class HostConnect : MonoBehaviour
{
    NetworkManager NetworkManager;
    public InputField ip_InputField;
    public GameObject HostConnect_go;

    void Awake()
    {
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    public void HostFunction()
    {
        Debug.Log("Hosted");
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        NetworkManager.StartHost();

        HostConnect_go.SetActive(false);
    }

    public void ConnectFunction()
    {
        Debug.Log("ClientJoined");
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if(ip_InputField.text != null)
        {
            ip_InputField.text = "127.0.0.1";
        }

        NetworkManager.networkAddress = ip_InputField.text;
        NetworkManager.StartClient();

        HostConnect_go.SetActive(false);
    }
}