using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameStateManager : NetworkBehaviour {

	[SyncVar]
	public bool win;


	private void Update(){

		if(LevelController.Instance.inGame){

			// check if minimum lemmings entered


			// check timer


			// check if all lemmings died
		}

	}

}
