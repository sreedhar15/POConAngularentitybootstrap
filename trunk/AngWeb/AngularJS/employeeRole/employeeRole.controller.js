(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployeeRoleController', EmployeeRoleController);

    EmployeeRoleController.$inject = ['$rootScope', 'FlashService', 'EmployeeRoleService'];

    function EmployeeRoleController($rootScope, FlashService, EmployeeRoleService) {

        var vm = this;

        vm.EmployeeRoles = [];

        vm.filter = "";
        
        vm.getEmployeeRoles = getEmployeeRoles;
        vm.getEmployeeRolesByFilter = getEmployeeRolesByFilter;
        vm.saveEmployeeRoles = saveEmployeeRoles;
        vm.addEmployeeRole = addEmployeeRole;
        vm.removeEmployeeRole = removeEmployeeRole;
        
        function getEmployeeRoles() {
            EmployeeRoleService.GetEmployeeRoles(function (response) {
                vm.EmployeeRoles = response.data.EmployeeRoles;
            });
        }

        function getEmployeeRolesByFilter(filter) {

            vm.filter = filter;

            EmployeeRoleService.GetEmployeeRolesByFilter(filter, function (response) {
                vm.EmployeeRoles = response.data.EmployeeRoles;
     
            });
            
        }


        function saveEmployeeRoles() {
            EmployeeRoleService.SaveEmployeeRoles(vm.EmployeeRoles, vm.filter, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addEmployeeRole() {

            var EmployeeRole = { "ID": 0, "Name": "" };

            vm.EmployeeRoles.push(EmployeeRole);
        }

        function removeEmployeeRole(index) {
            vm.EmployeeRoles.splice(index, 1);
        }

       
        vm.getEmployeeRolesByFilter('A,B,C,D');
    }


})();