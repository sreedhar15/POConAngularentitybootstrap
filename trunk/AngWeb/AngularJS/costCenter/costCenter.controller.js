(function () {
    'use strict';

    angular
        .module('app')
        .controller('CostCenterController', CostCenterController);

    CostCenterController.$inject = ['$rootScope', 'FlashService', 'CostCenterService'];

    function CostCenterController($rootScope, FlashService, CostCenterService) {

        var vm = this;

        vm.CostCenters = [];

        vm.getCostCenters = getCostCenters;

        vm.saveCostCenters = saveCostCenters;

        vm.addCostCenter = addCostCenter;
        vm.removeCostCenter = removeCostCenter;

        function getCostCenters() {
            CostCenterService.GetCostCenters(function (response) {
                vm.CostCenters = response.data.CostCenters;
            });
        }

        function saveCostCenters() {
            CostCenterService.SaveCostCenters(vm.CostCenters, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addCostCenter() {

            var CostCenter = { "ID": 0, "Name": "" };

            vm.CostCenters.push(CostCenter);
        }

        function removeCostCenter(index) {
            vm.CostCenters.splice(index, 1);
        }

        vm.getCostCenters();
    }


})();