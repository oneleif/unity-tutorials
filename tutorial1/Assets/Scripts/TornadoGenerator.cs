using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoGenerator : MonoBehaviour
{
    public GameObject particlePrefab;
    private int numberOfParticles = 20;

    public float currentY = 0;
    public Material[] materials;

    void Start()
    {
        ConnectChainToParent(transform);
        ConnectChainToParent(transform);
        ConnectChainToParent(transform);
        ConnectChainToParent(transform);
    }

    void Update()
    {
        float currentTime = Time.time;
        currentY += 0.05f;
        transform.position = new Vector3(Mathf.Sin(currentTime) * 10, 0, Mathf.Cos(currentTime) * 10);
    }

    void ConnectChainToParent(Transform parent)
    {
        List<GameObject> particleChain = CreateNewChain();
        for (int i = 0; i < numberOfParticles; i++)
        {
            SpringJoint joint = particleChain[i].GetComponent<SpringJoint>();

            if (i == 0)
            {
                joint.connectedBody = parent.GetComponent<Rigidbody>();
            }
            else
            {
                joint.connectedBody = particleChain[i - 1].GetComponent<Rigidbody>();
                joint.spring = 100;
            }
        }
    }

    List<GameObject> CreateNewChain()
    {
        List<GameObject> particles = new List<GameObject>();

        for (int i = 0; i < numberOfParticles; i++)
        {
            GameObject newParticle = Instantiate(particlePrefab);
            MeshRenderer myRenderer = newParticle.GetComponent<MeshRenderer>();
            myRenderer.material = materials[Random.Range(0, materials.Length)];
            newParticle.AddComponent<SpringJoint>();
            particles.Add(newParticle);
        }

        return particles;
    }
}
