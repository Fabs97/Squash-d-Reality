using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    [SyncVar] public string shooterName;
    [SyncVar] public string bulletName;
}
