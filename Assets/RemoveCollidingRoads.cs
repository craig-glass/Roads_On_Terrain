using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCollidingRoads : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider coll = GetComponent<SphereCollider>();
        coll.enabled = false;
        coll.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            Debug.Log("..." + other.name);
            Destroy(other.gameObject);
        }
    }
    
}
