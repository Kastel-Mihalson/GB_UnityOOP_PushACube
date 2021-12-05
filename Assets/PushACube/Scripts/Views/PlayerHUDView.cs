using System;
using UnityEngine;

public class PlayerHUDView : MonoBehaviour
{
    public event Action<int> CubeBonusCountChangedEvent;

    public void CurrentCubeBonusCount(int bonusCount)
    {
        CubeBonusCountChangedEvent?.Invoke(bonusCount);
    }
}
