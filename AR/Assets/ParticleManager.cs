using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{

    #region variables
    public ParticleSystem snowSystem;
    public ParticleSystem rainSystem;

    int count = 0;


    #endregion
    // Use this for initialization
	void Start () {
	    
	}

    //Swap between systems for testing
    void checkKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            snowSystem.playOnAwake = true;
            snowSystem.Play(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rainSystem.playOnAwake = true;
            rainSystem.Play(true);
        }
    }

	// Update is called once per frame
	void Update () {
        checkKeyInput();
	}
}
