using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapComponent
{
    public bool doorTop;
    public bool doorBottom;
    public bool doorLeft;
    public bool doorRight;
    public bool seen = false;

    public MapComponent(bool top, bool bottom, bool left, bool right)
    {
        doorTop = top;
        doorBottom = bottom;
        doorLeft = left;
        doorRight = right;
    }
}
