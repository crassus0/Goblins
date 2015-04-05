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

public class BasicSteering : MonoBehaviour
{
	private BasicSteeringStrategy Strategy
	{
		get;
		set;
	}

	protected virtual void Start()
	{
	}

	protected virtual void FixedUpdate()
	{
        Strategy.Steer(this);
	}
    public virtual void SetStrategy(BasicSteeringStrategy strategy)
    {
        if (Strategy!=null)
            Strategy.ExitState(this);
        Strategy = strategy;
        Strategy.EnterState(this);
    }
}

