
using Microsoft.MixedReality.Toolkit.MultiUse;

public abstract class ManagerBase
{
    protected ManagerBase()
    {
        var GI = GameInstance.I;
        if(GI == null) return;

        GI.AddManager(this);
    }
    public virtual void OnAwake() {}

    public virtual void OnDestroy() {}

    public virtual void OnStart() {}

    public virtual void OnUpdate() {}

    public virtual void OnSceneChange() {}
}