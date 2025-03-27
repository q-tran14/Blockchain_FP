# Setup before play game
- [Add Metamask into Chrome](https://chromewebstore.google.com/detail/nkbihfbeogaeaoehlefnkodbefgpgknn?utm_source=item-share-cb)
- Create account
- Add Sepolia testnet to Metamask: [Guide](https://developers.moralis.com/how-to-add-the-sepolia-network-to-metamask-full-guide/)
- Use faucet to earn coin (use for transaction):
  - [Superchain Dev Console | Faucet](https://console.optimism.io/faucet)
  - [Google Cloud Web3 - Ethereum Sepolia Faucet](https://cloud.google.com/application/web3/faucet/ethereum/sepolia)
- Have fun!
<!--## Game
### *For Using Unity Editor*
- Install [Unity Hub](https://unity.com/download)
- Install [Unity Editor 2022.3.11f1](https://unity.com/releases/editor/whats-new/2022.3.11#release-notes)
- Steps:
  - Step 1: Click "Open" in Unity Hub
  ![Imgur](https://i.imgur.com/LW0tJPH.png)
  - Step 2: Choose AxI folder
  ![Imgur](https://i.imgur.com/2QD5mHy.png)
  - Step 3,4: Move to Wallet Login scene and click play button
  ![Imgur](https://i.imgur.com/jHfWbYO.png)

### *For play in Build version*
- Download Build.zip in Releases and upzip it.
- Find and double click on "AxI.exe" in Build folder enjoy the game.

$\color{red}{\textbf{Notice}}$
- There are a number of processes in the game, you should wait for them to finish loading:
  - Login in the first scene.
  - Minting axie when receiving the gift in the second scene.
  - And when you return the second scene after you finish the battle from third scene. In this process, you should wait until the axie in scene play the animation. ( $\color{red}{\textbf{MUST WAIT}}$ )

## Smart Contract
- Run terminal or git bash, move to "Blockchain-FP\NFT-Market-Place\nft-marketplace\src\SmartContract"
- Install truffle
> npm intstall -g truffle

- For compile smart contract
> truffle compile

- For deploy AxICToken smart contract -> Marketplace smart contract (During this process, if the network is weak, the deployment process will have a "Polling Block Tracker" error or a "Timeout" error, just run again)
> truffle migrate --network sepolia --reset

## Web
- Run terminal or git bash, move to nft-marketplace folder
- Install every package
> npm install

- Run web project
> npm start -->
