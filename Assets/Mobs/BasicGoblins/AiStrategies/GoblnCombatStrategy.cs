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
        if (steering.Targets.Count == 0)
        {
            steering.SetStrategy(GoblinMoveStrategy.Instance());

        }
        else if (steering.Targets[0].tag != "Enemy")
        {
            GameObject mainTarget = steering.Targets[0];
            if (mainTarget.GetComponent<Collider2D>().bounds.min.x - steering.GetComponent<Collider2D>().bounds.max.x > 0.1)
            {
                steering.GetComponent<GoblinLocomotion>().MoveForward(20 * Time.deltaTime / Time.fixedDeltaTime);
            }

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
