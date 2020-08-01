using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    NotReady = 0, Ready = 1, Paused = 2, OnGoing = 3, Aborted = 4, Closed = 5
}
public enum WinType
{
    HitlerDead = 0, LiberalFull = 1, HitlerSelected = 2, FascistFull = 3
}
public enum TurnState
{
    SameTurn = 0, NewTurn = 1,
}
public enum SecretRole
{
    Liberal = 0, Fascist = 1, Hitler = 2
}
public enum CardType
{
    Liberal = 0, Fascist = 1
}
public enum Power
{
    Investigate = 0, Peek = 1, Select = 2, Kill = 3
}