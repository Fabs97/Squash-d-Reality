using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour {

	void Start () {
		
	}

    Vector3 velocity;

    Vector3 bestGuessPosition;

    // TODO: This should probably be something we get from the PlayerConnectionObject
    float ourLatency;   

    // This higher this value, the faster our local position will match the best guess position
    float latencySmoothingFactor = 10;

	void Update () {
        if( hasAuthority == false )
        {
            bestGuessPosition = bestGuessPosition + ( velocity * Time.deltaTime );
            transform.position = Vector3.Lerp( transform.position, bestGuessPosition, Time.deltaTime * latencySmoothingFactor);
            return;
        }

        transform.Translate( velocity * Time.deltaTime );

        if( Input.GetKeyDown(KeyCode.Space) )
        {
            this.transform.Translate( 0, 1, 0 );
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            velocity = new Vector3(1, 0, 0);
            CmdUpdateVelocity(velocity, transform.position);
        }

	}

    [Command]
    void CmdUpdateVelocity( Vector3 v, Vector3 p)
    {
        transform.position = p;
        velocity = v;

        RpcUpdateVelocity( velocity, transform.position);
    }

    [ClientRpc]
    void RpcUpdateVelocity( Vector3 v, Vector3 p )
    {
        if( hasAuthority ) return;

        velocity = v;
        bestGuessPosition = p + (velocity * (ourLatency));
    }
}
