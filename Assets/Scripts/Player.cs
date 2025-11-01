using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int vida = 3;
    public int quantidadeItens;
    public Vector3 posicoes;

    private float moveSpeed = 4.5f;
    public float dmgCooldown = 1f;
    private bool canTakeDmg = true;
    public bool steppingOnAcid;
    public GameObject updateTxt;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(Vector2.up * Time.deltaTime * moveSpeed); }
        if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(Vector2.down * Time.deltaTime * moveSpeed); }
        if (Input.GetKey(KeyCode.LeftArrow)) { transform.Translate(Vector2.left * Time.deltaTime * moveSpeed); }
        if (Input.GetKey(KeyCode.RightArrow)) { transform.Translate(Vector2.right * Time.deltaTime * moveSpeed); }

        UpdatePositions();

        if(canTakeDmg == false) 
        {
            dmgCooldown -= Time.deltaTime;
            if(dmgCooldown < 0)
            {
                canTakeDmg = true;
                dmgCooldown = 1;
            }
        }

        if(vida <= 0)
        {
            SceneManager.LoadScene(0);
        }

        if(!steppingOnAcid)
        {
            return;
        }
        else
        {
            GroundDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            quantidadeItens++;
            updateTxt.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Fonte de dano"))
        {
            steppingOnAcid = true;
            updateTxt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fonte de dano") && canTakeDmg)
        {
            steppingOnAcid = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fonte de dano") && canTakeDmg)
        {
            vida--;
            canTakeDmg = false;
            updateTxt.SetActive(true);
        }
    }


    void GroundDamage()
    {
        dmgCooldown -= Time.deltaTime;
        if (dmgCooldown < 0)
        {
            vida--;
            dmgCooldown = 1;
        }
    }


    void UpdatePositions()
    {
        posicoes.x = gameObject.transform.position.x;
        posicoes.y = gameObject.transform.position.y;
        posicoes.z = gameObject.transform.position.z;
    }
}
