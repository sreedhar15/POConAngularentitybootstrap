(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserSecurityRoleController', UserSecurityRoleController);

    UserSecurityRoleController.$inject = ['$rootScope', 'FlashService', 'UserService', 'UserSecurityRoleService'];

    function UserSecurityRoleController($rootScope, FlashService, UserService, UserSecurityRoleService) {

        var vm = this;

        vm.Users = []
        vm.SecurityRoles = [];
        vm.UserSecurityRoles = []; 

        vm.getUsers = getUsers;
        vm.getSecurityRoles = getSecurityRoles;
        vm.getUserSecurityRoles = getUserSecurityRoles;
        vm.addUserSecurityRoles = addUserSecurityRoles;
        vm.saveUserSecurityRoles = saveUserSecurityRoles;
        vm.removeUserSecurityRole = removeUserSecurityRole;
        
        //User Details
        function getUsers() {
            UserService.GetUsers(function (response) {
                vm.Users = response.data.users;
            });
        }

        //Security Role Details
        function getSecurityRoles() {
            UserSecurityRoleService.GetSecurityRoles(function (response) {
                vm.SecurityRoles = response.data.SecurityRoles;
            });
        }

        //User Security Role Details
        function getUserSecurityRoles() {
            UserSecurityRoleService.GetUserSecurityRoles(function (response) {
                vm.UserSecurityRoles = response.data.UserSecurityRoles;
            });
        }

        //Add User Security Role
        function addUserSecurityRoles() {
            var userSecurityRoles = { "UserID": "", "SecurityRoleID": "" };
            vm.UserSecurityRoles.push(userSecurityRoles);
        }

        //Save User Security Role Details
        function saveUserSecurityRoles() {
            UserSecurityRoleService.SaveUserSecurityRoles(vm.UserSecurityRoles, function (response) {
                FlashService.Success('Saved successfully.', false);
            });
        }

        //Remove User Security Role
        function removeUserSecurityRole(index) {
            vm.UserSecurityRoles.splice(index, 1);
        }


        vm.getUsers();
        vm.getSecurityRoles();
        vm.getUserSecurityRoles();
    }

})();