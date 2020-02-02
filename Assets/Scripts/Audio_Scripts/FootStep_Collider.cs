using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep_Collider : MonoBehaviour
{


	private string colliderType;


	// Start is called before the first frame update
	void Start()
	{

		AkSoundEngine.SetSwitch("Surface_Type", "Floor", gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}

	//This Function detects if there is a collision between the player controller
	//and a game object. If there is a collision it calls the function GetTerrainType
	//from the MaterialSwitchController Class which retains the terrain type that
	//has been set in the enumaration mode of that object. Then is calls PlayStepSoundMaterial
	//method which is using a switch stament to set the Wwise Switches.

	private void OnControllerColliderHit(ControllerColliderHit col)
	{
		if (col.gameObject.GetComponent<MaterialSwitchController>())
		{

			//store what the GetTerrainType returns and store is in the varible collider type.
			colliderType = col.gameObject.GetComponent<MaterialSwitchController>().GetTerrainType();
		}
		// calling the PlayStepSoundMaterialType function
		PlayStepSoundMaterialType();

		//print in the console the returned value of MaterialSwitchController
		Debug.Log(colliderType);
	}


	void PlayStepSoundMaterialType()
	{
		//checks the content of the colliderType variable and depending on the value of the 
		//variable we switch the surface type switch group to the appropriate switch type
		switch (colliderType)
		{
			case "Floor":
				AkSoundEngine.SetSwitch("Surface_Type", "Floor", gameObject);
				break;
		}
	}
}
