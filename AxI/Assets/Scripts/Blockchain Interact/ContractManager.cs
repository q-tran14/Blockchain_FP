using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public List<Contract> contracts;
    public static ContractManager CInstance { get; private set; }
    private void Awake()
    {
        if (CInstance != null && CInstance != this) Destroy(this);
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            CInstance = this;
            DontDestroyOnLoad(this);
        }
    }

    async public void SendTransaction()
    {

        //string data = await EVM.CreateContractData(abi, method, args);
        //string response = await Web3Wallet.SendTransaction(PlayerPrefs.GetString("ChainID"), contract, "0", data, "", "");
    }
}

public class Contract{
    public string name;
    public string address;
    public string abi;
}
