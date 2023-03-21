using System.Collections.Generic;
using UnityEngine;

public class JointCreator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allBones;

    [SerializeField]
    private JointType jointType;

    [ContextMenu("CreateJoints")]
    private void CreateJoints()
    {
        allBones = new List<GameObject>();

        GetBone(transform);

        switch (jointType)
        {
            case JointType.HingeJoint:
                Rigidbody2D previousRB = null;

                for (int i = 0; i < allBones.Count; i++)
                {
                    allBones[i].AddComponent<HingeJoint2D>();

                    if (previousRB != null)
                        allBones[i].GetComponent<HingeJoint2D>().connectedBody = previousRB;

                    previousRB = allBones[i].GetComponent<Rigidbody2D>();
                }

                break;

            case JointType.CharacterJoint:
                Rigidbody previousBody = null;

                for (int i = 0; i < allBones.Count; i++)
                {
                    allBones[i].AddComponent<CharacterJoint>();

                    if (previousBody != null)
                        allBones[i].GetComponent<CharacterJoint>().connectedBody = previousBody;

                    previousBody = allBones[i].GetComponent<Rigidbody>();
                }

                break;

        }


    }

    private void GetBone(Transform bone)
    {
        allBones.Add(bone.gameObject);

        foreach (Transform child in bone)
        {
            GetBone(child);
        }
    }
}

public enum JointType { HingeJoint, CharacterJoint }
