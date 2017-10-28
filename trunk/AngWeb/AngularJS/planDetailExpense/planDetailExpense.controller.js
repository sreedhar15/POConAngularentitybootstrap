(function () {
    'use strict';

    
    angular
        .module('app')
        .controller('PlanDetailExpenseController', PlanDetailExpenseController);


    PlanDetailExpenseController.$inject = ['$rootScope', 'FlashService', 'ProjectService', 'ExpenseTypeService', 'ExpenseByTypeService', 'PlanDetailService', 'PlanDetailExpenseService'];

    
    function PlanDetailExpenseController($rootScope, FlashService, ProjectService, ExpenseTypeService, ExpenseByTypeService, PlanDetailService, PlanDetailExpenseService) {

         var vm = this;


 
        vm.PlanDetailExpenses = [];

     
 
        vm.projectSelectionChanged = projectSelectionChanged;
        vm.projectID = 0;

        vm.expenseTypeSelectionChanged = expenseTypeSelectionChanged;
        vm.expenseTypeID = 0;

        vm.planDetailSelectionChanged = planDetailSelectionChanged;
        vm.planDetailID = 0;

        vm.getPlanDetails = getPlanDetails;

        vm.getProjects = getProjects;
        vm.getExpenseTypes = getExpenseTypes;
        vm.getExpenseByTypesList = getExpenseByTypesList;

        vm.getPlanDetailExpenses = getPlanDetailExpenses;
        vm.savePlanDetailExpenses = savePlanDetailExpenses;

        vm.addPlanDetailExpense = addPlanDetailExpense;
        vm.removePlanDetailExpense = removePlanDetailExpense;




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

        function getExpenseTypes() {
            ExpenseTypeService.GetExpenseTypes(function (response) {
                vm.ExpenseTypes = response.data.ExpenseTypes;
            });
        }

        ///filter by ExpenseTypeID
        function getExpenseByTypesList(expenseTypeID) {
            ExpenseByTypeService.GetExpenseByTypesList(expenseTypeID, function (response) {
                vm.ExpenseByTypesList = response.data.ExpenseByTypesList;
            });
        }

        function getPlanDetailExpenses(planDetailID, projectID, expenseTypeID) {
            PlanDetailExpenseService.GetPlanDetailExpenses(planDetailID, projectID, expenseTypeID, function (response) {
                vm.PlanDetailExpenses = response.data.PlanDetailExpenses;

            });
        }

        function savePlanDetailExpenses() {
            
            PlanDetailExpenseService.SavePlanDetailExpenses(vm.PlanDetailExpenses, vm.planDetailID, vm.projectID, vm.expenseTypeID, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addPlanDetailExpense() {

            // add additional details

            var PlanDetailExpense = {

                "ID": 0,
                "PlanDetailID": 1, // Actual plan - 2017
                "ProjectID": 0,
                "ExpenseByTypeID": 0,
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

            vm.PlanDetailExpenses.push(PlanDetailExpense);
        }

        function removePlanDetailExpense(index) {
            vm.PlanDetailExpenses.splice(index, 1);
        }

        function expenseTypeSelectionChanged(expenseTypeID) {

            vm.expenseTypeID = expenseTypeID;
            vm.getExpenseByTypesList(expenseTypeID);
            vm.getPlanDetailExpenses(vm.planDetailID, vm.projectID, vm.expenseTypeID);
        }

        function projectSelectionChanged(projectID) {
            vm.projectID = projectID;
            vm.getPlanDetailExpenses(vm.planDetailID, vm.projectID, vm.expenseTypeID);
        }


        function planDetailSelectionChanged(planDetailID) {
            vm.planDetailID = planDetailID.currentTarget.value;
            vm.getPlanDetailExpenses(vm.planDetailID, vm.projectID, vm.expenseTypeID);
        }

        vm.getPlanDetails(1); // default plan ID is 1
        vm.getExpenseTypes();
        vm.getProjects();
        vm.getExpenseByTypesList(vm.expenseTypeID);
    }



})();