using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] CanvasGroup cg;
    float currentOpac;
    float targetOpac;
    [SerializeField] float interp;


    private void Start()
    {
        currentOpac = 0; targetOpac = 0;
    }

    private void Update()
    {
        RaycastHit hit;
        bool hitting = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("FoodItem"));

        if (hitting)
        {
            CancelInvoke();
            text.text = hit.collider.gameObject.name;
            targetOpac = 1;
        }
        else
        {
            targetOpac = 0;
        }



        currentOpac = Mathf.Lerp(currentOpac, targetOpac, interp);
        if (Mathf.Abs(cg.alpha - 0.5f) > 0.49f)
            currentOpac = targetOpac;

        cg.alpha = currentOpac;
    }

}
