using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitchController : MonoBehaviour
{

    //Creates a public enumaration with all terrain types
    public enum Mode { Floor }
    public Mode terrainType;


    //use this for initalization
    private void Start()
    {

    }
    //Update is called once per frame
    private void Update()
    {

    }

    //Function is called by other scripts and returns a string which 
    //corresponds to the type of terrain

    public string GetTerrainType()
    {

        string typeString = "";

        switch (terrainType)
        {

            case Mode.Floor:
                typeString = "Floor";
                break;

        }

        return typeString;
    }
}
