using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedManager : ManagerBase
{
    private GameInstance GI;
    private FeedFactory _factory;
    private bool isTiming = true;
    private const float delay = 5;

    public override void OnAwake()
    {
        base.OnAwake();
        _factory = new FeedFactory(GameInstance.I.Plane);
        GI = GameInstance.I;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(!isTiming) return;
        GI.CoroutineHelp(GenerateCoroutine());
    }

    IEnumerator GenerateCoroutine()
    {
        _factory.GenerateRandom();
        isTiming = false;
        yield return new WaitForSeconds(delay);
        isTiming = true;
    }
}
