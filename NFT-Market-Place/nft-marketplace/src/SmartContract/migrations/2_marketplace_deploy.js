var Marketplace = artifacts.require("Marketplace");

module.exports = function(deployer) {
  // deployment steps
  deployer.deploy(Marketplace,"0xA0d5aCae47Cf9991062cBc5820d578ae5d52CD6C",10);
};