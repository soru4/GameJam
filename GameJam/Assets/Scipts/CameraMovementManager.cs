using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementManager : MonoBehaviour
{
    Transform cam;
    AnimationCurve smooth;
    [SerializeField] Transform mainTransform, movedTransform;
    Vector3 mainPos, movedPos;
    Quaternion mainRot, movedRot;
    [SerializeField] float moveTime;
    bool toMainPos = true;

    // Start is called before the first frame update
    void Start()
    {
        smooth = RoundManager.inst.smooth;
        cam = Camera.main.transform;

        mainPos = mainTransform.position;
        movedPos = movedTransform.position;
        mainRot = mainTransform.rotation;
        movedRot = movedTransform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TogglePos();
    }

    public void TogglePos()
    {
        toMainPos = !toMainPos;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float start = Time.time;
        Vector3 startPos = toMainPos ? movedPos : mainPos;
        Vector3 endPos = toMainPos ? mainPos : movedPos;
        Quaternion startRot = toMainPos ? movedRot : mainRot;
        Quaternion endRot = toMainPos ? mainRot : movedRot;

        while (Time.time < start + moveTime)
        {
            float t = (Time.time - start) / moveTime;
            Vector3 v3 = Vector3.Lerp(startPos, endPos, smooth.Evaluate(t));
            Quaternion q = Quaternion.Lerp(startRot, endRot, smooth.Evaluate(t));
            cam.SetPositionAndRotation(v3, q);
            yield return null;
        }

        cam.SetPositionAndRotation(endPos, endRot);
    }

}
