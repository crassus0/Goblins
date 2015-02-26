using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public abstract class CreatableObject:PhysicsObject
{
    public abstract string IconName { get; }
    public abstract int Price { get; }
    
    public virtual void SetAngleOnCreation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public virtual void RotateOnCreation(float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }
    public virtual void ScaleOnCreation(float scale)
    {
        scale = transform.localScale.x + scale;
        transform.localScale = Vector2.one * scale;
    }
    public virtual void SetScaleOnCreation(float scale)
    {
        transform.localScale = Vector2.one * scale;
    }
    public virtual void DragOnCretaion(Vector2 position)
    {
        Vector3 newPosition=position;
        float yCoord = Terrain.TerrainHeight(position.x);
        newPosition.y = position.y > yCoord + renderer.bounds.extents.y ? position.y : yCoord + renderer.bounds.extents.y;
        transform.position = newPosition;
    }
    public void Place()
    {
        Level.CurrentLevel.CreateObject(this);
    }
}
