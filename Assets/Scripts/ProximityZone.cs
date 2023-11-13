using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(contact.point);
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
