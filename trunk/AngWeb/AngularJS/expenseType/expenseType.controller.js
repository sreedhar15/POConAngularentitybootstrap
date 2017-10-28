(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExpenseTypeController', ExpenseTypeController);

    ExpenseTypeController.$inject = ['$rootScope', 'FlashService', 'ExpenseTypeService'];

    function ExpenseTypeController($rootScope, FlashService, ExpenseTypeService) {

        var vm = this;

        vm.ExpenseTypes = [];

        vm.getExpenseTypes = getExpenseTypes;

        vm.saveExpenseTypes = saveExpenseTypes;

        vm.addExpenseType = addExpenseType;
        vm.removeExpenseType = removeExpenseType;

        function getExpenseTypes() {
            ExpenseTypeService.GetExpenseTypes(function (response) {
                vm.ExpenseTypes = response.data.ExpenseTypes;
            });
        }

        function saveExpenseTypes() {
            ExpenseTypeService.SaveExpenseTypes(vm.ExpenseTypes, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addExpenseType() {

            var ExpenseType = { "ID": 0, "Name": "" };

            vm.ExpenseTypes.push(ExpenseType);
        }

        function removeExpenseType(index) {
            vm.ExpenseTypes.splice(index, 1);
        }

        vm.getExpenseTypes();
    }


})();