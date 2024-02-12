using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] List<Transform> locations;
    [SerializeField] float speedScaler;
    Transform cam;
    AnimationCurve smooth;

    private void Start()
    {
        smooth = RoundManager.inst.smooth;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveToIndex(Random.Range(0, locations.Count));
        }
    }

    public void MoveToIndex(int index)
    {
        StopAllCoroutines();
        float dist = Vector3.Distance(cam.transform.position, locations[index].position);
        float dur = dist * speedScaler;
        StartCoroutine(SmoothLerp(cam, locations[index], dur));
    }

    IEnumerator SmoothLerp(Transform trStart, Transform trEnd, float dur)
    {
        Vector3 pStart = trStart.position, pEnd = trEnd.position;
        Quaternion rStart = trStart.rotation, rEnd = trEnd.rotation;

        float start = Time.time;
        while (Time.time < start + dur)
        {
            float tRaw = (Time.time - start) / dur;
            float time = smooth.Evaluate(tRaw);
            cam.SetPositionAndRotation(
                Vector3.Lerp(pStart, pEnd, time),
                Quaternion.Slerp(rStart, rEnd, time)
            );
            yield return null;
        }
        cam.SetPositionAndRotation(pEnd, rEnd);
    }

}
