using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedManager : ManagerBase
{
    private GameInstance GI;
    private FeedFactory _factory;
    private bool isActive = true;
    private bool isTiming = true;
    private const float delay = 5;

    public override void OnAwake()
    {
        base.OnAwake();
        GI = GameInstance.I;
        if(GI.UserType == EUSERTYPE.RECIEVER) isActive = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(!isActive || !isTiming) return;
        GI.CoroutineHelp(GenerateCoroutine());
    }

    IEnumerator GenerateCoroutine()
    {
        if(_factory == null) yield break;

        _factory.GenerateRandom();
        isTiming = false;
        yield return new WaitForSeconds(delay);
        isTiming = true;
    }

    public void GetFeedFactory(GameObject plane)
    {
        _factory ??= new FeedFactory(plane);
    }

}
