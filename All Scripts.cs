public class HealthBar : MonoBehaviour
{
   public Slider slider;
   void Start()
   {
    slider=GetComponent<Slider>();
   }

   public void SetMaxHealth(int health)
   {
    slider.maxValue=health;
    slider.value=health;
   }
    public void SetHealth(int health)
    {
        slider.value=health;
    }
}

public class ScoreBar : MonoBehaviour
{
   public Slider slider;
   void Start()
   {
    slider=GetComponent<Slider>();
   }

   public void SetMaxScroe(int score)
   {
    slider.minValue=score;
    slider.value=score;
   }
    public void SetScore(int score)
    {
        slider.value=score;
    }
}

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed=2f;
    public float yOffset=1f;
    public Transform target;

    void Update()
    {
        Vector3 newPos=new Vector3(target.position.x,target.position.y + yOffset,-10f);
        transform.position=Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);  
    }

}

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;
   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {  
           if(collision.gameObject.tag=="Player")
        {
            
            playerHealth.TakeDamage(damage);
        
        }
            
    }
}

public class MonstorMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    void Update()
    {
        if(isChasing)
        {
            if(transform.position.x>playerTransform.position.x)
            {
                transform.localScale=new Vector3(4,4,4);
                transform.position +=Vector3.left * moveSpeed *Time.deltaTime;
            }
            if(transform.position.x<playerTransform.position.x)
            {
                transform.localScale=new Vector3(-4,4,4);
                transform.position +=Vector3.right * moveSpeed *Time.deltaTime;
            }

        }
        else
        {
            if(Vector2.Distance(transform.position,playerTransform.position) < chaseDistance)
            {
                isChasing=true;
            }

            if(patrolDestination==0)
        {
            transform.position=Vector2.MoveTowards(transform.position,patrolPoints[0].position,moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position,patrolPoints[0].position)<.2f)
            {
                transform.localScale=new Vector3(4,4,4);
                patrolDestination=1;
            }

        }
        if(patrolDestination==1)
        {
            transform.position=Vector2.MoveTowards(transform.position,patrolPoints[1].position,moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position,patrolPoints[1].position)<.2f)
            {
                transform.localScale=new Vector3(-4,4,4);
                patrolDestination=0;
            }
            
        }

        }
        
    }
}

public class NormalMusic : MonoBehaviour
{
    public AudioClip backgroundMusic;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;  
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            audioSource.Stop();
        }
    }
}

public class Blocker : MonoBehaviour
{
    
    [SerializeField] AudioClip clickSound;

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Blocker"))
        {
            AudioSource.PlayClipAtPoint(clickSound,other.transform.position);
            
        }
    }
}

public class CharacterController : MonoBehaviour
{
    

   [SerializeField] public float speed=5f;
   [SerializeField] public float jumpForce=10f;
   
   private SpriteRenderer spriteRenderer;
   bool jump=false;
   int jumpCounter=0;
   bool canJump=true;
   private Animator animator;


   void Start(){
    spriteRenderer=GetComponent<SpriteRenderer>();
    animator=GetComponent<Animator>();
    
   }
   
   
   public void Update(){
     CharacterControl();
     JumpControl();
     
    }
    
    void CharacterControl(){
        float horizontalMove=Input.GetAxis("Horizontal");
        Vector3 hareket=new Vector3(horizontalMove,0f,0f);
        transform.position += hareket*speed*Time.deltaTime;
        if(horizontalMove>0f){
            spriteRenderer.flipX=false;
        }
        else if(horizontalMove<0f){
            spriteRenderer.flipX=true;
        }
         if (Mathf.Abs(horizontalMove) > 0f)
        {
        animator.SetFloat("isRunning", Mathf.Abs(horizontalMove));
        animator.SetBool("isRunning", true);
        }
        else
        {
        animator.SetFloat("isRunning", 0f);
        animator.SetBool("isRunning", false);
        }
    }    
    void JumpControl(){
        if(Input.GetKeyDown(KeyCode.Space)&& !jump && canJump){
            jumpCounter=jumpCounter+1;
            animator.SetBool("jump",true);
            GetComponent<Rigidbody2D>().velocity=new Vector2(0f,jumpForce);
            if(jumpCounter>1)
            {
                canJump=false;
                jumpCounter=0;
            }
            
            jump=true;
        }
        else {
            animator.SetBool("jump",false);
            jump=false;
        }
    }
       
   
    public void FixedUpdate(){
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            jump=false;
            canJump=true;
        }
    }
    
 
}

public class FoxPower : MonoBehaviour
{
  [SerializeField] public AudioClip audioClip;
  [SerializeField] public AudioClip audioClip2;
    public ScoreBar scoreBar;
    public PlayerHealth playerHealth;
    public CharacterController characterController;
    int minPower=0;
    int currentPower=0;

    void Start()
    {
        currentPower=minPower;
        scoreBar.SetScore(minPower);
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentPower!=0)
        {
            
            if(playerHealth.currentHealth>=100)
            {
                playerHealth.currentHealth -= 20;
            }
            playerHealth.currentHealth += 20;
            currentPower -=1;
            scoreBar.SetScore(currentPower);
            playerHealth.healthBar.SetHealth(playerHealth.currentHealth);  
            characterController.jumpForce +=1;
            characterController.speed +=1;
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FoxPowerGems"))
        {
            AudioSource.PlayClipAtPoint(audioClip,other.transform.position);
            currentPower +=1;
            Destroy(other.gameObject); 
        }
        scoreBar.SetScore(currentPower);
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentPower==minPower+1)
        {
          AudioSource.PlayClipAtPoint(audioClip2,other.transform.position);  
        }
     
    }
}

public class Heal : MonoBehaviour
{

    [SerializeField] AudioClip clickSound;

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("HealUpper"))
        {
            AudioSource.PlayClipAtPoint(clickSound,other.transform.position);
            Destroy(other.gameObject); 
        }
    }
}

public class NextLevel : MonoBehaviour
{
   
   public void OnTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.CompareTag("FinishLevel"))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
   }
    
}

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;


    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }

        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;


    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }

        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}

public class RestartLevel : MonoBehaviour
{
  public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

public class Score : MonoBehaviour
{
    [SerializeField] public AudioClip audioClip;
    public ScoreBar scoreBar;
    int minScore=0;
    int currentScore;

    void Start()
    {
        currentScore=minScore;
        scoreBar.SetScore(minScore);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectibles"))
        {
            AudioSource.PlayClipAtPoint(audioClip,other.transform.position);
            currentScore +=1;
            Destroy(other.gameObject); 
        }
        scoreBar.SetScore(currentScore);
        
        if(other.gameObject.CompareTag("TutorialCollectibles"))
        {
            AudioSource.PlayClipAtPoint(audioClip,other.transform.position);
            currentScore +=1;
            Destroy(other.gameObject); 
           
        }
        scoreBar.SetScore(currentScore);
        
    }

   
}

public class YeniBölüm : MonoBehaviour
{
   
   public void OnTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.CompareTag("FinishLevel"))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
   }
    
}
