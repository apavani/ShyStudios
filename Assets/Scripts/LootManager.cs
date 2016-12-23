using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public int maxGemCapacity;
    private int gemsHolding = 0;
    List<GameObject> gemsInLoot;

    void Start()
    {
        gemsInLoot = new List<GameObject>();
    }
	// Use this for initialization
	void OnTriggerEnter (Collider other) {
		if(other.tag=="Gem" && gemsHolding<maxGemCapacity)
        {
            gemsHolding++;
            other.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
            other.transform.parent = transform;
            other.transform.localPosition = new Vector3(0, -1.5f, 0);
            gemsInLoot.Add(other.gameObject);
        }

        else if(other.tag=="TreasureChest" && gemsHolding>0)
        {
            gemsHolding--;
            foreach(GameObject go in gemsInLoot)
            {
                Destroy(go);
            }
        }
	}
}
