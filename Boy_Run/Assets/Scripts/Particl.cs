using UnityEngine;
using System.Collections;

public class Particl : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Player";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -3;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
