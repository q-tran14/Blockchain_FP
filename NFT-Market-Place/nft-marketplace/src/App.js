import React, { useState } from 'react';
import { ethers } from "ethers";
import {BrowserRouter, Routes, Route} from 'react-router-dom';
import Navigation from './NavigationBar.js';
import Marketplace from './Marketplace.js';
import Collection from './Collection.js';
import HomePage from './HomePage.js';
import { Spinner } from 'react-bootstrap';
import marketplaceAbi from '../src/SmartContract/build/contracts/Marketplace.json';
import axICTokenAbi from '../src/SmartContract/build/contracts/AxICToken.json';

function App() {
  const {utils} = ethers;
  const [account, setAccount] = useState(null);
  const [loading, setLoading] = useState(null);
  const [balance, setBalance] = useState('');
  const [axICToken, setAxICToken] = useState({});
  const [marketplace, setMarketplace] = useState({});

  const marketplaceAddress = "0xa4De5C749A7dC0Bc2EE0C9C71Cc469CA9FAEd4eF";
  const axICTokentAddress = "0xA0d5aCae47Cf9991062cBc5820d578ae5d52CD6C";

  const loadContracts = async (signer) => {
    const marketplace = new utils.Contract(marketplaceAddress,marketplaceAbi.abi,signer);
    setMarketplace(marketplace);
    const axICToken = new utils.Contract(axICTokentAddress.address, axICTokenAbi.abi, signer);
    setAxICToken(axICToken);
    setLoading(false);
  };

  const web3Handler = async () => {
    const accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
    setAccount(accounts[0])
    // Get provider from Metamask
    const provider = new utils.providers.Web3Provider(window.ethereum)
    // Set signer
    const signer = provider.getSigner()
    loadContracts(signer)
  }

  
  return (
      <BrowserRouter>
        <div>
          <Navigation web3Handler={web3Handler} account={account}/>
          {loading ? (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '80vh' }}>
              <Spinner animation="border" style={{ display: 'flex' }} />
              <p className='mx-3 my-0'>Awaiting Metamask Connection...</p>
            </div>
          ) : (
            <Routes>
            <Route path = "/HomePage" element = {              
              <HomePage/>
              
            }/>
            <Route path = "/Marketplace" element = {              
              <Marketplace/>
            
            }/>
            <Route path = "/Collection" element = {
              <Collection/>

            }/>
          </Routes>
          )}
         
        </div>
      </BrowserRouter>
  );
}

export default App;