    (function () {
    'use strict';

    angular
        .module('app')
        .controller('PlanGroupController', PlanGroupController);

    PlanGroupController.$inject = ['$rootScope', 'FlashService', 'PlanGroupService'];

    function PlanGroupController($rootScope, FlashService, PlanGroupService) {

        var vm = this;

        vm.planGroups = [];

        vm.getPlanGroups = getPlanGroups;

        vm.savePlanGroups = savePlanGroups;

        vm.addPlanGroup = addPlanGroup;
        vm.removePlanGroup = removePlanGroup;

        function getPlanGroups() {
            PlanGroupService.GetPlanGroups(function (response) {
                vm.planGroups = response.data.planGroups;
            });
        }

        function savePlanGroups() {
            PlanGroupService.SavePlanGroups(vm.planGroups, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addPlanGroup() {

            var planGroup = { "ID": 0, "PlanGroupName": "", "Priority": 0 };

            vm.planGroups.push(planGroup);
        }

        function removePlanGroup(index) {
            vm.planGroups.splice(index, 1);
        }

        vm.getPlanGroups();
    }


})();