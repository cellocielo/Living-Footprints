using UnityEngine;

public class CameraFollowSpecificParticle : MonoBehaviour
{
    public Transform carTarget;
    public ParticleSystem particleSystem;
    public float switchTime = 10f;
    public float followSpeed = 5f;
    public Vector3 offset = new Vector3(0, 2, -5);
    public int particleIndex = 0;
    
    private float timer = 0f;
    private bool followingParticle = false;
    private ParticleSystem.Particle[] particles;
    private Vector3 targetParticlePosition;
    
    void Start()
    {
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }
    
    void LateUpdate()
    {
        timer += Time.deltaTime;
        
        if (timer >= switchTime && !followingParticle)
        {
            followingParticle = true;
            Debug.Log("Switching to follow particle #" + particleIndex);
        }
        
        if (!followingParticle)
        {
            FollowCar();
        }
        else
        {
            FollowSpecificParticle();
        }
    }
    
    void FollowCar()
    {
        if (carTarget != null)
        {
            Vector3 desiredPosition = carTarget.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.LookAt(carTarget);
        }
    }
    
    void FollowSpecificParticle()
{
    int particleCount = particleSystem.GetParticles(particles);
    
    if (particleCount > particleIndex && particleIndex >= 0)
    {
        // Follow the particle at the specified index
        targetParticlePosition = particles[particleIndex].position;
        
        // Flip the offset so camera is positioned correctly for the new view direction
        Vector3 particleOffset = new Vector3(0, 0, 5); // Notice the positive Z instead of negative
        Vector3 desiredPosition = targetParticlePosition + particleOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        
        // Look away from city toward blank landscape
        Vector3 lookDirection = Vector3.back; // Adjust as needed
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), followSpeed * Time.deltaTime);
    }
}
}