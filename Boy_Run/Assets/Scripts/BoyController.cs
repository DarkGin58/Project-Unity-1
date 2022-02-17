//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BoyController : MonoBehaviour
//{
//    public float jetpackForce = 30.0f;
//    public float forwardMovementSpeed;
//    public Transform groundCheckTransform;
//    private Animator animator;
//    private bool grounded;
//    public LayerMask groundCheckLayerMask;
//    public ParticleSystem jetpack;

//    void AdjustJetpack(bool jetpackActive)
//    {
//        ParticleSystem.EmissionModule jpEmission = jetpack.emission;
//        jpEmission.enabled = !grounded;
//        jpEmission.rateOverTime = jetpackActive ? 300.0f : 75.0f;

//    }

//    private void FixedUpdate()
//    {
//        bool jetpackActive = Input.GetButton("Fire1");

//        if(jetpackActive)
//        {
//            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetpackForce));
//        }

//        Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
//        newVelocity.x = forwardMovementSpeed;
//        GetComponent<Rigidbody2D>().velocity = newVelocity;

//        UpdateGroundedStatus();

//        AdjustJetpack(jetpackActive);
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void UpdateGroundedStatus()
//    {
//        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
//        animator.SetBool("grounded", grounded);
//    }
//}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BoyController : MonoBehaviour
{

	public float jetpackForce = 75.0f;

	public float lives = 1.0f;


	public float forwardMovementSpeed = 3.0f;

	public Transform groundCheckTransform;

	private bool grounded;

	public LayerMask groundCheckLayerMask;

	Animator animator;

	public ParticleSystem jetpack;

	private bool dead = false;

	private uint coins = 0;

	public Texture2D coinIconTexture;
	public Texture2D lifeIconTexture;

	public AudioClip coinCollectSound;

	public AudioSource jetpackAudio;

	public AudioSource footstepsAudio;


	public GameObject restartDialog;
	public GameObject WinDialogue;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		restartDialog.SetActive(false);
		WinDialogue.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		bool jetpackActive = Input.GetButton("Fire1");

		jetpackActive = jetpackActive && !dead;

		if (jetpackActive)
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetpackForce));
		}

		if (!dead)
		{
			Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
			newVelocity.x = forwardMovementSpeed;
			GetComponent<Rigidbody2D>().velocity = newVelocity;
		}

		UpdateGroundedStatus();

		AdjustJetpack(jetpackActive);

		AdjustFootstepsAndJetpackSound(jetpackActive);

		
	}

	void UpdateGroundedStatus()
	{
		//1
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);

		//2
		animator.SetBool("grounded", grounded);
	}

	void AdjustJetpack(bool jetpackActive)
	{
		jetpack.enableEmission = !grounded;
		jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Coins"))
			CollectCoin(collider);
		else
			HitByLaser(collider);
		
	}

	void HitByLaser(Collider2D laserCollider)
	{
		
		if (!dead)
		{
			laserCollider.gameObject.GetComponent<AudioSource>().Play();
			lives--;
			coins = 0;
			if (lives == 0)
			{
				dead = true;

				animator.SetBool("dead", true);
				restartDialog.SetActive(true);
			}

		}
	}


	public void RestartGame()
	{
		//Application.LoadLevel(Application.loadedLevelName);
		SceneManager.LoadScene("SampleScene");
	}

	public void ExitToMenu()
	{
		Application.LoadLevel("MenuScene");
	}

	void CollectCoin(Collider2D coinCollider)
	{
		coins++;
		
		Destroy(coinCollider.gameObject);

		AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);

		if (coins == 30)
        {
			dead = true;
			WinDialogue.SetActive(true);
        }
	}

    void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !dead && grounded;

        jetpackAudio.enabled = !dead && !grounded;
        jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;
    }

    void DisplayCoinsCount()
	{
		Rect coinIconRect = new Rect(10, 10, 32, 32);
		GUI.DrawTexture(coinIconRect, coinIconTexture);

		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect(coinIconRect.xMax, coinIconRect.y, 60, 32);
		GUI.Label(labelRect, coins.ToString(), style);
	}

	void DisplaylivesCount()
	{
		Rect lifeIconRect = new Rect(100, 10, 32, 32);
		GUI.DrawTexture(lifeIconRect, lifeIconTexture);

		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.red;

		Rect labelRect = new Rect(lifeIconRect.xMax, lifeIconRect.y, 60, 32);
		GUI.Label(labelRect, lives.ToString(), style);
	}
	void OnGUI()
	{
		DisplayCoinsCount();
		DisplaylivesCount();
	}
}
