using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPointPartDetect : MonoBehaviour
{
    public bool isOnDectectBall;
    public PartOfFoot ofFoot;

    public PartOfFoot GetFootOfPart()
    {
        return ofFoot;
    }
}
public enum PartOfFoot
{
    ½Å±³ÄÚ²à,
    ½ÅÄÚ²à,
    ½Å±³Íâ²à
}