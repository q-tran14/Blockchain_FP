import { useState, useEffect } from 'react'
import {ethers} from 'ethers';
import { Row, Col, Card, Button } from 'react-bootstrap'

const Collection = ({ marketplace, axICToken, account }) => {
  const [loading, setLoading] = useState(true)
  const [items, setItems] = useState([])
  const loadUserCollection = async () => {
    
    const axies = await axICToken.getAllToken(account);
    axies.forEach(async (a) => {
        // get uri url from nft contract
        const uri = await axICToken.tokenURI(a);
        // use uri to fetch the nft metadata stored on ipfs 
        const response = await fetch(uri);
        const metadata = await response.json();
        console.log(metadata);
    });
    
    setLoading(false)
    setItems(items)
  }

  return ("");
}
export default Collection