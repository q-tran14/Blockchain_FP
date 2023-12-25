import { useState, useEffect } from 'react'
import {ethers} from 'ethers';
import { Row, Col, Card, Button } from 'react-bootstrap'
import coinImg from "../images/coins1.png"
const Marketplace = ({ marketplace, axICToken, account }) => {
  const axieID = "#"+"6593121";
  const axiePrice = "0.0001";
  // Get axie's Image from CID of the image stored in metadata file
  const axieImageCid = "bafkreicir7mmj2bqq267a7swoprm4slxmrbclvcevg66suagifomnvhh3y";
  const axieURI = "https://ipfs.io/ipfs/"+axieImageCid;

  return (
    <div >
      {/* Row: max 6 axie */}
      <div id="axieRow">
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
      </div>
      <div id="axieRow">
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
        <div className="axieItem">
          <div className="axieImg">
            <img id="axieImg" src={axieURI} alt=''/>
          </div>
          <div className="axieInfo">
            <h3 id="axieID">{axieID}</h3>
            <div>
              <img src={coinImg} alt="" id="coinIcon"></img>
              <p id="price">{axiePrice} ETH</p>
            </div>
          </div>
          <Button className="buy-sellBtn" onClick="" variant="outline-light">Buy</Button>
        </div>
      </div>
    </div>
  );
}
export default Marketplace