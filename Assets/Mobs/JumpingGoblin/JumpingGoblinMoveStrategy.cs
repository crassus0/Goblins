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
public class JumpingGoblinMoveStrategy : GoblinMoveStrategy
{
	public override void Steer(BasicSteering parent)
	{
		JumpingGoblinSteering jumpParent=parent as JumpingGoblinSteering;
        base.Steer(jumpParent);

        jumpParent.DelayTime+=Time.fixedDeltaTime;
        if (jumpParent.DelayTime>2)
        {
            jumpParent.DelayTime-=2;
            jumpParent.SendMessage("Jump",new Vector2(2,10));
        }
       

	}

}

