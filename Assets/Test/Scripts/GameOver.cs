using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	private GameManager_Master gameManagerMaster;
	public GameObject PanelGameOver;
	// Use this for initialization
	void OnEnable(){
		SetInitialReferences ();
		gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
	}

	void OnDisable(){
		gameManagerMaster.GameOverEvent -= TurnOnGameOverPanel;
	}

	void SetInitialReferences(){
		gameManagerMaster = GetComponent<GameManager_Master> ();
	}

	void TurnOnGameOverPanel(){
		if (PanelGameOver != null) 
		{
			PanelGameOver.SetActive (true);
		}
	}
}
