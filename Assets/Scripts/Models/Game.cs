using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game 
{
    public string chancellorId;
    public string presidentId;
    public int gameId;
    public GameStatus status;
    public int electionFailTracker;
    public int numberOfRounds;
    public List<Player> players;
    public List<Card> remainingCards;
    public List<Card> discardedCards;
    public List<Card> inHandCards;
    public List<Card> onTableCards;
    public int nbreOfPeeks;
    public int nbreOfKills;
    public int nbreOfInvestigation;
    public int nbreOfPresidentSelection;
    public WinType winType;
    public Player chancellor;
    public TurnState turnState;

}
