using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] Transform scanPivot;

    private HashSet<GameObject> detectedObject = new HashSet<GameObject>();
    
    public delegate void OnTargetDetectedDelegate(GameObject newTarget);

    public event OnTargetDetectedDelegate OnTargetDetected;
    public Transform ScanPivot
    {
        get => scanPivot;
    }


    public void StartScan(float scanRadius, float scanDuration)
    {
        StartCoroutine(ScanCoroutine(scanRadius, scanDuration));
    }

    IEnumerator ScanCoroutine(float scanRadius, float scanDuration)
    {
        float counter = 0f;
        float growRate = scanRadius / scanDuration;
        scanPivot.transform.localScale = Vector3.zero;
        while (counter < scanDuration)
        {
            counter += Time.deltaTime;
            scanPivot.transform.localScale += growRate * Time.deltaTime * Vector3.one;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectedObject.Contains(other.gameObject))
            return;
        
        detectedObject.Add(other.gameObject);
        OnTargetDetected?.Invoke(other.gameObject);
    }
}
