using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry
{
    static public Vector3 PointFromGrid(Vector2 gridPoint)
    {
        //float x = -3.5f + 1.0f * gridPoint.x;
        //float z = -3.5f + 1.0f * gridPoint.y;
        float x = gridPoint.x;
        float z = gridPoint.y;
        return new Vector3(x, 0, z);
    }

    static public Vector2Int GridPoint(int col, int row)
    {
        return new Vector2Int(col, row);
    }

    static public Vector2 GridFromPoint(Vector3 point)
    {
        //float col = Mathf.FloorToInt(4.0f + point.x);
        //float row = Mathf.FloorToInt(4.0f + point.z);
        float col = point.x;
        float row = point.z;
        return new Vector2(col, row);
    }
}
