(function () {
    'use strict';

    angular
        .module('app')
        .controller('FranchiseController', FranchiseController);

    FranchiseController.$inject = ['$rootScope', 'FlashService', 'FranchiseService'];

    function FranchiseController($rootScope, FlashService, FranchiseService) {

        var vm = this;

        vm.Franchises = [];

        vm.getFranchises = getFranchises;

        vm.saveFranchises = saveFranchises;

        vm.addFranchise = addFranchise;
        vm.removeFranchise = removeFranchise;

        function getFranchises() {
            FranchiseService.GetFranchises(function (response) {
                vm.Franchises = response.data.Franchises;
            });
            
        }

        function saveFranchises() {
            FranchiseService.SaveFranchises(vm.Franchises, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
            
        }

        function addFranchise() {

            var Franchise = { "ID": 0, "Name": "" };

            vm.Franchises.push(Franchise);
        }

        function removeFranchise(index) {
            vm.Franchises.splice(index, 1);
        }

        vm.getFranchises();
    }


})();