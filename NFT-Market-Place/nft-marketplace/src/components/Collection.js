import { useState, useEffect } from 'react'
import {ethers} from 'ethers';
import { Row, Col, Card, Button } from 'react-bootstrap'

const {utils} = 'ethers';

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
        const totalPrice = await marketplace.getTotalPrice(a);
        //Define item
        const item = {
          totalPrice,
          itemId: a,
          seller: account,
          name: metadata.name,
          description: metadata.description,
          image: metadata.image
        }
        // Add item to items array
        items.push(item);
    });
    
    setLoading(false)
    setItems(items)

    if (loading) return (
        <div className="flex justify-center">
          {items.length > 0 ?
            <div className="px-5 py-3 container">
                <h2>Listed</h2>
              <Row xs={1} md={2} lg={4} className="g-4 py-3">
                {items.map((item, idx) => (
                  <Col key={idx} className="overflow-hidden">
                    <Card>
                      <Card.Img variant="top" src={item.image} />
                      <Card.Footer>{utils.formatEther(item.totalPrice)} ETH</Card.Footer>
                    </Card>
                  </Col>
                ))}
              </Row>
            </div>
            : (
              <main style={{ padding: "1rem 0" }}>
                <h2>No listed assets</h2>
              </main>
            )}
        </div>
    )
  }

  return ("");
}
export default Collection