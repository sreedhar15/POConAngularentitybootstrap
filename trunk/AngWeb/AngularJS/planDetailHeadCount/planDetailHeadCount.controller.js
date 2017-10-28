(function () {
    'use strict';

    angular
        .module('app')
        .controller('PlanDetailHeadCountController', PlanDetailHeadCountController);

    PlanDetailHeadCountController.$inject = ['$rootScope', 'FlashService', 'ProjectService','EmployeeRoleTypeService', 'EmployeeRoleByTypeService', 'PlanDetailService', 'PlanDetailHeadCountService'];

    function PlanDetailHeadCountController($rootScope, FlashService,  ProjectService, EmployeeRoleTypeService, EmployeeRoleByTypeService, PlanDetailService, PlanDetailHeadCountService) {

        var vm = this;

        vm.PlanDetailHeadCounts = [];

        vm.projectSelectionChanged = projectSelectionChanged;
        vm.projectID = 0;
        
        vm.employeeRoleTypeSelectionChanged = employeeRoleTypeSelectionChanged;
        vm.employeeRoleTypeID = 0;

        vm.planDetailSelectionChanged = planDetailSelectionChanged;
        vm.planDetailID = 0;

        vm.getPlanDetails = getPlanDetails;

        vm.getProjects = getProjects;
        vm.getEmployeeRoleTypes = getEmployeeRoleTypes;
        vm.getEmployeeRoleByTypesList = getEmployeeRoleByTypesList;

        vm.getPlanDetailHeadCounts = getPlanDetailHeadCounts;
        vm.savePlanDetailHeadCounts = savePlanDetailHeadCounts;

        vm.addPlanDetailHeadCount = addPlanDetailHeadCount;
        vm.removePlanDetailHeadCount = removePlanDetailHeadCount;

       

        function getPlanDetails(planID) {
            PlanDetailService.GetPlanDetails(planID, function (response) {
                vm.PlanDetails = response.data.PlanDetails;
            });
        }

        function getProjects() {
            ProjectService.GetProjects(function (response) {
                vm.Projects = response.data.projects;
            });
        }

        function getEmployeeRoleTypes() {
            EmployeeRoleTypeService.GetEmployeeRoleTypes(function (response) {
                vm.EmployeeRoleTypes = response.data.EmployeeRoleTypes;
            });
        }

        ///filter by employeeRoleTypeID
        function getEmployeeRoleByTypesList(employeeRoleTypeID) {
            EmployeeRoleByTypeService.GetEmployeeRoleByTypesList(employeeRoleTypeID, function (response) {
                vm.EmployeeRoleByTypesList = response.data.EmployeeRoleByTypesList;
            });
        }

        function getPlanDetailHeadCounts(planDetailID, projectID, employeeRoleTypeID) {
            PlanDetailHeadCountService.GetPlanDetailHeadCounts(planDetailID, projectID, employeeRoleTypeID, function (response) {
                vm.PlanDetailHeadCounts = response.data.PlanDetailHeadCounts;
                
            });
        }

        function savePlanDetailHeadCounts() {
            PlanDetailHeadCountService.SavePlanDetailHeadCounts(vm.PlanDetailHeadCounts, vm.planDetailID, vm.projectID, vm.employeeRoleTypeID, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addPlanDetailHeadCount() {

            // add additional details

            var PlanDetailHeadCount = {

                "ID": 0,
                "PlanDetailID": 1, // Actual plan - 2017
                "ProjectID": 0,
                "EmployeeRoleByTypeID": 0,
                "Month1": 0,
                "Month2": 0,
                "Month3": 0,
                "Month4": 0,
                "Month5": 0,
                "Month6": 0,
                "Month7": 0,
                "Month8": 0,
                "Month9": 0,
                "Month10": 0,
                "Month11": 0,
                "Month12": 0
            };

            vm.PlanDetailHeadCounts.push(PlanDetailHeadCount);
        }

        function removePlanDetailHeadCount(index) {
            vm.PlanDetailHeadCounts.splice(index, 1);
        }

        function employeeRoleTypeSelectionChanged(employeeRoleTypeID) {
            vm.employeeRoleTypeID = employeeRoleTypeID;
            vm.getEmployeeRoleByTypesList(employeeRoleTypeID);
            vm.getPlanDetailHeadCounts(vm.planDetailID, vm.projectID, vm.employeeRoleTypeID);
        }

        function projectSelectionChanged(projectID) {
            vm.projectID = projectID;
            vm.getPlanDetailHeadCounts(vm.planDetailID, vm.projectID, vm.employeeRoleTypeID);
        }


        function planDetailSelectionChanged(planDetailID) {
            vm.planDetailID = planDetailID.currentTarget.value;
            vm.getPlanDetailHeadCounts(vm.planDetailID, vm.projectID, vm.employeeRoleTypeID);
        }

        vm.getPlanDetails(1); // default plan ID is 1
        vm.getEmployeeRoleTypes();
        vm.getProjects();
        vm.getEmployeeRoleByTypesList(vm.employeeRoleTypeID);
    }


})();