using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanePositions
{
    Top,
    Mid,
    Bot
}

public class WavePattern : MonoBehaviour
{
    public List<LanePositions> pattern;

    public int GetPosValue(int index)
    {
        return (int)pattern[index];
    }
}
