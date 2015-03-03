using UnityEngine;
using System.Collections;

public class AimCollider : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
            transform.parent.SendMessage("AddTarget",other.gameObject);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.SendMessage("RemoveTarget", other.gameObject);
    }
}
