const AxICToken_contract = artifacts.require("./contracts/AxICToken.sol");
const Trade_contract = artifacts.require("./contracts/TradeContract.sol");

module.exports = function(deployer) {
  deployer.deploy(AxICToken_contract,'0xb7277ff78b3e2e7b120D6b5bF8008811e6dfc11a');
};