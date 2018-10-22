using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singkron : MonoBehaviour {

    private string url_update = "http://api5.dev.ent.pens.ac.id/public/update";
    WWW www;
    WWWForm form;

    public void Sink()
    {
        StartCoroutine(Up());
        //CoreMarker.saveScore();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Up()
    {

        form = new WWWForm();
        form.AddField("username", Data._username);
        form.AddField("password", Data._password);
        //form.AddField("score", Touch3D.Point);

        www = new WWW(url_update, form);
        yield return www;

    }
}
