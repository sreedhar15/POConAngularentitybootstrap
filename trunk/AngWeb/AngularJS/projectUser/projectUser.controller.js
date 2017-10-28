(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectUserController', ProjectUserController);

    ProjectUserController.$inject = ['$rootScope', 'FlashService', 'UserService', 'ProjectUserService'];

    function ProjectUserController($rootScope, FlashService, UserService, ProjectUserService) {

        var vm = this;

        vm.Users = []
        vm.Projects = [];
        vm.SecurityRoles = [];
        vm.ProjectUsers = [];

        vm.getUsers = getUsers;
        vm.getProjects = getProjects;
        vm.getSecurityRoles = getSecurityRoles;
        vm.getProjectUsers = getProjectUsers;
        vm.addProjectUsers = addProjectUsers;
        vm.saveProjectUsers = saveProjectUsers;
        vm.removeProjectUser = removeProjectUser;

        //User Details
        function getUsers() {
            UserService.GetUsers(function (response) {
                vm.Users = response.data.users;
            });
        }

        function getProjects() {
            ProjectUserService.GetProjects(function (response) {
                vm.Projects = response.data.projects;
            });
        }

        //Security Role Details
        function getSecurityRoles() {
            ProjectUserService.GetSecurityRoles(function (response) {
                vm.SecurityRoles = response.data.SecurityRoles;
            });
        }

        //Project User Details
        function getProjectUsers() {
            ProjectUserService.GetProjectUsers(function (response) {
                vm.ProjectUsers = response.data.ProjectUsers;
            });
        }

        //Add Project User
        function addProjectUsers() {
            var projectUsers = { "UserID": "", "ProjectID": "", "SecurityRoleID": "" };
            vm.ProjectUsers.push(projectUsers);
        }

        //Save Project User Details
        function saveProjectUsers() {
            ProjectUserService.SaveProjectUsers(vm.ProjectUsers, function (response) {
                FlashService.Success('Saved successfully.', false);
            });
        }

        //Remove Project User
        function removeProjectUser(index) {
            vm.ProjectUsers.splice(index, 1);
        }


        vm.getUsers();
        vm.getProjects();
        vm.getSecurityRoles();
        vm.getProjectUsers();
    }

})();