using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTestObject : MonoBehaviour, IGazeInteractable
{
    private const float rotateSpeed = 60f;

    public bool IsGazeOn { get; set; }

    private void Start() {
        IsGazeOn = false;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        Stop();
    }

    public void OnStay()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }

    private void Stop()
    {

    }
}
