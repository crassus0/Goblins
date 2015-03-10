using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
    public static int RaycastMaskPhysics;
	void Awake () 
    {
	    RaycastMaskPhysics = LayerMask.GetMask("Physics Objects");
	}
}
