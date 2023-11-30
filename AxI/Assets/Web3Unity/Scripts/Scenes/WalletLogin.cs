using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WalletLogin: MonoBehaviour
{
    ProjectConfigScriptableObject projectConfigSO = null;
    public Toggle rememberMe;

    void Start() {
        projectConfigSO = (ProjectConfigScriptableObject)Resources.Load("ProjectConfigData", typeof(ScriptableObject));
        PlayerPrefs.SetString("ProjectID", projectConfigSO.ProjectId);
        PlayerPrefs.SetString("ChainID", projectConfigSO.ChainId);
        PlayerPrefs.SetString("Chain", projectConfigSO.Chain);
        PlayerPrefs.SetString("Network", projectConfigSO.Network);
        PlayerPrefs.SetString("RPC", projectConfigSO.Rpc);
        // if remember me is checked, set the account to the saved account
        if(PlayerPrefs.HasKey("RememberMe") && PlayerPrefs.HasKey("Account"))
        {
            if (PlayerPrefs.GetInt("RememberMe") == 1 && PlayerPrefs.GetString("Account") != "")
            {
                // move to next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
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
        string signature = await Web3Wallet.Sign(message);
        // verify account
        string account = await EVM.Verify(message, signature);
        int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42 && expirationTime >= now) {
            // save account
            PlayerPrefs.SetString("Account", account);
            if (rememberMe.isOn)
                PlayerPrefs.SetInt("RememberMe", 1);
            else
                PlayerPrefs.SetInt("RememberMe", 0);
            print("Account: " + account);
            // load next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
