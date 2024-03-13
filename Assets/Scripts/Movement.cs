using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float sensisivity = 2f;
    public bool lockedCursor = true;
    Rigidbody miRigidBody;
    int monedas = 0;
    int vidas = 3;
    Vector3 posicionInicial;
    public Text Monedas;
    public Text Vidas;
    public int index_escena;


    private void Start()
    {
	int monedas_temp = PlayerPrefs.GetInt("Monedas"); 
        if(monedas_temp>monedas){    
                monedas=monedas_temp;
                Monedas.text = "Monedas: "+monedas;
        }
	
        int vidas_temp = PlayerPrefs.GetInt("Vidas");
        if(vidas_temp!=vidas && vidas_temp!=0){    
                vidas=vidas_temp;
                Vidas.text = "Vidas: "+vidas;
        }

        PlayerPrefs.SetInt("Monedas",0);
        PlayerPrefs.SetInt("Vidas",3);


	Cursor.visible = false;
        if (lockedCursor){
            Cursor.lockState = CursorLockMode.Locked;
        }
	miRigidBody = GetComponent<Rigidbody> ();
	posicionInicial = transform.position;
	//Debug.Log(posicionInicial);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal,0f,vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        float mouseX = Input.GetAxis("Mouse X") * sensisivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensisivity;

        transform.Rotate(Vector3.up * mouseX);
        //transform.Rotate(Vector3.left * mouseY);
        

    }

    void OnTriggerEnter(Collider colision){
	if (colision.CompareTag("Salida")){
		//Debug.Log("Haz encontrado la salida");
		PlayerPrefs.SetInt("Monedas",monedas);
		PlayerPrefs.SetInt("Vidas",vidas);
		//Debug.Log("Cargando");
		SceneManager.LoadSceneAsync(index_escena, LoadSceneMode.Single);
	}else if (colision.CompareTag("Enemigo")){
		miRigidBody.MovePosition(posicionInicial);
		vidas = vidas - 1;
		Vidas.text = "Vidas: "+vidas;
		if (vidas <=0){
		    PlayerPrefs.SetInt("Monedas",0);
                    PlayerPrefs.SetInt("Vidas",3);
	            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);	
		}
	}else if (colision.CompareTag("Moneda")){
		colision.gameObject.SetActive(false);
		monedas = monedas + 1;
		Monedas.text = "Monedas: "+monedas;
		//Debug.Log("Monedas:");
		//Debug.Log(monedas);
	}
    }

}
