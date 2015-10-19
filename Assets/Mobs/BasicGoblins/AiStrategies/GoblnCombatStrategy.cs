using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GoblinCombatStrategy : BasicSteeringStrategy {
    public static GoblinCombatStrategy Instance()
    {
        if (s_strategy == null)
            s_strategy = new GoblinCombatStrategy();
        return s_strategy;
    }
    static GoblinCombatStrategy s_strategy;

    public static readonly HashSet<string> TargetTags = new HashSet<string>(new string[] { "ShootingObject", "DestroyableObject", "MainTarget", "Enemy" });

    public static void CheckTargets(GoblinSteering controller, float distance=1f)
    {
        IEnumerable<GameObject> hitsTemp = from u in BasicSteeringUtility.GetForwardObjectsList(controller, distance)
                                           where TargetTags.Contains(u.collider.tag)
                                           where u.collider.gameObject != controller.gameObject
                                           orderby -u.point.x
                                           select u.collider.gameObject;
        controller.Targets = new List<GameObject>(hitsTemp);
        
    }
    protected GoblinCombatStrategy()
    {
    }
    public virtual void SteerOther(BasicSteering controller)
    {
        
        GoblinSteering steering = controller as GoblinSteering;
        if (steering.SurfaceContact.normal == Vector2.zero)
        {
            steering.SetStrategy(GoblinFloatStrategy.Instance());
        }
        CheckTargets(steering);
        if (steering.Targets.Count == 0 || steering.Targets[0].tag == "Enemy")
        {
            steering.SetStrategy(GoblinMoveStrategy.Instance());

        }
        else 
        {
            GameObject mainTarget = steering.Targets[0];
            
            steering.GetComponent<GoblinLocomotion>().Kick(mainTarget.GetComponent<PhysicsObject>());

        }
    }
    public virtual void SteerPhysics(BasicSteering controller)
    {
        
    }

    public virtual void ExitState(BasicSteering controller)
    {
        
    }

    public virtual void EnterState(BasicSteering controller)
    {
        
    }

    

}
