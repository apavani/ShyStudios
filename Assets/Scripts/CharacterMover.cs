using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class CharacterMover : MonoBehaviour {

    private float halfSizeX;
    private float halfSizeY;
	// Use this for initialization
	void Start () {
        halfSizeX = gameObject.GetComponent<Renderer>().bounds.extents.x/2;
        halfSizeY = gameObject.GetComponent<Renderer>().bounds.extents.y/ 2;
    }

    // Update is called once per frame
    void Update () {
        if (Input.acceleration.x < -0.15)
        transform.position = new Vector3(Mathf.Max(MapBounds.minX + halfSizeX, transform.position.x+TCKTilt.sidewaysAxis*Time.deltaTime)   , transform.position.y, transform.position.z);
        else if(Input.acceleration.x > 0.15)
        transform.position = new Vector3(Mathf.Min(MapBounds.maxX - halfSizeX, transform.position.x-TCKTilt.sidewaysAxis*Time.deltaTime), transform.position.y, transform.position.z);
    }
}
