using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSlimeSpawn : MonoBehaviour
{
    public GameManager gm;
    public PlayerMove pm;

    public GameObject shieldSlime;
    private GameObject currentShieldSlime;

    private Vector3 spawnPoint;

    public bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused && currentShieldSlime == null &&  isActive == true)
        {
            SpawnShieldSlime();
            // Shield slime spawning sounds
        }
    }

    public void SpawnShieldSlime()
    {
        currentShieldSlime = Instantiate(shieldSlime, spawnPoint, Quaternion.identity);
    }
}
