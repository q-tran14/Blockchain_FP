import { useState, useEffect } from 'react'
import {ethers} from 'ethers';
import coinImg from "../images/coins1.png"
import Button from 'react-bootstrap/Button';
const Marketplace = ({ marketplace, axICToken, account }) => {
  // const axieID = "#6593121";
  // const axiePrice = "0.0001";
  // // Get axie's Image from CID of the image stored in metadata file
  // const axieImageCid = "";
  // const axieURI = "https://ipfs.io/ipfs/"+ axieImageCid;
    const [loading, setLoading] = useState(true)
    const [items, setItems] = useState([])

    const loadMarketplaceItems = async () => {
    
      const itemCount = await marketplace.itemCount;
      let items = []
      for (let i = 1; i <= itemCount; i++) {
        const item = await marketplace.items(i)
        if ( item.seller == account && !item.sold) {
          // get uri url from nft contract
          const uri =  axICToken.tokenURI(item.tokenId)
          // use uri to fetch the nft metadata stored on ipfs 
          const response =  fetch(uri)
          const metadata =  response.json()
          // get total price of item (item price + fee)
          const totalPrice = marketplace.getTotalPrice(item.itemId)
          // Add item to items array
          items.push({
            totalPrice,
            itemId: item.itemId,
            seller: item.seller,
            name: metadata.name,
            description: metadata.description,
            image: metadata.image
          })
        }
      }
      setLoading(false)
      setItems(items)
    }
    useEffect(() => {loadMarketplaceItems()}, [])
    if (loading) return (
      <main style={{ padding: "1rem 0" }}>
      <h2>Loading...</h2>
    </main>
    )
  return (
      items.map((item) => (
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={item.image} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{item.name}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{item.totalPrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
      ))  
  );
}
export default Marketplace