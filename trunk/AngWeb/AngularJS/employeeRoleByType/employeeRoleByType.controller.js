(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployeeRoleByTypeController', EmployeeRoleByTypeController);

    EmployeeRoleByTypeController.$inject = ['$rootScope', 'FlashService', 'EmployeeRoleService', 'EmployeeRoleTypeService', 'EmployeeRoleByTypeService'];

    function EmployeeRoleByTypeController($rootScope, FlashService, EmployeeRoleService, EmployeeRoleTypeService,  EmployeeRoleByTypeService) {

        var vm = this;

        vm.EmployeeRoleByTypes = [];

        vm.employeeRoleTypeSelectionChanged = employeeRoleTypeSelectionChanged;
        vm.employeeRoleTypeID = 0;

        vm.getEmployeeRoleTypes = getEmployeeRoleTypes;
        vm.getEmployeeRoles = getEmployeeRoles;

        vm.getEmployeeRoleByTypes = getEmployeeRoleByTypes;
        vm.getEmployeeRoleByTypesByTypeID = getEmployeeRoleByTypesByTypeID;

        vm.saveEmployeeRoleByTypes = saveEmployeeRoleByTypes;
        vm.addEmployeeRoleByType = addEmployeeRoleByType;
        vm.removeEmployeeRoleByType = removeEmployeeRoleByType;

        function getEmployeeRoleTypes() {
            EmployeeRoleTypeService.GetEmployeeRoleTypes(function (response) {
                vm.EmployeeRoleTypes = response.data.EmployeeRoleTypes;
            });
        }

        function getEmployeeRoles() {
            EmployeeRoleService.GetEmployeeRoles(function (response) {
                vm.EmployeeRoles = response.data.EmployeeRoles;
            });
        }


        function getEmployeeRoleByTypes() {
            EmployeeRoleByTypeService.GetEmployeeRoleByTypes(function (response) {
                vm.EmployeeRoleByTypes = response.data.EmployeeRoleByTypes;
            });
        }

        ///filter by employeeRoleTypeID
        function getEmployeeRoleByTypesByTypeID(employeeRoleTypeID) {
            EmployeeRoleByTypeService.GetEmployeeRoleByTypesByTypeID(employeeRoleTypeID, function (response) {
                vm.EmployeeRoleByTypes = response.data.EmployeeRoleByTypes;
            });
        }

        function saveEmployeeRoleByTypes() {
            EmployeeRoleByTypeService.SaveEmployeeRoleByTypes(vm.EmployeeRoleByTypes, vm.employeeRoleTypeID, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addEmployeeRoleByType() {

            var EmployeeRoleByType = { "ID": 0, "EmployeeRoleTypeID":0, "EmployeeRoleID": 0 };

            vm.EmployeeRoleByTypes.push(EmployeeRoleByType);
        }

        function removeEmployeeRoleByType(index) {
            vm.EmployeeRoleByTypes.splice(index, 1);
        }

        function employeeRoleTypeSelectionChanged(employeeRoleTypeID) {
            vm.employeeRoleTypeID = employeeRoleTypeID;
            vm.getEmployeeRoleByTypesByTypeID(employeeRoleTypeID);
        }

        vm.getEmployeeRoles();
        vm.getEmployeeRoleTypes();
        vm.getEmployeeRoleByTypesByTypeID(vm.employeeRoleTypeID);
       
    }


})();