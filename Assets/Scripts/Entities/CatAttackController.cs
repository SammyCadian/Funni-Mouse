using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatAttackController : MonoBehaviour
{
    [SerializeField] private KeyCode laserEyeKey;
    [SerializeField] private KeyCode debrisLeftKey;
    [SerializeField] private KeyCode debrisMiddleKey;
    [SerializeField] private KeyCode debrisRightKey;
    [SerializeField] private KeyCode punchLeftKey;
    [SerializeField] private KeyCode punchRightKey;
    [SerializeField] private Transform playerRef;
    [SerializeField] private GameObject laserEyes;
    [SerializeField] private GameObject debrisSpawner;
    [SerializeField] private GameObject fist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(laserEyeKey))
        {
            Instantiate(laserEyes);
        }
        if(Input.GetKeyDown(debrisLeftKey))
        {
            Instantiate(debrisSpawner, Camera.main.transform.position + new Vector3(-8, 8, 1), transform.rotation);
        }
        if(Input.GetKeyDown(debrisMiddleKey))
        {
            Instantiate(debrisSpawner, Camera.main.transform.position + new Vector3(0, 8, 1), transform.rotation);
        }
        if(Input.GetKeyDown(debrisRightKey))
        {
            Instantiate(debrisSpawner, Camera.main.transform.position + new Vector3(8, 8, 1), transform.rotation);
        }
        if(Input.GetKeyDown(punchRightKey))
        {
            Instantiate(fist, Camera.main.transform.position + new Vector3(8, -8, 1), transform.rotation).GetComponent<EnemyFist>().SetTarget(playerRef);
        }
        if(Input.GetKeyDown(punchLeftKey))
        {
            Instantiate(fist, Camera.main.transform.position + new Vector3(-8, -8, 1), transform.rotation).GetComponent<EnemyFist>().SetTarget(playerRef);
        }
    }
}
