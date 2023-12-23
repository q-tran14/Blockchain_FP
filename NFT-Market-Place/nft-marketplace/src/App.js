import React, { useState } from 'react';
import { ethers } from "ethers";

function App() {
  //const ethers = require('ethers');
  const [account, setAccount] = useState(null);
  const [balance, setBalance] = useState('');

  const connectWallet = async () => 
  {
    if (window.ethereum != null) 
    {
      try {
        await window.ethereum.request({ method: 'eth_requestAccounts', });
        const provider = new ethers.BrowserProvider(window.ethereum);
        const signer = await provider.getSigner();
        const address = await signer.address;
        console.log(provider.listAccounts());
        console.log(address);
        setAccount(address);

        const balance = await provider.getBalance(address);
        setBalance(ethers.formatEther(balance));
      } catch (error) {
        console.error('Connect wallet error:', error);
      }
    } else {
      console.error('MetaMask is not installed!');
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Ethers React App</h1>
        {account ? (
          <div>
            <p>Connected Account: {account}</p>
            <p>Balance: {balance} ETH</p>
          </div>
        ) : (
          <button onClick={connectWallet}>Connect Wallet</button>
        )}
      </header>
    </div>
  );
}

export default App;
