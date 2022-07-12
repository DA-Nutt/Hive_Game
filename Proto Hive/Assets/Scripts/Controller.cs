using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    /* Resovling Actions
     * 0 Selection State
     * 1. Select a Territory
     *  Select an Action state
     * 2. Select an Action (Button Click)
     *  Select an Action State 
     * 3. Select a Target of the Action
     * 4. Visual Confrimation/Feedback of Action
     */

    #region Variables
    //A filter to ensure the player can only select Territory tiles
    public LayerMask navMask;
    public InteractableObject focusObject;
    public HiveManager hiveManager;
    public Camera cam;
    public Territory highlightedTerritory;
    public Territory targetedTeritory;
    public ControllerState controllerState;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        controllerState = ControllerState.territorySelection;
    }

    // Update is called once per frame
    void Update()
    {
        #region Territory Selection Controller State
        if (controllerState != ControllerState.targetSelection)
        {
            //If the player left clicks...
            if (Input.GetMouseButtonDown(0))
            {
                //...Shoot out a ray from the mouse
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //Store data about the object we hit
                RaycastHit hit;

                //If our ray hits something on our mask...
                if (Physics.Raycast(ray, out hit, 100, navMask))
                {
                    if (hit.collider.CompareTag("ClickableObject"))
                    {
                        //Grab a reference to the object we just clicked on
                        InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                        //Set that selected tile as our focus
                        SetFocus(interactableObject);
                        controllerState = ControllerState.actionSelection;
                    }

                }
                else //If the ray does not hit an interactable object...
                {
                    RemoveFocus();
                    controllerState = ControllerState.territorySelection;
                }
            }
        }
       
        #endregion


        #region Action Selection Controller State
        if (controllerState == ControllerState.actionSelection) 
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                hiveManager.SpawnUnit(highlightedTerritory, 50);
            }


            if (Input.GetKeyDown(KeyCode.M)) //"Move Units" Action Button
            {
                controllerState = ControllerState.targetSelection;
                Debug.Log("Select A Territory To Move Units To");
                
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                highlightedTerritory.SpawnHive();
            }
        }
        #endregion      
       

        if(controllerState == ControllerState.targetSelection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //...Shoot out a ray from the mouse
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //Store data about the object we hit
                RaycastHit hit;

                //If our ray hits something on our mask...
                if (Physics.Raycast(ray, out hit, 100, navMask))
                {
                    if (hit.collider.CompareTag("ClickableObject"))
                    {
                        //Grab a reference to the object we just clicked on
                        targetedTeritory = hit.collider.GetComponent<Territory>();

                        highlightedTerritory.MoveUnits(targetedTeritory);
                        controllerState = ControllerState.territorySelection;

                    }

                }
                else //If the ray does not hit an interactable object...
                {
                    RemoveFocus();
                    controllerState = ControllerState.territorySelection;
                }
            }
        }
    }


    #region Controller Methods
    private void SetFocus(InteractableObject newFocus)
    {
        controllerState = ControllerState.actionSelection;
        focusObject = newFocus;
        highlightedTerritory = newFocus.gameObject.GetComponent<Territory>();
        highlightedTerritory.SelectTerritory();
        Debug.Log("Setting Focus To " + newFocus.name);
    }

    private void RemoveFocus()
    {
        if (focusObject != null)
        {
            controllerState = ControllerState.territorySelection;
            Debug.Log("Defocusing On " + focusObject.name);
            focusObject = null;
        }  
    }
    #endregion

}
