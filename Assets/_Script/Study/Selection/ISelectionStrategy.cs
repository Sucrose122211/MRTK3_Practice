using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectionStrategy
{
    void OnSelect(out GameObject target);
}
