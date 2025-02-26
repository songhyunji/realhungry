﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class FoodIngredient : ScriptableObject, IEquatable<FoodIngredient>, IComparable<FoodIngredient>
{
    //이름, 그림, 설명, 획득 점수, 나올 확률, 무게
    public int id;
    public string nameStr;
    [TextArea]
    public string description;
    public Sprite sprite;
    public int score;
    public int probability;
    public float weight;

    public bool Equals(FoodIngredient other)
    {
        if (other == null) return false;
        return (this.id.Equals(other.id));
    }

    public int CompareTo(FoodIngredient comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return this.id.CompareTo(comparePart.id);
    }

#if UNITY_EDITOR
    public void SetData(Dictionary<string, object> data)
    {
        // id	name	description	score	probability	weight
        id = (int)data["id"];
        nameStr = (string)data["name"];
        description = (string)data["description"];
        score = (int)data["score"];
        probability = (int)data["probability"];
        weight = (float)data["weight"];
    }
#endif
}
