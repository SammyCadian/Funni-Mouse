using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // [SerializeField] GameObject baselineEcho;
    // [SerializeField] private int maxEchoes;
    // [SerializeField] GameObject EchoHolder;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float echoSpacing;
    [SerializeField] private KeyCode rewindButton;
    [SerializeField] private KeyCode rechargeButton;
    
    [SerializeField] private PlayerEcho echoA, echoB, echoC;
    private PlayerMovement plyrMovement;
    private int echoes;
    private float echoTimer;
    // private LinkedList<GameObject> echoList = new LinkedList<GameObject>();
    private GameObject[] echoList;
    // Start is called before the first frame update
    void Start()
    {
        plyrMovement = GetComponent<PlayerMovement>();
        echoes = 3;
        // echoA = EchoHolder.transform.GetChild(2).GetComponent<PlayerEcho>();
        // echoB = EchoHolder.transform.GetChild(1).GetComponent<PlayerEcho>();
        // echoC = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();
        // // echoList = new PlayerEcho[maxEchoes];
        // for (int i = 0; i < maxEchoes; i++)
        // {
        //     echoList[i] = Instantiate(baselineEcho);
        // }
        // for (int i = 0; i < maxEchoes; i++)
        // {
        //     //Instatiate a new Echo for the player
        //     // PlayerEcho newEcho = ;

        //     //Add that echo to the list
        //     echoList.AddFirst(Instantiate(baselineEcho));
        //     echoList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing(), plyrMovement.GetAnimState());
        // }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessEchoCreation();
        InputManager();
        echoTimer += Time.deltaTime;
        // Debug.Log(echoList.Count);
    }
    private void InputManager()
    {
        if (Input.GetKeyDown(rewindButton) && echoes > 0)
        {
            Debug.Log("hi");
            echoes--;
            plyrMovement.Rewind(echoA.GetVelocity(), echoA.transform.position, echoA.GetFacing(), echoA.GetState());
        }
        
        if (Input.GetKey(rechargeButton) && echoes < 3)
        {

        }
    }
    private void ProcessEchoCreation()
    {
        if (echoTimer > echoSpacing)
        {
            echoTimer = 0f;
            CreateEcho();
        }
    }
    private void CreateEcho()
    {
        // Debug.Log(plyrMovement.GetAnimSta());
        // plyrMovement.GetAnimSta();
        echoC.SetValues(echoB.GetVelocity(), echoB.transform.position, echoB.GetFacing(), echoB.GetState());
        echoB.SetValues(echoA.GetVelocity(), echoA.transform.position, echoA.GetFacing(), echoA.GetState());
        echoA.SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing(), plyrMovement.GetAnimState());
    }

    /*
    private void CreateEcho()
    {
        PlayerEcho echoA = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();
        PlayerEcho echoB = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();
        PlayerEcho echoC = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();
        /*
        // //Instatiate a new Echo for the player
        // GameObject newEcho = Instantiate(baselineEcho);
        // Debug.Log(newEcho);

        // //Add that echo to the list
        // echoList.AddFirst(newEcho);
        // echoList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing(), plyrMovement.GetAnimState());

        // //Get and destroy the oldest Echo
        // GameObject oldEcho = echoList.Last.Value;
        // Destroy(oldEcho);
        // echoList.RemoveLast();

        // for (int i = maxEchoes - 1; i > 0; i--)
        // {
        //     echoList[i].GetComponent<PlayerEcho>().SetValues(echoList[i - 1].GetComponent<PlayerEcho>().GetVelocity(), echoList[i - 1].transform.position, echoList[i - 1].GetComponent<PlayerEcho>().GetFacing(), echoList[i - 1].GetComponent<PlayerEcho>().GetState());
        // }
        // echoList[0].GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing(), plyrMovement.GetAnimState());
        
    }*/
}
