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
[RequireComponent(typeof(EdgeCollider2D))]
public class Terrain : PhysicsObject
{
    
    static List<Terrain> s_terrainComponents= new List<Terrain>();
    public static Vector2 GetTerrainNormal(float x)
    { 
        Vector2 direction=Vector2.zero;
        Vector2 point1 = Vector2.zero;
        Vector2 point2 = Vector2.zero;
        for(int i=0; i<s_terrainComponents.Count; i++)
        {
            int position = s_terrainComponents[i].GetPointPosition(x);
            if(position==0 && i>0)
            {
                point1 = s_terrainComponents[i - 1].GetComponent<EdgeCollider2D>().points[s_terrainComponents[i - 1].GetComponent<EdgeCollider2D>().points.Length - 2];
                point2= s_terrainComponents[i].GetComponent<EdgeCollider2D>().points[0];
                direction=(point2-point1).normalized;
            }
            if(position>0)
            {
                point1 = s_terrainComponents[i].GetComponent<EdgeCollider2D>().points[position - 1];
                point2 = s_terrainComponents[i].GetComponent<EdgeCollider2D>().points[position];
                direction = (point2 - point1).normalized;
            }

        }
        Vector2 normal = new Vector2(direction.y, -direction.x);
        if (normal.y < 0)
            normal = -normal;
        Vector3 midpoint=new Vector3(point1.x+point2.x, point1.y+point2.y)/2;
        Debug.DrawRay(midpoint, new Vector3(normal.x, normal.y), Color.red);
        return normal;
    }
    int GetPointPosition(float x)
    {
        float xCoord=x-transform.position.x;
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        int position=-1;
        if (xCoord < collider.points[0][0])
        { 
            position = 0; 
        }
        else
        {
            for (int i = 1; i < collider.points.Length; i++)
            {
                if (xCoord < collider.points[i][0] && xCoord > collider.points[i - 1][0])
                    position = i - 1;
            }
        }
        return position;
    }
    protected override void Start()
    {
        if (!s_terrainComponents.Contains(this))
            s_terrainComponents.Add(this);
        s_terrainComponents.OrderBy(x => x.transform.position.x);
    }
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
	}

	protected override void OnCollisionExit2D(Collision2D collision)
	{
	}

	protected override void OnHit(HitInfo info)
	{
	}

	protected override void FixedUpdate()
	{
	}
}

