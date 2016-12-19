using System;
using UnityEngine;
using TouchControls;

public class CharacterMover : MonoBehaviour {

    public float levitateSpeed;
    public float gravity;
    private float halfSizeX;
    private float halfSizeY;

    IPlayerController _control;
	// Use this for initialization
	void OnEnable () {
        halfSizeX = gameObject.GetComponent<Renderer>().bounds.extents.x/2;
        halfSizeY = gameObject.GetComponent<Renderer>().bounds.extents.y/ 2;
        _control = GameObject.Find("Controller").GetComponent<AccelerometerControl>();
        _control.LeftControl += new EventHandler<NavigationControlArgs>(GoLeft);
        _control.RightControl += new EventHandler<NavigationControlArgs>(GoRight);
        _control.Levitate += new EventHandler<NavigationControlArgs>(GoUp);
    }


    private void GoLeft(object sender, NavigationControlArgs e)
    {
        transform.position = new Vector3(Mathf.Max(MapBounds.minX + halfSizeX, transform.position.x +  e.value* Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void GoRight(object sender, NavigationControlArgs e)
    {
        transform.position = new Vector3(Mathf.Min(MapBounds.maxX - halfSizeX, transform.position.x - e.value * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void GoUp(object sender, NavigationControlArgs e) { 
        if(e.levitate)
        {
            transform.position = new Vector3(transform.position.x,Mathf.Min(MapBounds.maxY - halfSizeY, transform.position.y + transform.up.y * Time.deltaTime * levitateSpeed), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Max(MapBounds.minY + halfSizeY, transform.position.y - transform.up.y * Time.deltaTime * gravity), transform.position.z);
        }
    }
}
