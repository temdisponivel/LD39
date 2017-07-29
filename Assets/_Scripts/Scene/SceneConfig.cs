using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfig : MonoBehaviour
{
    public static SceneConfig Instance;
    public Transform PlayerStartPoint;

    void Awake()
    {
        Instance = this;
    }
    
    public void Setup()
    {
        var playerTrans = PlayerController.Instance.transform;

        playerTrans.position = PlayerStartPoint.transform.position;
        playerTrans.rotation = PlayerStartPoint.transform.rotation;
    }
}
