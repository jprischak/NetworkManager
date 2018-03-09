using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    /**
    *  VARIABLES
    * */
    public GameObject playerPrefab;


    private const string TYPENAME = "GameName";
    private const string GAMENAME = "RoomName";
    private HostData[] hostList;





    /**
    *  CLASS FUNCTIONS
    * */
    private void Start()
    {
    }


    // This function is called when the server is successfully initialized
    private void OnServerInitialized()
    {
        Debug.Log("Server Initializied");

        SpawnPlayer();
    }


    private void OnConnectedToServer()
    {
        Debug.Log("Server Joined");

        SpawnPlayer();
    }


    private void OnGUI()
    {
        // We will draw a button to the screen if we have not started or joined a server
        if(!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();


            if(hostList != null)
            {
                for(int i=0; i<hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }

        }
    }


    private void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        // Check to see if the message that was recieved is our list of host data
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }




    /**
    *  FUNCTIONS
    * */
    private void StartServer()
    {
        // To Initialize you need to list the max players 4
        // and the port number 25000
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(TYPENAME, GAMENAME);
    }


    private void RefreshHostList()
    {
        MasterServer.RequestHostList(TYPENAME);
    }


    private void JoinServer(HostData hostData)
    {
        Debug.Log("Joining: " + hostData.gameName);

        Network.Connect(hostData);
    }


    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, new Vector3(0f, 2f, 0f), Quaternion.identity, 0);
    }
}
