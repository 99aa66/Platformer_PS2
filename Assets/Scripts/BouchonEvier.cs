using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouchonEvier : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabBilleSegs;
    public int numLinks = 6;

    private void Start()
    {
        GenerateBouchonEvier();
    }

    void GenerateBouchonEvier()
    {
        Rigidbody2D prevBod = hook;
        for(int i = 0; i< numLinks; i++)
        {
            int index = i % prefabBilleSegs.Length;
            GameObject newSeg = Instantiate(prefabBilleSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody= prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
