using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBonusController
{
    private CubeBonusModel _cubeBonusModel;
    private CubeBonusView _cubeBonusView;

    public CubeBonusController(CubeBonusModel model, CubeBonusView view)
    {
        _cubeBonusModel = model;
        _cubeBonusView = view;
    }
}
