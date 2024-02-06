using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalIngredient : MonoBehaviour
{

    public string ingredientName = null;
    [Range(2, 20)]
    public int MaxCount = 10;
    [Range(0, 10)]
    public int MinCount = 0;
    public Ingredient ingredientType;
    public bool onBelt = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onBelt && (GameManager.inst.currentGameState == GameState.StopScroll || GameManager.inst.currentGameState == GameState.ContinueScroll || GameManager.inst.currentGameState == GameState.ShowScore))
        {
            transform.position += new Vector3(RoundManager.inst.beltSpeed * 20 * Time.deltaTime, 0, 0);
        }
        if (transform.position.x >= 270 && onBelt)
        {

            GameManager.inst.ingredientsOnBelt.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            GameManager.inst.updateIngredientAmount(this, ingredientType, +1);
            Debug.Log("+1 on" + gameObject.name);
            //increments up
        }else if (Input.GetMouseButton(1))
        {
            GameManager.inst.updateIngredientAmount(this, ingredientType, -1);
            // increments down
        }
    }

   
}

