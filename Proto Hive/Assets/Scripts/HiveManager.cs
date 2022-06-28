using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveManager : MonoBehaviour
{
    //when the player clicks on the HIVE, it spawns a new unit

    #region Variables
    public GameObject unit;
    public Transform spawnTransform;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpawnUnit();
        }
    }

    //Instantiate a unit on the same tile you clicked the hive on
    public void SpawnUnit()
    {
        Instantiate(unit, spawnTransform.position, Quaternion.identity);
    }
}
