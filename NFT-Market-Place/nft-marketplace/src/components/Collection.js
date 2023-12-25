import { useState, useEffect } from 'react'
import {ethers} from 'ethers';
import { Row, Col, Card, Button } from 'react-bootstrap'

const {utils} = 'ethers';

const Collection = ({ marketplace, axICToken, account }) => {
  const [loading, setLoading] = useState(true)
  const [axies, setAxies] = useState([])
  const loadUserCollection = async () => {
    // Get all token account have  
    const tokens = await axICToken.getAllToken(account);
    console.log(tokens);
    let axies = [];
    tokens.forEach(async (a) => {
      // get uri url from nft contract
      const uri = await axICToken.tokenURI(a);
      // use uri to fetch the nft metadata stored on ipfs 
      const response = await fetch(uri);
      const metadata = await response.json();
      console.log(metadata.image);
      let imgURI = "https://ipfs.io/ipfs/" + metadata.image.slice(7);
      //Define item
      const axie = {
        tokenId: a,
        axieId: metadata.name,
        image: imgURI
      }

      console.log(axie);
      // Add item to items array
      axies.push(axie);
    });
    
    setLoading(false);
    setAxies(axies);
  }
  useEffect(() => {loadUserCollection()},[]);
  if (loading) return (
    <main style={{ padding: "1rem 0" }}>
      <h2>Loading...</h2>
    </main>
  )
  return (
    <div className="flex justify-center">
      {axies.length > 0 ?
        axies.map((axie) => (
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axie.image} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axie.name}</h3>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Sell</Button>
        </div>
      )) : (
        <main style={{ padding: "1rem 0" }}>
                <h2>No listed assets</h2>
              </main>
      )}
    </div>
    );
}
export default Collection