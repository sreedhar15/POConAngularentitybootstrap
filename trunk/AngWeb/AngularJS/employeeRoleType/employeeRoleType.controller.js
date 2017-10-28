(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployeeRoleTypeController', EmployeeRoleTypeController);

    EmployeeRoleTypeController.$inject = ['$rootScope', 'FlashService', 'EmployeeRoleTypeService'];

    function EmployeeRoleTypeController($rootScope, FlashService, EmployeeRoleTypeService) {

        var vm = this;

        vm.EmployeeRoleTypes = [];

        vm.getEmployeeRoleTypes = getEmployeeRoleTypes;

        vm.saveEmployeeRoleTypes = saveEmployeeRoleTypes;

        vm.addEmployeeRoleType = addEmployeeRoleType;
        vm.removeEmployeeRoleType = removeEmployeeRoleType;

        function getEmployeeRoleTypes() {
            EmployeeRoleTypeService.GetEmployeeRoleTypes(function (response) {
                vm.EmployeeRoleTypes = response.data.EmployeeRoleTypes;
            });
        }

        function saveEmployeeRoleTypes() {
            EmployeeRoleTypeService.SaveEmployeeRoleTypes(vm.EmployeeRoleTypes, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addEmployeeRoleType() {

            var EmployeeRoleType = { "ID": 0, "Name": "" };

            vm.EmployeeRoleTypes.push(EmployeeRoleType);
        }

        function removeEmployeeRoleType(index) {
            vm.EmployeeRoleTypes.splice(index, 1);
        }

        vm.getEmployeeRoleTypes();
    }


})();