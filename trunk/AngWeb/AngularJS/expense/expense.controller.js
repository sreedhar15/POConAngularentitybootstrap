(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExpenseController', ExpenseController);

    ExpenseController.$inject = ['$rootScope', 'FlashService', 'ExpenseService'];

    function ExpenseController($rootScope, FlashService, ExpenseService) {

        var vm = this;

        vm.Expenses = [];

        vm.filter = "";
        vm.getExpenses = getExpenses;
        vm.getExpensesByFilter = getExpensesByFilter;
        vm.saveExpenses = saveExpenses;
        vm.addExpense = addExpense;
        vm.removeExpense = removeExpense;

        function getExpenses() {
            ExpenseService.GetExpenses(function (response) {
                vm.Expenses = response.data.Expenses;
            });
        }

        function getExpensesByFilter(filter) {

            vm.filter = filter;

            ExpenseService.GetExpensesByFilter(filter, function (response) {
                vm.Expenses = response.data.Expenses;

            });

        }


        function saveExpenses() {
            ExpenseService.SaveExpenses(vm.Expenses, vm.filter, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addExpense() {

            var Expense = { "ID": 0, "Name": "" };

            vm.Expenses.push(Expense);
        }

        function removeExpense(index) {
            vm.Expenses.splice(index, 1);
        }


        vm.getExpensesByFilter('A,B,C,D');
    }


})();