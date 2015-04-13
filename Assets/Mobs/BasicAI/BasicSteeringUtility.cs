using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public static class BasicSteeringUtility
{
    public static List<RaycastHit2D> GetForwardObjectsList(BasicSteering controller, float range = 1f)
    {
        Collider2D controllerCollider = controller.GetComponent<Collider2D>();
        float xMax = controllerCollider.bounds.max.x;
        float xMin = controllerCollider.bounds.max.x;
        float yMax = controllerCollider.bounds.min.y;
        float yMin = controllerCollider.bounds.min.y;
        Vector2 dirVector = new Vector2(range, yMax - yMin);
        HashSet<RaycastHit2D> hits = new HashSet<RaycastHit2D>(Physics2D.RaycastAll(new Vector2(xMax, yMin), dirVector, dirVector.magnitude, Constants.RaycastMaskPhysics));
        //Debug.DrawRay(new Vector2(xMax, yMin), dirVector, Color.red);
        dirVector.y = -dirVector.y;
        hits.UnionWith(Physics2D.RaycastAll(new Vector2(xMax, yMax), dirVector, dirVector.magnitude, Constants.RaycastMaskPhysics));
        //Debug.DrawRay(new Vector2(xMax, yMax), dirVector, Color.red);
        return hits.ToList();
    }
}
