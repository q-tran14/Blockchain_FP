using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class WalletLogin: MonoBehaviour
{
    ProjectConfigScriptableObject projectConfigSO = null;
    public Toggle rememberMe;
    public string account;
    public GameObject panel;
    public GameObject loginBtn;
    public GameObject toggleBox;
    public GameObject loading;
    public GameObject loginError;
    public GameObject DBManager;
    void Start() {
        projectConfigSO = (ProjectConfigScriptableObject)Resources.Load("ProjectConfigData", typeof(ScriptableObject));
        PlayerPrefs.SetString("ProjectID", projectConfigSO.ProjectId);
        PlayerPrefs.SetString("ChainID", projectConfigSO.ChainId);
        PlayerPrefs.SetString("Chain", projectConfigSO.Chain);
        PlayerPrefs.SetString("Network", projectConfigSO.Network);
        PlayerPrefs.SetString("RPC", projectConfigSO.Rpc);

        
        // if remember me is checked, set the account to the saved account
        #if !UNITY_EDITOR
        if(PlayerPrefs.HasKey("RememberMe") && PlayerPrefs.HasKey("Account"))
        {
            if (PlayerPrefs.GetInt("RememberMe") == 1 && PlayerPrefs.GetString("Account") != "")
            {
                // move to next scene
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                LoadHome();
            }
        }
        #endif
    }

    async public void OnLogin()
    {
        // get current timestamp
        int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // set expiration time
        int expirationTime = timestamp + 60;
        // set message
        string message = expirationTime.ToString();
        // sign message
        string signature = "";
        try
        {
            signature = await Web3Wallet.Sign(message);
            // verify account
            account = await EVM.Verify(message, signature);
            int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
            // validate
            if (account.Length == 42 && expirationTime >= now)
            {
                // save account
                PlayerPrefs.SetString("Account", account);
                if (rememberMe.isOn) PlayerPrefs.SetInt("RememberMe", 1);
                else PlayerPrefs.SetInt("RememberMe", 0);
                DBManager.SetActive(true);
                print("Account: " + account);
                
                LoadHome();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            loginError.SetActive(true);
            await Task.Delay(3000);
            loginError.SetActive(false);
        }
    }

    public async void LoadHome()
    {
        // load next scene
        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        panel.SetActive(false);
        loginBtn.SetActive(false);
        toggleBox.SetActive(false);
        loading.SetActive(true);
        await Task.Delay(3000);
        scene.allowSceneActivation = true;

    }
}
