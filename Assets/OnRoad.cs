using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoad : MonoBehaviour
{
    public bool isDestroyed = false;
    Terrain terrain;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        Vector3 pos = transform.position;
        pos.y = terrain.SampleHeight(pos);
        pos.y += 5f;
        transform.position = pos;
        BoxCollider coll = GetComponent<BoxCollider>();
        coll.enabled = false;
        coll.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Road"))
        {
            Debug.Log("other = " + other.gameObject.name);
            Destroy(gameObject);
        }
        if (!other.gameObject.GetComponent<OnRoad>().isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
