using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Feed : SelfDestructive
{
    private Vector3 _moveDirection;
    private float _moveSpeed;
    private bool isDestroyable = false;
    // Start is called before the first frame update
    void Start()
    {
        DestroyCondition = CheckDestroyCondition;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    public void OnInstantiate(Vector3 dir, float speed)
    {
        _moveDirection = dir;
        _moveSpeed = speed;
    }

    bool CheckDestroyCondition()
    {
        return isDestroyable;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Background"))
        {
            isDestroyable = true;
        }    
    }

    // TOOD: Event Delegate 형식으로 변경
    // TODO: DataManager 제작(Save data)
    private void OnDestroy() {
        var GI = GameInstance.I;
        var manager = GI.GazeManager;
        Debug.Log($"Gaze Origin: {manager.GazeOrigin}, \nGaze Direction: {manager.GazeVector} \nObject Position: {this.transform.position}, \nObject Speed: {_moveSpeed}");
        GI.Score = GI.Score + 1;
    }
}
