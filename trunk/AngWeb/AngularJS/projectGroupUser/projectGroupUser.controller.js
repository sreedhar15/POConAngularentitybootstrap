(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectGroupUserController', ProjectGroupUserController);

    ProjectGroupUserController.$inject = ['$rootScope', 'FlashService', 'UserService', 'ProjectGroupUserService'];

    function ProjectGroupUserController($rootScope, FlashService, UserService, ProjectGroupUserService) {

        var vm = this;

        vm.Users = []
        vm.ProjectGroups = [];
        vm.SecurityRoles = [];
        vm.ProjectGroupUsers = [];

        vm.getUsers = getUsers;
        vm.getProjectGroups = getProjectGroups;
        vm.getSecurityRoles = getSecurityRoles;
        vm.getProjectGroupUsers = getProjectGroupUsers;
        vm.addProjectGroupUsers = addProjectGroupUsers;
        vm.saveProjectGroupUsers = saveProjectGroupUsers;
        vm.removeProjectGroupUser = removeProjectGroupUser;

        //User Details
        function getUsers() {
            UserService.GetUsers(function (response) {
                vm.Users = response.data.users;
            });
        }

        function getProjectGroups() {
            ProjectGroupUserService.GetProjectGroups(function (response) {
                vm.ProjectGroups = response.data.ProjectGroups;
            });
        }

        //Security Role Details
        function getSecurityRoles() {
            ProjectGroupUserService.GetSecurityRoles(function (response) {
                vm.SecurityRoles = response.data.SecurityRoles;
            });
        }

        //Project Group User Details
        function getProjectGroupUsers() {
            ProjectGroupUserService.GetProjectGroupUsers(function (response) {
                vm.ProjectGroupUsers = response.data.ProjectGroupUsers;
            });
        }

        //Add Project Group User
        function addProjectGroupUsers() {
            var projectGroupUsers = { "UserID": "", "ProjectGroupID": "", "SecurityRoleID":"" };
            vm.ProjectGroupUsers.push(projectGroupUsers);
        }

        //Save Project Group User Details
        function saveProjectGroupUsers() {
            ProjectGroupUserService.SaveProjectGroupUsers(vm.ProjectGroupUsers, function (response) {
                FlashService.Success('Saved successfully.', false);
            });
        }

        //Remove Project Group User
        function removeProjectGroupUser(index) {
            vm.ProjectGroupUsers.splice(index, 1);
        }


        vm.getUsers();
        vm.getProjectGroups();
        vm.getSecurityRoles();
        vm.getProjectGroupUsers();
    }

})();