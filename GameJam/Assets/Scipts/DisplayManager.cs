using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayManager : MonoBehaviour
{
    [SerializeField] Text displayText;
    [SerializeField] CanvasGroup cg;
    float targetTransparency = 0;
    float transparency = 0;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("FoodItem")))
        {
            string ingName = hit.collider.gameObject.name.Split()[0];
            Ingredient ing = (Ingredient)Enum.Parse(typeof(Ingredient), ingName);
            int amount = GameManager.inst.IngredientAmount.ContainsKey(ing) ? GameManager.inst.IngredientAmount[ing] : 0;
            displayText.text = ingName + " - " + amount + "/" + ing.GetMax();
            targetTransparency = 1;
        }
        else
        {
            targetTransparency = 0;
        }

        transparency = Mathf.Lerp(transparency, targetTransparency, 0.15f);
        cg.alpha = transparency;

    }
}
