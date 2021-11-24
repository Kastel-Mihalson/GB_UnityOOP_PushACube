using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSettings
{
    protected const string HORIZONTAL = "Horizontal";
    protected const string VERTICAL = "Vertical";

    protected float moveSpeed = 5f;
    protected float turnSpeed = 2f;
    protected float maxHealth = 100;
}
