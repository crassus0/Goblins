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

public abstract class BasicLocomotion : MonoBehaviour
{
	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
	}

	public abstract void MoveForward(float speed);
    public abstract void Kick(GameObject target);
}

