using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using UnityEngine.UI;

public enum EBUTTONTYPE
{
    SAVE, CLEAR
}

public class UIButton : UIBase
{
    // Start is called before the first frame update
    [SerializeField] private EBUTTONTYPE tpye;
    private Button m_button;
    public Button Button => m_button;
    void Start()
    {
        if(Initializer.UserType == EUSERTYPE.RECIEVER)
        {
            DontDestroyOnLoad(transform.parent.gameObject);
            m_button = GetComponent<Button>();

            StartCoroutine(OnClickEvent());
        }

        IEnumerator OnClickEvent()
        {
            yield return new WaitUntil(() => GameInstance.I != null);
            switch(tpye)
            {
                case EBUTTONTYPE.SAVE:
                    m_button.onClick.AddListener(GameInstance.I.DataManager.ExportAllJSON);
                    break;
                case EBUTTONTYPE.CLEAR:
                    m_button.onClick.AddListener(GameInstance.I.DataManager.ClearData);
                    break;
                default:
                    break;
            }
        }
    }
}
