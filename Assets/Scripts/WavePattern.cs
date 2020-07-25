using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanePosition
{
    Top,
    Mid,
    Bot
}

public class WavePattern : MonoBehaviour
{
    public List<LanePosition> pattern;

}
