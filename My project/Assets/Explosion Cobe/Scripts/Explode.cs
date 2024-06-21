using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public void Run(Cube explosionCube)
    {
        foreach (Rigidbody exploadableCube in GetExplodableObjects(explosionCube))        
            exploadableCube.AddExplosionForce(explosionCube.ExplosionForce, explosionCube.transform.position, explosionCube.ExplosionRadius);		
    }

    private List<Rigidbody> GetExplodableObjects(Cube explosionCube)
    {
        Collider[] explodableColliders = Physics.OverlapSphere(explosionCube.transform.position, explosionCube.ExplosionRadius);

        List<Rigidbody> explodableRigidbodies = new();

        foreach (Collider explodableCollider in explodableColliders)        
            if (explodableCollider.attachedRigidbody!=null)            
                explodableRigidbodies.Add(explodableCollider.attachedRigidbody);

        return explodableRigidbodies;
	}
}
