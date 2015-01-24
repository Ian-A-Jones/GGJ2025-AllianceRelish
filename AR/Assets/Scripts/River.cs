using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class River : MonoBehaviour {

	public ParticleSystem riverParticles;

	// Use this for initialization
	void Awake () {
		riverParticles.renderer.sortingLayerName = "River";
		riverParticles.renderer.sortingOrder = 1;


	}
	
	// Update is called once per frame
	void Update () {





	}


}
