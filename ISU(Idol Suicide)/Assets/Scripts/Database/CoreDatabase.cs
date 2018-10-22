using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CoreDatabase : MonoBehaviour {

    [SerializeField] GameObject LoginMenu, SignUpMenu;
    [SerializeField] InputField inputSignUpName, inputSignUpEmail, inputSignUpPassword;
    [SerializeField] InputField inputLoginName, inputLoginPassword;

    private string url_register = "http://api5.dev.ent.pens.ac.id/public/api/user/register";
    private string url_login = "http://api5.dev.ent.pens.ac.id/public/api/user/login";
    private string url_postCoin = "http://api5.dev.ent.pens.ac.id/public/api/coin/add";
    private string url_getCoin = "http://api5.dev.ent.pens.ac.id/public/api/coin/get/";
    public string status;
    protected static User myUser;
    WWW www;
    WWWForm form;

    // Use this for initialization
    void Start () {
        Data._status = PlayerPrefs.GetInt("status");
        Data._username = PlayerPrefs.GetString("username");
        Data._password = PlayerPrefs.GetString("password");
        Data._id = PlayerPrefs.GetInt("id");
        if (Data._status == 1)
        {
            StartCoroutine(LoginUser());
            InitHome();
        }
        else
        {
            InitLogin();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator RegisterUser()
    {
        form = new WWWForm();
        form.AddField("username", inputSignUpName.text);
        form.AddField("email", inputSignUpEmail.text);
        form.AddField("password", inputSignUpPassword.text);


        www = new WWW(url_register, form);
        yield return www;
        Debug.Log(www);

        status = www.text;
        Debug.Log(status);

        User myUser = JsonUtility.FromJson<User>(status);
        Data._username = myUser.username;
        Data._password = myUser.password;
        Data._email = myUser.email;
        Data._id = myUser.id;
        Debug.Log(myUser.username);
    }

    IEnumerator LoginUser()
    {
        form = new WWWForm();
        form.AddField("username", inputLoginName.text);
        form.AddField("password", inputLoginPassword.text);
        
        www = new WWW(url_login, form);
        yield return www;
        Debug.Log(www);

        status = www.text;
        Debug.Log(status);

        User myUser = JsonUtility.FromJson<User>(status);
        Data._username = myUser.username;
        Data._password = myUser.password;
        Data._email = myUser.email;
        Data._id = myUser.id;
        Debug.Log(myUser.username);
    }

    IEnumerator postCoin()
    {
        form = new WWWForm();
        form.AddField("id_user", Data._id);
        form.AddField("coin", 200);
        www = new WWW(url_postCoin, form);
        yield return www;
    }

    IEnumerator getCoin()
    {
        UnityWebRequest www = UnityWebRequest.Get(url_getCoin + Data._id);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
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

    public void onPressSubmitSignUpButton()
    {
        StartCoroutine(RegisterUser());
        InitLogin();
        /*playerSave();
        PlayerPrefs.SetInt("status", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("username", inputSignUpName.text);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("password", inputSignUpPassword.text);
        PlayerPrefs.Save();*/
    }

    public void onPressLoginButton()
    {
        StartCoroutine(LoginUser());
        if(inputLoginName.text == Data._username && inputLoginPassword.text == Data._password)
        {
            playerSave();
            PlayerPrefs.SetInt("status", 1);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("username", inputLoginName.text);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("password", inputLoginPassword.text);
            PlayerPrefs.Save();
            Debug.Log(Data._username);

            Data._status = PlayerPrefs.GetInt("status");
            if (Data._status == 1)
            {
                InitHome();
            }
        }
    }

    public void onPressPostCoin()
    {
        StartCoroutine(postCoin());
    }

    public void onPressGetCoin()
    {
        StartCoroutine(getCoin());
    }

    public void InitLogin()
    {
        LoginMenu.SetActive(true);
        SignUpMenu.SetActive(false);
    }

    public void InitSignUp()
    {
        SignUpMenu.SetActive(true);
        LoginMenu.SetActive(false);
    }

    public void InitHome()
    {
        SignUpMenu.SetActive(false);
        LoginMenu.SetActive(false);
    }

    
}
