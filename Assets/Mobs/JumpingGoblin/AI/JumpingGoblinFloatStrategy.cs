﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class JumpingGoblinFloatStrategy : GoblinFloatStrategy
{
    public static new JumpingGoblinFloatStrategy Instance()
    {
        if (s_strategy == null)
            s_strategy = new JumpingGoblinFloatStrategy();
        return s_strategy;
    }
    static JumpingGoblinFloatStrategy s_strategy;
    public static float ParachuteOpenSpeed { get { return -1.5f; } }
    protected JumpingGoblinFloatStrategy()
    {
    }
	public override void SteerPhysics(BasicSteering controller)
	{
        JumpingGoblinSteering parentController = controller as JumpingGoblinSteering;
        Vector2 normal = Vector2.up;
        float angle = 0;
        angle = Vector2.Angle(parentController.transform.up, normal) * Mathf.Sign(parentController.transform.up.x - normal.x);
        angle = Utility.NormalizeAngle(angle);
        parentController.GetComponent<GoblinLocomotion>().KeepBalance(angle);
        if (controller.GetComponent<Rigidbody2D>().velocity.y < ParachuteOpenSpeed)
        {
            controller.SendMessage("Break", 4f);
            controller.SendMessage("ParachuteOpen");
        }

	}
    public override void SteerOther(BasicSteering controller)
    {
        JumpingGoblinSteering steering = controller as JumpingGoblinSteering;
        if (steering.SurfaceContact.normal != Vector2.zero)
        {
            controller.SetStrategy(JumpingGoblinMoveStrategy.Instance());
        }
    }
    public override void ExitState(BasicSteering controller)
    {
        controller.SendMessage("ParachuteClose");
    }
}

