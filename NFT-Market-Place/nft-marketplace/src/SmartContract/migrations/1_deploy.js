var AxI = artifacts.require("AxICToken");
var Marketplace = artifacts.require("Marketplace");

module.exports = function(deployer) {
  // deployment steps
  deployer.deploy(AxI,"0xb7277ff78b3e2e7b120D6b5bF8008811e6dfc11a").then(function() {
    return deployer.deploy(Marketplace,AxI.address,25);
  });
};