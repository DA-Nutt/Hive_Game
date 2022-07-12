using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Territory : MonoBehaviour
{

    #region Variables
    public int numUnits;
    public int unitCapacity;
    public GameObject hiveObject;
    public GameObject unitObject;
    public Transform spawnTransform;
    public bool hasHive;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Methods
    public void SelectTerritory()
    {
    
    }

    public void MoveUnits(Territory target)
    {
        //remove units from this tile 
        //add units to the target tile
        Debug.Log("Moving " + numUnits + " Units From " + gameObject.name + " to" + target.name);
        target.numUnits += numUnits;
        numUnits = 0;
        UpdateTerritoryGFX();
        target.UpdateTerritoryGFX();
    }


    public void SpawnHive()
    {
        if (!hasHive && numUnits >= 100)
        {
            hasHive = true;
            //Instantiate(hiveObject, transform.position, Quaternion.identity);
            hiveObject.SetActive(true);
            Debug.Log("Spawning Hive");
        } else
        {
            Debug.Log("Unable to create a hive!");
        }
    }

    public void UpdateTerritoryGFX()
    {
        if (numUnits > 0)
        {
            unitObject.SetActive(true);
        }
        else
        {
            unitObject.SetActive(false);
        }
    }
    #endregion

}
