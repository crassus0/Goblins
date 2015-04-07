using UnityEngine;
using System.Collections;

public class CatapultVisualizer : MonoBehaviour 
{
    public GameObject Shell;
    public GameObject EmptyShell;
    public Transform ShellTransform;

    public void DisconnectShell()
    {
        Shell.transform.parent = null;
    }
}
