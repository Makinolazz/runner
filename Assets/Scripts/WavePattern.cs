using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanePosition
{
    Top,
    Mid,
    Bot
}

public enum ObstacleType
{
    TallWall,
    SmallWall,
    GroundBarrier,
    AirBarrier,
    GroundRamp,
    AirRamp,
    ShortPit,
    MidPit,
    LongPit
}

public class WavePattern : MonoBehaviour
{
    [SerializeField] public int amountOfObstacles;
    public List<LanePosition> pattern;
    public List<ObstacleType> obstacle;

}
