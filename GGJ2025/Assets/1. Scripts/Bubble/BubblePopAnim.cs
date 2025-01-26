using UnityEngine;

public class BubblePopAnim : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem popParticleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BubblePopParticles()
    {
        popParticleSystem.Play();
    }
}
