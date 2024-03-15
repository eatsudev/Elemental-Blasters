using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit {
    
    Vector3 GetPosition();
    void SetPosition(Vector3 position);
    CheckPoint GetLastCheckPoint();
    void SetCheckPoint(CheckPoint checkPoint);
}
