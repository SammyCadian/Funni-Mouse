using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject baselineEcho;
    [SerializeField] GameObject prayingCursor;
    // [SerializeField] private int maxEchoes;
    // [SerializeField] GameObject EchoHolder;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float echoSpacing;
    [SerializeField] private KeyCode rewindButton;
    [SerializeField] private KeyCode rechargeButton;
    
    // [SerializeField] private PlayerEcho echoA, echoB, echoC;
    [SerializeField] private Color colorA, colorB, colorC;
    [SerializeField] private OuchieMenu ouchUI;
    private GameObject currentCursor;
    private PlayerMovement plyrMovement;
    private int echoes;
    private float echoTimer;
    private float rechargeTimer;
    private bool praying;
    private LinkedList<GameObject> echoAList;
    private LinkedList<GameObject> echoBList;
    private LinkedList<GameObject> echoCList;
    // private GameObject[] echoList;
    // Start is called before the first frame update
    void Start()
    {
        echoAList = new LinkedList<GameObject>();
        echoBList = new LinkedList<GameObject>();
        echoCList = new LinkedList<GameObject>();
        plyrMovement = GetComponent<PlayerMovement>();
        echoes = 3;

        for (int i = 0; i < 5; i++)
        {
            echoAList.AddFirst(Instantiate(baselineEcho));
            echoAList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing());
        }
        // Debug.Log(echoAList.Count);
        for (int i = 0; i < 5; i++)
        {
            echoBList.AddFirst(Instantiate(baselineEcho));
            echoBList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing());
        }
        for (int i = 0; i < 5; i++)
        {
            echoCList.AddFirst(Instantiate(baselineEcho));
            echoCList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(plyrMovement.IsRewinding())
        {
            ProcessRewind();
        }else
        {
            ProcessEchoUpdates();
            if(Time.timeScale != 0f){InputManager();}
            if(praying){plyrMovement.PrayingContinue(); ProcessPraying();}
        }
        echoTimer += Time.deltaTime;
        // Debug.Log(echoList.Count);
    }
    private void ProcessPraying()
    {
        if(rechargeTimer >= rechargeTime)
        {
            RechargeEcho();
            rechargeTimer = 0f;
            if(echoes == 3){plyrMovement.PrayingEnd(); praying = false; currentCursor.GetComponent<PrayingCursor>().FinishPraying(); currentCursor = null;}
        }
        rechargeTimer += Time.deltaTime;
    }
    private void InputManager()
    {
        if (Input.GetKeyDown(rewindButton) && echoes > 0)
        {
            plyrMovement.StartRewind();
            // plyrMovement.Rewind(echoA.GetVelocity(), echoA.transform.position, echoA.GetFacing(), echoA.GetState());
        }
        
        if (Input.GetKeyDown(rechargeButton) && echoes < 3)
        {
            currentCursor = Instantiate(prayingCursor);
            currentCursor.GetComponent<PrayingCursor>().SetTracking(transform);
            plyrMovement.PrayingStart();
            rechargeTimer = 0f;
            praying = true;
        }
        if (Input.GetKeyUp(rechargeButton) && praying)
        {
            currentCursor.GetComponent<PrayingCursor>().FinishPraying();
            currentCursor = null;
            plyrMovement.PrayingEnd();
            praying = false;
        }
    }
    private void RechargeEcho()
    {
        switch(echoes)
        {
            case 0:
                for (int i = 0; i < 5; i++)
                {
                    echoAList.AddFirst(Instantiate(baselineEcho));
                    echoAList.First.Value.GetComponent<SpriteRenderer>().enabled = false;
                    echoAList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing());
                }
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    echoBList.AddFirst(Instantiate(baselineEcho));
                    echoBList.First.Value.GetComponent<SpriteRenderer>().enabled = false;
                    echoBList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing());
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++)
                {
                    echoCList.AddFirst(Instantiate(baselineEcho));
                    echoCList.First.Value.GetComponent<SpriteRenderer>().enabled = false;
                    echoCList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing());
                }
                break;
        }
        echoes++;
    }
    private void ProcessRewind()
    {
        if (echoTimer > 0.1f)
        {
            echoTimer = 0f;
            PlayerEcho e = echoAList.First.Value.GetComponent<PlayerEcho>();
            echoAList.RemoveFirst();
            if(echoAList.Count == 0)
            {
                plyrMovement.RewindFinal(e);
                LoseEcho();
                echoes--;
            }else
            {
                plyrMovement.RewindOneNode(e);
            }
            Destroy(e.gameObject);
        }
    }
    private void ProcessEchoUpdates()
    {
        if (echoTimer > echoSpacing)
        {
            echoTimer = 0f;
            UpdateEcho();
        }
    }
    /*
    private void CreateEcho()
    {
        // Debug.Log(plyrMovement.GetAnimSta());
        // plyrMovement.GetAnimSta();
        echoC.SetValues(echoB.GetVelocity(), echoB.transform.position, echoB.GetFacing(), echoB.GetState());
        echoB.SetValues(echoA.GetVelocity(), echoA.transform.position, echoA.GetFacing(), echoA.GetState());
        echoA.SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing(), plyrMovement.GetAnimState());
    }*/
    private void LoseEcho()
    {
        echoAList = echoBList;
        echoBList = echoCList;
        echoCList = new LinkedList<GameObject>();
    }
    
    private void UpdateEcho()
    {
        // PlayerEcho echoA = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();
        // PlayerEcho echoB = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();
        // PlayerEcho echoC = EchoHolder.transform.GetChild(0).GetComponent<PlayerEcho>();\
        // //Instatiate a new Echo for the player
        // GameObject newEcho = ;
        if(echoes == 0){return;}
        echoAList.AddFirst(Instantiate(baselineEcho));
        echoAList.First.Value.GetComponent<SpriteRenderer>().color = colorA;
        echoAList.First.Value.GetComponent<SpriteRenderer>().enabled = false;
        echoAList.First.Value.GetComponent<PlayerEcho>().SetValues(plyrMovement.GetVelocity(), transform.position, plyrMovement.GetFacing(), plyrMovement.GetAnimState());

        GameObject oldEcho = echoAList.Last.Value;
        echoAList.RemoveLast();
        echoAList.Last.Value.GetComponent<SpriteRenderer>().enabled = true;
        if (echoes == 1)
        {
            Destroy(oldEcho);
            return;
        }
        oldEcho.GetComponent<SpriteRenderer>().color = colorB;
        oldEcho.GetComponent<SpriteRenderer>().enabled = false;
        echoBList.AddFirst(oldEcho);
        
        oldEcho = echoBList.Last.Value;
        echoBList.RemoveLast();
        echoBList.Last.Value.GetComponent<SpriteRenderer>().enabled = true;
        oldEcho.GetComponent<SpriteRenderer>().color = colorC;
        oldEcho.GetComponent<SpriteRenderer>().enabled = false;
        if(echoes == 2)
        {
            Destroy(oldEcho);
            return;
        }

        echoCList.AddFirst(oldEcho);
        
        oldEcho = echoCList.Last.Value;
        echoCList.RemoveLast();
        echoCList.Last.Value.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(oldEcho);
    
    }
    
    public void GetHit()
    {
        if(plyrMovement.IsRewinding()){return;}
        if(echoes > 0)
        {
            plyrMovement.StartRewind();
        }else
        {
            // enabled = false;
            ouchUI.Pause();
        }
    }
}
