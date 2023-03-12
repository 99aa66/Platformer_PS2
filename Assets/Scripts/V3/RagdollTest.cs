using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTest : MonoBehaviour
{
    private RagdollController rdController;
    // Start is called before the first frame update
    void Start()
    {
        rdController= GetComponent<RagdollController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!rdController.RagdollActive) 
            {
                rdController.ActivateRagdoll();
            }
            else 
            {
                rdController.DisableRagdoll();
            }
        }
    }
}
