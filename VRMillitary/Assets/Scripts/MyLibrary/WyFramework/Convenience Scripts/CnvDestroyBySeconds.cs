using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CnvDestroyBySeconds : MonoBehaviour
{

    public float time = 1f;
	public List<GameObject> objToDestroy;

    // Start is called before the first frame update
    void Start()
    {

        // Add self, if nothing was passed in insepctor
		if (objToDestroy.Count == 0) {
            objToDestroy.Add(gameObject);
		}

		foreach (GameObject obj in objToDestroy) {
            Destroy(obj, time);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
