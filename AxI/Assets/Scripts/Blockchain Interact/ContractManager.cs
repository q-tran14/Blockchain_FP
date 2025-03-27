using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ContractManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public Contract contract;
    public static ContractManager CInstance { get; private set; }
    private void Awake()
    {
        if (CInstance != null && CInstance != this) {
            DestroyImmediate(gameObject);
        }
        else
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            CInstance = this;
            DontDestroyOnLoad(this);
        }
    }

    async public Task<string> SendTransaction(string method, string[] args) // Mint
    {
        string obj = JsonConvert.SerializeObject(args);
        string data = await EVM.CreateContractData(contract.abi, method, obj);
        string response = "";
        try
        {
            response = await Web3Wallet.SendTransaction(PlayerPrefs.GetString("ChainID"), contract.address, "0", data, "", "");
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        return response;
    }

    async public void ReadValueFromSmartContract(string method, string[] args)  // Get all token
    {
        string obj = JsonConvert.SerializeObject(args);
        string response = await EVM.Call(PlayerPrefs.GetString("ChainID"), PlayerPrefs.GetString("Network"), contract.address, contract.abi, method, obj);
    }

    async public Task<bool> GetUserAxieFromSmartContract(string[] args)
    {
        string obj = JsonConvert.SerializeObject(args);
        string response = await EVM.Call(PlayerPrefs.GetString("ChainID"), PlayerPrefs.GetString("Network"), contract.address, contract.abi, "getAllToken", obj);
        Debug.Log(response);
        if (!response.Equals("[]")) // Load Axie ID to Player.AxieIDs - Old player
        {
            List<string> tokenIDs = JsonConvert.DeserializeObject<List<string>>(response);

            foreach (string token in tokenIDs)
            {
                string res = await EVM.Call(PlayerPrefs.GetString("ChainID"), PlayerPrefs.GetString("Network"), contract.address, contract.abi, "tokenURI", $"[\"{token}\"]");
                Debug.Log(res);
                UnityWebRequest wr = new UnityWebRequest();
                do
                {
                    wr = UnityWebRequest.Get(res);
                    wr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                    wr.timeout = 10;
                    await wr.SendWebRequest();
                    Debug.Log("Web request Download Handler data is null? " + wr.downloadHandler.data.IsUnityNull());
                } while (wr.downloadHandler.data.IsUnityNull() == true);
                
                Metadata metadata = JsonUtility.FromJson<Metadata>(System.Text.Encoding.UTF8.GetString(wr.downloadHandler.data));
                Debug.Log("Add axie in ContractManager");
                player.addAxie(metadata.name);
            }

            Debug.Log("Finish");
            if (player.getListAxies().Count == tokenIDs.Count) return true;
            else return false;
        }
        return true;
    }
}

[System.Serializable]
public class Contract{
    public string name = "AxICToken";
    public string address = "0x759Cbb0C5544bf437cbc6d2916de5A7D3136089a";
    public string abi = "[ { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"initialOwner\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"ERC721EnumerableForbiddenBatchMint\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" }, { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" } ], \"name\": \"ERC721IncorrectOwner\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"ERC721InsufficientApproval\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"approver\", \"type\": \"address\" } ], \"name\": \"ERC721InvalidApprover\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"ERC721InvalidOperator\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" } ], \"name\": \"ERC721InvalidOwner\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"receiver\", \"type\": \"address\" } ], \"name\": \"ERC721InvalidReceiver\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"ERC721InvalidSender\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"ERC721NonexistentToken\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"index\", \"type\": \"uint256\" } ], \"name\": \"ERC721OutOfBoundsIndex\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"EnforcedPause\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ExpectedPause\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" } ], \"name\": \"OwnableInvalidOwner\", \"type\": \"error\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"OwnableUnauthorizedAccount\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"approved\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"Approval\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"ApprovalForAll\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_fromTokenId\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_toTokenId\", \"type\": \"uint256\" } ], \"name\": \"BatchMetadataUpdate\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_tokenId\", \"type\": \"uint256\" } ], \"name\": \"MetadataUpdate\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"previousOwner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"newOwner\", \"type\": \"address\" } ], \"name\": \"OwnershipTransferred\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"Paused\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"Transfer\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"Unpaused\", \"type\": \"event\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"approve\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"getApproved\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"isApprovedForAll\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"owner\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"ownerOf\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"paused\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"renounceOwnership\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"setApprovalForAll\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"index\", \"type\": \"uint256\" } ], \"name\": \"tokenByIndex\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"index\", \"type\": \"uint256\" } ], \"name\": \"tokenOfOwnerByIndex\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"transferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"newOwner\", \"type\": \"address\" } ], \"name\": \"transferOwnership\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"uri\", \"type\": \"string\" } ], \"name\": \"safeMint\", \"outputs\": [], \"stateMutability\": \"payable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" } ], \"name\": \"getAllToken\", \"outputs\": [ { \"internalType\": \"uint256[]\", \"name\": \"\", \"type\": \"uint256[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"pause\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"unpause\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" } ], \"name\": \"tokenURI\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes4\", \"name\": \"interfaceId\", \"type\": \"bytes4\" } ], \"name\": \"supportsInterface\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
}
