(function () {
    'use strict';

    angular
        .module('app')
        .controller('PlanController', PlanController);

    PlanController.$inject = ['$rootScope', 'FlashService', 'PlanService', 'PlanGroupService'];

    function PlanController($rootScope, FlashService, PlanService, PlanGroupService) {

        var vm = this;

        vm.plans = [];

        vm.planGroups = [];

        vm.getPlanGroups = getPlanGroups;

        vm.getPlans = getPlans;

        vm.savePlans = savePlans;

        vm.addPlan = addPlan;
        vm.removePlan = removePlan;

        function getPlans() {
            PlanService.GetPlans(function (response) {
                vm.plans = response.data.plans;
            });
        }

        function savePlans() {
            PlanService.SavePlans(vm.plans, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addPlan() {

            var plan = { "ID": 0, "PlanName": "", "Priority": 0 };

            vm.plans.push(plan);
        }

        function removePlan(index) {
            vm.plans.splice(index, 1);
        }

        function getPlanGroups() {
            PlanGroupService.GetPlanGroups(function (response) {
                vm.planGroups = response.data.planGroups;
            });
        }

        vm.getPlans();
        vm.getPlanGroups();
    }


})();