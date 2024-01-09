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
        
        NetworkManager.StopHost();
        NetworkManager.StopClient();
        
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

        NetworkManager.StopHost();
        NetworkManager.StopClient();

        if(IsNetworkAddressValid(ip_InputField.text))
        {
            NetworkManager.StartClient();
        }       

        HostConnect_go.SetActive(false);
    }

    public bool IsNetworkAddressValid(string address)
    {
        // Check if the address is not null or empty and meets other required conditions
        bool isValid = !string.IsNullOrEmpty(address) && IsValidIPFormat(address);

        return isValid;
    }

    // Example method to check if the address is a valid IP format (you can expand this as needed)
    bool IsValidIPFormat(string address)
    {
        // Implement your logic here to validate the IP address format
        // Example: You might use regular expressions or other methods to validate the IP address format
        // This is a basic example just to show the idea

        bool isValid = System.Net.IPAddress.TryParse(address, out _);
        return isValid;
    }
    
}