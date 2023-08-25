using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_SlideThreshold = 25;		
	[SerializeField] private float m_SlideFactor = 3;	
	[Range(0, 1)] [SerializeField] private float m_SlideJumpBoost = .3f;	
	[SerializeField] private float m_SlideJumpThrehold = .3f;	
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private float m_AirSpeed = 10;
	[SerializeField] private float m_JumpForce = 400f;							
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_slideStarted = false;
	const float k_CeilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	bool crouch = false;
	public void Move(float move, bool crouchInput, bool jump)
	{
		crouch = crouchInput;
		if(m_Grounded && crouch && Mathf.Abs(m_Rigidbody2D.velocity.x) > Mathf.Max(Mathf.Abs(move),m_SlideThreshold) * m_CrouchSpeed && !m_slideStarted)
		{
			m_slideStarted = true;
		}

		if(m_slideStarted)
		{
			if(!m_Grounded || Mathf.Abs(m_Rigidbody2D.velocity.x) < Mathf.Max(Mathf.Abs(move),3) * m_CrouchSpeed)
			{
				m_slideStarted = false;
			}
			move *= 0;
		}

		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			
		}
		
		if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
				jump = false;
			}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_slideStarted?m_SlideFactor:m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		if(m_AirControl && !m_Grounded)
		{
			m_Rigidbody2D.AddForce(new Vector2(move*m_AirSpeed,0));
		}

		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			// m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			if(m_slideStarted)
			{
				if(Mathf.Abs(m_Rigidbody2D.velocity.x) < m_SlideJumpThrehold)
				m_Rigidbody2D.velocity = (new Vector2(m_Rigidbody2D.velocity.x + Mathf.Sign(m_Rigidbody2D.velocity.x) * m_JumpForce*m_SlideJumpBoost, (m_SlideJumpBoost+1)*m_JumpForce));
			}
			else
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,m_JumpForce);
			}
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isGrounded()
	{
		return m_Grounded;
	}
	public bool isCrouching()
	{
		return crouch;
	}
	public bool isSliding()
	{
		return m_slideStarted;
	}
	public float getXVelocity()
	{
		return m_Rigidbody2D.velocity.x;
	}
}
