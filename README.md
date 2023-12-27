# How to run the project
## Game
### *For Using Unity Editor*
- Install [Unity Hub](https://unity.com/download)
- Install [Unity Editor 2022.3.11f1](https://unity.com/releases/editor/whats-new/2022.3.11#release-notes)

### *For play in Build version*
- Double click on "Game Jam.exe" in Build folder in "Blockchain_FP\AxI\Build" and enjoy the game.

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

- For deploy AxICToken smart contract -> Marketplace smart contract (During this process, if the network is weak, the deployment process will have a "Polling Block Tracker" error or a "Timeout" error)
> truffle migrate --network sepolia --reset

## Web
- Run terminal or git bash, move to nft-marketplace folder
- Install every package
> npm install

- Run web project
> npm start
