using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingObject : MonoBehaviour
{
    [SerializeField] float m_SmoothSpeed = 5f;
    private Rigidbody2D m_RigidBody;
    private float m_CurrentSpeed;
    private float m_CurrentAcceleration;

    void Start ()
	{
        m_CurrentSpeed = m_SmoothSpeed;
        m_RigidBody = GetComponent <Rigidbody2D> ();
	}

	public void Move (float xDir, float yDir)
	{
        m_RigidBody.velocity = m_CurrentSpeed * (new Vector2 (xDir, yDir).normalized);
	}

    public void MoveAccelerate(float xDir, float yDir, float acceleration)
    {
        m_CurrentAcceleration = Mathf.Clamp(m_CurrentAcceleration + acceleration, 0, m_CurrentSpeed);
        m_RigidBody.velocity = m_CurrentAcceleration * (new Vector2(xDir, yDir).normalized);
    }

    public void MoveDecelerate(float xDir, float yDir, float acceleration)
    {
        m_CurrentAcceleration = Mathf.Clamp(m_CurrentAcceleration - acceleration, 0, m_CurrentSpeed);
        m_RigidBody.velocity = m_CurrentAcceleration * (new Vector2(xDir, yDir).normalized);
    }
    
    public void ApplyImpulse(Vector2 impulse)
    {
        m_RigidBody.AddForce (impulse, ForceMode2D.Impulse);
    }

    public void ApplyForce (Vector2 force)
    {
        m_RigidBody.AddForce (force, ForceMode2D.Impulse);
    }
    public void CancelVelocity()
    {
        m_RigidBody.velocity = Vector2.zero;
    }

    public void CancelXVelocity()
    {
        m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);
    }

    public void CancelYVelocity()
    {
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 0f);
    }

    public void ApplySpeedMultiplier(float muliplier)
    {
        m_CurrentSpeed *= muliplier;
    }

    public void MoveHorizontal (float xDir)
    {
        m_RigidBody.velocity = new Vector2 (xDir * m_CurrentSpeed, m_RigidBody.velocity.y);
    }

    public void Freeze()
    {
        m_RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Unfreeze()
    {
        m_RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetSmoothSpeed(float speed)
    {
        m_CurrentSpeed = speed;
    }

    public void ResetSmoothSpeed()
    {
        m_CurrentSpeed = m_SmoothSpeed;
    }

    public void ResetAcceleration()
    {
        m_CurrentAcceleration = 0f;
    }
}