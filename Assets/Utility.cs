using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Utility
{
    public static float NormalizeAngle(float angle)
    {
        angle=(angle+360)%360;
        return angle > 180 ? angle - 360 : angle;
    }
}
