//StageSelectシーンでステージの選択ボタンを配置
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButton : MonoBehaviour {

	public int page;
	public GameObject stageButton;

	public int kiteix,kiteiy;

	public float kankakux,kankakuy;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 5; j++) {
				GameObject button = (GameObject)Instantiate(stageButton) as GameObject;
				button.transform.parent = this.transform;
				button.transform.position = new Vector3(kankakux*j+kiteix,kankakuy*i+kiteiy,0);

			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
