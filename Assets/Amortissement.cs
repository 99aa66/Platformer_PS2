using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amortissement : MonoBehaviour
{
    private HingeJoint hingeJoint;
    private Rigidbody connectedBody;

    public float damping = 10f;
    public float restitutionForce = 100f;

    private void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        connectedBody = hingeJoint.connectedBody;
    }

    private void FixedUpdate()
    {
        // Calcul de la force d'amortissement
        Vector3 dampingForce = -connectedBody.angularVelocity * damping;

        // Ajout de la force d'amortissement au rigidbody du corps attaché au joint
        connectedBody.AddTorque(dampingForce);

        // Calcul de la force de restitution
        if (hingeJoint.angle > hingeJoint.limits.max)
        {
            float maxRestitutionForce = (hingeJoint.angle - hingeJoint.limits.max) * restitutionForce;
            connectedBody.AddTorque(transform.forward * maxRestitutionForce);
        }
        else if (hingeJoint.angle < hingeJoint.limits.min)
        {
            float minRestitutionForce = (hingeJoint.angle - hingeJoint.limits.min) * restitutionForce;
            connectedBody.AddTorque(transform.forward * minRestitutionForce);
        }
    }
}
