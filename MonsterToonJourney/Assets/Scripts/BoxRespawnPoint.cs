using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawnPoint : MonoBehaviour
{
    private GameManager gm;
    public BoxRespawn br;

    public List<GameObject> boxes;

    public GameObject ogBox;
    public GameObject spawnBox;

    public Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        respawnPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBox()
    {
        if (!gm.isPaused && ogBox == null)
        {
            ogBox = Instantiate(spawnBox, respawnPoint, Quaternion.identity);
            br.ogBox = ogBox;
        }

    }
}
