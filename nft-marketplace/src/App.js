import React, { useState } from 'react';
import { ethers } from 'ethers';

function App() {
  const { utils } = ethers;
  const [account, setAccount] = useState('');
  const [balance, setBalance] = useState('');

  const connectWallet = async () => 
  {
    if (window.ethereum) 
    {
      try {
        await window.ethereum.request({ method: 'eth_requestAccounts' });
        const provider = new utils.providers.Web3Provider(window.ethereum);
        const signer = provider.getSigner();
        const address = await signer.getAddress();
        setAccount(address);

        const balance = await provider.getBalance(address);
        setBalance(utils.formatEther(balance));
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
