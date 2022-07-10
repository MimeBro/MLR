using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalTypes
{
    Neutral,//Beats nothing               | Resists nothing                              | Weak to Wind and Magic
    Water,//Beats Fire, Earth             | Resists Fire, Metal and itself               | Weak to Thunder and Nature
    Fire,//Beats Nature, Metal and Dark   | Resists Nature,Metal and itself              | Weak to Water and Earth
    Nature,//Beats Water and Earth        | Resists Water,Earth,Thunder and itself       | Weak to Fire,Wind and Metal
    Earth,//Beats Thunder and Fire        | Resists Thunder,Fire and itself              | Weak to Water,Nature and Metal
    Wind,//Beats Neutral and Nature       | Resists Nature,Earth and itself              | Weak to Thunder and Dark
    Thunder,//Beats Water, Wind and Metal | Resists Metal and itself                     | Weak to Earth
    Dark,//Beats Magic and Wind,          | Resists Magic and itself                     | Weak to Fire
    Magic,//Beats Neutral and itself      | Resists Water,Fire,Wind                      | Weak to Dark and Itself
    Metal,//Beats Earth and Nature         | Resists Neutral,Magic,Nature,Wind, itself    | Weak to Fire and Thunder
    None
}

#region Element Score

//Neutral: 0 advantages 0 resistances 2 weaknesses resisted by 1 = 3 Worst Offensive*
//Water: 2 advantages 2 resistances 2 weaknesses resisted by 2   = 8
//Fire: 3 advantages 2 resistances 2 weaknesses resisted by 2    = 9
//Nature: 2 advantages 3 resistances 3 weaknesses resisted by 3  = 9 
//Earth: 2 advantages  2 resistances 3 weaknesses resisted by 2  = 10 Worst Defensive*
//Wind: 2 advantages 2 resistances 2 weakness resisted by 2      = 8
//Thunder: 3 advantages 1 resistances 1 weakness resisted by 2   = 6
//Dark: 2 advantages 1 resistances 1 weaknesses resisted by 0    = 5  Best Offensive*
//Magic: 2 advantages 3 resistances 2 weaknesses resisted by 1   = 9
//Metal: 2 advantages 5 resistances 2 weakness resisted by 3     = 10  Best Defensive*

#endregion

public class ElementalInteractions
{
    public static float ElementalInteraction(ElementalTypes attacker, ElementalTypes receiver)
    {
        var element = receiver;
        
        switch (attacker)
        {
            //If received a Neutral attack
            case ElementalTypes.Neutral:
                return element switch
                {
                    //Resists
                    ElementalTypes.Metal => 0.5f,
                    _ => 1f
                };
            //If received a Water Attack
            case ElementalTypes.Water:
                return element switch
                {
                    //Beats Fire and Earth
                    ElementalTypes.Fire or ElementalTypes.Earth => 2f,
                    //Resisted by Water, Nature and Magic
                    ElementalTypes.Water or ElementalTypes.Nature or  ElementalTypes.Magic => 0.5f,
                    _ => 1f
                };
            //If received a Fire Attack
            case ElementalTypes.Fire:
                return element switch
                {
                    //Beats Nature, Metal and Dark
                    ElementalTypes.Nature or ElementalTypes.Metal or ElementalTypes.Dark => 2f,
                    //Resisted by Fire, Water, Earth and Magic
                    ElementalTypes.Fire or ElementalTypes.Earth or ElementalTypes.Magic or ElementalTypes.Water => 0.5f,
                    _ => 1f
                };
            //If received a Nature Attack
            case ElementalTypes.Nature:
                return element switch
                {
                    //Beats Water 
                    ElementalTypes.Water or ElementalTypes.Earth => 2f,
                    //Resisted by Fire, Nature and Metal
                    ElementalTypes.Fire or ElementalTypes.Nature or ElementalTypes.Metal=> 0.5f,
                    _ => 1f
                };
            //If received an Earth Attack
            case ElementalTypes.Earth:
                return element switch
                {
                    //Beats Fire and Thunder
                    ElementalTypes.Fire or ElementalTypes.Thunder => 2f,
                    //Resisted by Earth, Magic, Wind and Metal
                    ElementalTypes.Earth or ElementalTypes.Wind or ElementalTypes.Metal => 0.5f,
                    _ => 1f
                };
            //If received a Wind attack
            case ElementalTypes.Wind:
                return element switch
                {
                    //Beats Nature and Neutral
                    ElementalTypes.Nature or ElementalTypes.Neutral => 2f,
                    //Resisted by Wind, Earth, Metal and Magic
                    ElementalTypes.Wind or ElementalTypes.Metal or ElementalTypes.Magic => 0.5f,
                    _ => 1f
                };
            //If received a Thunder Attack
            case ElementalTypes.Thunder:
                return element switch
                {
                    //Beats Water, Wind and Metal
                    ElementalTypes.Water or ElementalTypes.Wind or ElementalTypes.Metal => 2f,
                    //Resisted by Thunder and Earth
                    ElementalTypes.Thunder or ElementalTypes.Earth or ElementalTypes.Nature=> 0.5f,
                    _ => 1f
                };
            //If received a Dark Attack
            case ElementalTypes.Dark:
                return element switch
                {
                    //Beats Magic
                    ElementalTypes.Magic or ElementalTypes.Wind => 2f,
                    //Resisted by Dark, Thunder and Fire
                    ElementalTypes.Dark => 0.5f,
                    _ => 1f
                };
            //If received a Magic Attack
            case ElementalTypes.Magic:
                return element switch
                {
                    //Beats Neutral, Metal and Magic
                    ElementalTypes.Neutral or ElementalTypes.Magic => 2f,
                    //Resisted by Dark and Metal
                     ElementalTypes.Dark or ElementalTypes.Metal=> 0.5f,
                    _ => 1f
                };
            //if received a Metal attack
            case ElementalTypes.Metal:
                return element switch
                {
                    //Beats Nature and Earth
                    ElementalTypes.Earth or ElementalTypes.Nature=> 2f,
                    //Resisted by Fire and Metal
                    ElementalTypes.Fire or ElementalTypes.Metal or ElementalTypes.Thunder or ElementalTypes.Water=> 0.5f,
                    _ => 1f
                };
            default: return 1f;
        }
    }
}