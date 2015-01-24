using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenderParticlesOnLayer : MonoBehaviour {

	public ParticleSystem particles;
    public string sortingLayerName;


	// Use this for initialization
	void Awake () {
        if (particles != null)
        {
            changeOrder(particles);
        }

	}
	
	// Update is called once per frame
	void Update () {





	}

    public void changeOrder(ParticleSystem particleSystem)
    {
        particleSystem.renderer.sortingLayerName = sortingLayerName;
        particleSystem.renderer.sortingOrder = 1;

    }
}
