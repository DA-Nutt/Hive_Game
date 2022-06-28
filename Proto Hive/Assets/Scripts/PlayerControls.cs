using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    #region Variables
    //A filter to ensure the player can only select Territory tiles
    public LayerMask navMask;
    public TerritoryTile focus;

    public Camera cam;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player left clicks...
        if (Input.GetMouseButtonDown(0)) 
        {
            //...Shoot out a ray from the mouse
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //Store data about the object we hit
            RaycastHit hit;

            //If our ray hits something on our mask (A Territory Tile)...
            if (Physics.Raycast(ray, out hit, 100, navMask)) 
            {
                //Grab a reference to the tile we just clicked on
                TerritoryTile tile = hit.collider.GetComponent<TerritoryTile>();

                //Set that selected tile as our focus
                SetFocus(tile); 
                
            } else //If the ray does not hit a tile...
            {
                RemoveFocus();
            }
        }
    }

    private void SetFocus(TerritoryTile newFocus)
    {
        focus = newFocus;
        Debug.Log("Setting Focus To " + newFocus.name);
    }

    private void RemoveFocus()
    {
        Debug.Log("Defocusing On " + focus.name);
        focus = null;
    }

}
