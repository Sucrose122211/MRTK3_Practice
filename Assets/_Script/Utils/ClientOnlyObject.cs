using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public class ClientOnlyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EUSERTYPE userType = Initializer.UserType;
        if(userType != EUSERTYPE.SENDER)
            gameObject.SetActive(false);
    }
}
