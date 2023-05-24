using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthController : MonoBehaviour
{
    public bool havePlayed = false;
    public List<string> strings = new List<string>();
    public bool canRecord = true;

    const string CONTRASENA = "GameManager-GameManager-GameManager-GameManager-GameManager-GameManager-GameManager-GameManager";

    private void Update()
    {
        if (strings.Count == 8 && canRecord)
        {
            canRecord = false;
            string passwordToCompare = string.Join("-", strings);
            VerifyPassword(passwordToCompare);
        }
    }

    private void VerifyPassword(string password)
    {
        if(CONTRASENA.Equals(password))
        {
            StartCoroutine(CorrectPassword());
        }
        else
        {
            StartCoroutine(WrongPassword());
        }
    }

    private IEnumerator CorrectPassword()
    {
        Light globalLight = GameObject.Find("Directional Light").GetComponent<Light>();
        Debug.Log(globalLight.ToString());
        if (globalLight == null) {
            yield break;
        };

        globalLight.color= Color.green;

        // Esperar 3 segundos
        yield return new WaitForSeconds(3);

        globalLight.color = Color.white;

    }

    private IEnumerator WrongPassword()
    {
        Light globalLight = GameObject.Find("Directional Light").GetComponent<Light>();
        if (globalLight == null)
        {
            yield break;
        };

        globalLight.color = Color.red;

        // Esperar 3 segundos
        yield return new WaitForSeconds(3);

        globalLight.color = Color.white;

    }

}
