using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private void Start()
    {
        Cursor.visible = false;
        if (lockedCursor){
            Cursor.lockState = CursorLockMode.Locked;
        }
	miRigidBody = GetComponent<Rigidbody> ();
	posicionInicial = transform.position;
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
		Debug.Log("Haz encontrado la salida");
	}else if (colision.CompareTag("Enemigo")){
		miRigidBody.MovePosition (posicionInicial);
		vidas = vidas - 1;
		Vidas.text = "Vidas: "+vidas;
	}else if (colision.CompareTag("Moneda")){
		colision.gameObject.SetActive(false);
		monedas = monedas + 1;
		Monedas.text = "Monedas: "+monedas;
		//Debug.Log("Monedas:");
		//Debug.Log(monedas);
	}
    }
}
