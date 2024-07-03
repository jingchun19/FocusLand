using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent {}

public class NotEnoughCurrency : GameEvent
{
    public int amount;

    public NotEnoughCurrency(int amount)
    {
        this.amount = amount;
    }
}

public class CurrencyChange : GameEvent
{
    public int amount;

    public CurrencyChange(int amount)
    {
        this.amount = amount;
    }
}

public class EnoughCurrency : GameEvent
{
    
}

public class LevelChangeEvent : GameEvent
{
    public int newLevel;

    public LevelChangeEvent(int currLvl)
    {
        newLevel = currLvl;
    }
} 

public class XpAddEvent : GameEvent
{
    public int amount;

    public XpAddEvent(int amount)
    {
        this.amount = amount;
    }
}
