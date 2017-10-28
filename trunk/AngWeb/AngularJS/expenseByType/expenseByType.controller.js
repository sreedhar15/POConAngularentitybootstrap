(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExpenseByTypeController', ExpenseByTypeController);

    ExpenseByTypeController.$inject = ['$rootScope', 'FlashService', 'ExpenseTypeService','ExpenseService','ExpenseByTypeService'];

    function ExpenseByTypeController($rootScope, FlashService, ExpenseTypeService, ExpenseService, ExpenseByTypeService) {

        var vm = this;

        vm.ExpenseByTypes = [];

        vm.expenseTypeSelectionChanged = expenseTypeSelectionChanged;
        vm.expenseTypeID = 0;

        vm.getExpenseTypes = getExpenseTypes;
        vm.getExpenses = getExpenses;

        vm.getExpenseByTypes = getExpenseByTypes;
        vm.getExpenseByTypesByTypeID = getExpenseByTypesByTypeID;
        
        vm.saveExpenseByTypes = saveExpenseByTypes;
        vm.addExpenseByType = addExpenseByType;
        vm.removeExpenseByType = removeExpenseByType;

        function getExpenseTypes() {
            ExpenseTypeService.GetExpenseTypes(function (response) {
                vm.ExpenseTypes = response.data.ExpenseTypes;
            });
        }

        function getExpenses() {
            ExpenseService.GetExpenses(function (response) {
                vm.Expenses = response.data.Expenses;
            });
        }


        function getExpenseByTypes() {
            ExpenseByTypeService.GetExpenseByTypes(function (response) {
                vm.ExpenseByTypes = response.data.ExpenseByTypes;
            });
        }

        ///filter by expenseTypeID
        function getExpenseByTypesByTypeID(expenseTypeID) {
            ExpenseByTypeService.GetExpenseByTypesByTypeID(expenseTypeID, function (response) {
                vm.ExpenseByTypes = response.data.ExpenseByTypes;
            });
        }


        function saveExpenseByTypes() {
            ExpenseByTypeService.SaveExpenseByTypes(vm.ExpenseByTypes, vm.expenseTypeID, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addExpenseByType() {

            var ExpenseByType = { "ID": 0, "Name": "" };

            vm.ExpenseByTypes.push(ExpenseByType);
        }

        function removeExpenseByType(index) {
            vm.ExpenseByTypes.splice(index, 1);
        }

        function expenseTypeSelectionChanged(expenseTypeID) {
            vm.expenseTypeID = expenseTypeID;
            vm.getExpenseByTypesByTypeID(expenseTypeID);
        }

        vm.getExpenses();
        vm.getExpenseTypes();
        vm.getExpenseByTypesByTypeID(vm.expenseTypeID);

        
    }


})();