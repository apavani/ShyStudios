using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour {

     public static float minX;
     public static float  maxX;
     public static float  minY;
     public static float  maxY;
     
     void Update()
    {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        Vector3 leftBottomBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector3 rightTopBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        minX = leftBottomBounds.x;
        maxX = rightTopBounds.x;
        minY = leftBottomBounds.y;
        maxY = rightTopBounds.y;
    }
}
