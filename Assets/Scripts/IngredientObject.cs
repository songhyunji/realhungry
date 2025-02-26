﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public static List<IngredientObject> activatedOb;
    public static Queue<IngredientObject> deactivatedOb;

    private static int max_Ob = 10;
    private static IngredientObject prefab;

    public static void Init_Ob(IngredientObject _prefab)
    {
        prefab = _prefab;
        activatedOb = new List<IngredientObject>();
        deactivatedOb = new Queue<IngredientObject>();

        for (int i = 0; i < max_Ob; i++)
        {
            IngredientObject temp = Instantiate(prefab);
            deactivatedOb.Enqueue(temp);
            temp.gameObject.SetActive(false);
        }
    }

    public static void ResetObject()
    {
        while(activatedOb.Count > 0)
        {
            activatedOb[0].DeactivateObject();
        }
    }

    public static IngredientObject Pull_Ob()
    {
        IngredientObject temp;
        if (deactivatedOb.Count == 0)
        {
            temp = Instantiate(prefab);
            activatedOb.Add(temp);

            max_Ob++;
            return temp;
        }

        temp = deactivatedOb.Dequeue();
        activatedOb.Add(temp);
        temp.gameObject.SetActive(true);
        return temp;
    }

    public SpriteRenderer spRenderer;
    public FoodIngredient data;

    public Rigidbody2D rigid;

    public void Init(FoodIngredient ing)
    {
        data = ing;
        spRenderer.sprite = data.sprite;
        rigid.sharedMaterial.bounciness = data.weight;
        
    }

    public void AddIngredient()
    {
        TestFoodMaker.Instance.AddIngredient(this.data);
    }

    public void DeactivateObject()
    {
        activatedOb.Remove(this);
        deactivatedOb.Enqueue(this);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.CompareTo("Bottom") == 0)
        {
            DeactivateObject();

            // 점수 감소
            if(data.score > 0)
            {
                FoodGameManager.Instance.Score -= data.score;
            }
            else
            {
                FoodGameManager.Instance.Score += data.score;
            }

        }
    }
    
    void Update()
    {
    }
}
