using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserData : MonoBehaviour {

    #region variable

    #region variable_database

    [SerializeField] InputField inputLoginEmail, inputLoginPassword;
    [SerializeField] InputField inputSignUpName, inputSignUpEmail, inputSignUpPassword;

    private string url_register = "http://testlaravel7.dev.ent.pens.ac.id/public/";
    private string url_login = "http://testlaravel7.dev.ent.pens.ac.id/public/login";
    private string url_update = "http://testlaravel7.dev.ent.pens.ac.id/public/update";
    public string status;
    protected static User myUser;
    WWW www;
    WWWForm form;

    #endregion

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region database
    IEnumerator RegisterUser()
    {
        form = new WWWForm();
        form.AddField("username", inputSignUpName.text);
        form.AddField("email", inputSignUpEmail.text);
        form.AddField("password", inputSignUpPassword.text);

        www = new WWW(url_register, form);
        yield return www;
        status = www.text;
        myUser = JsonUtility.FromJson<User>(status);
        Data._username = myUser.username;
        Data._password = myUser.password;
        Data._email = myUser.email;
        Data._id = myUser.id;
    }

    IEnumerator LoginUser()
    {
        form = new WWWForm();
        form.AddField("email", Data._email);
        form.AddField("password", Data._password);
        www = new WWW(url_login, form);
        yield return www;
        status = www.text;
        myUser = JsonUtility.FromJson<User>(status);
        Data._username = myUser.username;
        Data._password = myUser.password;
        Data._email = myUser.email;
        Data._id = myUser.id;
    }


    public static void playerSave()
    {
        PlayerPrefs.SetString("username", Data._username);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("email", Data._email);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("password", Data._password);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("id", Data._id);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("status", Data._status);
        PlayerPrefs.Save();
    }
    #endregion
}
