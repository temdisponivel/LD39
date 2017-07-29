using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfig : MonoBehaviour
{
    public static SceneConfig Instance;
    public Transform PlayerStartPoint;
    public CameraData InitialCameraData;
    public bool SetCameraPinToPlayer;

    public Cable[] CablesThatStartEnabled;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void Setup()
    {
        var playerTrans = PlayerController.Instance.transform;

        playerTrans.position = PlayerStartPoint.transform.position;
        playerTrans.rotation = PlayerStartPoint.transform.rotation;

        if (SetCameraPinToPlayer)
            InitialCameraData.TransformToFollow = PlayerController.Instance.transform;

        CameraController.Instance.SetData(InitialCameraData);

        for (int i = 0; i < CablesThatStartEnabled.Length; i++)
            CablesThatStartEnabled[i].Lit();
    }
}
