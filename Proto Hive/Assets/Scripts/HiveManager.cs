using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveManager : MonoBehaviour
{

    //Instantiate a unit on the same tile you clicked the hive on
    public void SpawnUnit(Territory highlightedTerritory, int unitsToSpawn)
    {
        if (highlightedTerritory.hasHive)
        {
            highlightedTerritory.numUnits += unitsToSpawn;
            highlightedTerritory.UpdateTerritoryGFX();
            Debug.Log("Spawning " + unitsToSpawn + " units on" + highlightedTerritory.name);
        } else
        {
            Debug.Log("This tile does not have a hive!");
        }
       
    }
}
