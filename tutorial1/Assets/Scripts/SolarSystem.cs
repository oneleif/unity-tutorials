using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public GameObject earth;
    public Rigidbody earthRigidBody;
    public GameObject sun;
    public Rigidbody sunRigidBody;
     
    public double gravitationalConstant = 6.674; // 0.00000000006674
    //public double earthsMass = 5.972e24;
    double earthsMass = 5.972e2;
    //public double sunsMass = 1.989e30;
    double sunsMass = 8.989e7;
    public double scale = 1e-10;
    public double distanceBetweenSunAndEarth = 1.5e11;

    public Vector3 currentVelocity;


    void Start()
    {
        earthRigidBody = earth.GetComponent<Rigidbody>();
        sunRigidBody = sun.GetComponent<Rigidbody>();
        earthRigidBody.AddForce(earth.transform.forward * 100);
    }

    void Update()
    {
        //ApplyForceToSun();
        ApplyForceToEarth();
        currentVelocity = earthRigidBody.velocity;


    }

    void ApplyForceToEarth()
    {
        double distance = Vector3.Distance(earth.transform.position, sun.transform.position);
        //g * m * m / r * r
        double forceToApply = gravitationalConstant * earthsMass * sunsMass / (distance * distance);
        Vector3 forceDirection = sun.transform.position - earth.transform.position;
        Vector3 force = forceDirection * (float) forceToApply;
        earthRigidBody.AddForce(force);
    }
}
