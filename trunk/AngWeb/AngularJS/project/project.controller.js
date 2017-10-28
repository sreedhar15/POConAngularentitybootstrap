(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectController', ProjectController);

    ProjectController.$inject = ['$rootScope', 'FlashService', 'ProjectService'];

    function ProjectController($rootScope,FlashService,ProjectService) {

        var vm = this;

        vm.projects = [];

        vm.filter = "";

        vm.getProjectsByFilter = getProjectsByFilter;


        vm.getProjects = getProjects;

        vm.saveProjects = saveProjects;

        vm.addProject = addProject;
        vm.removeProject = removeProject;

        function getProjects() {
                ProjectService.GetProjects(function (response) {
                vm.projects = response.data.projects;
            });
        }

        function getProjectsByFilter(filter) {

            vm.filter = filter;

            ProjectService.GetProjectsByFilter(filter, function (response) {
                vm.projects = response.data.projects;

            });

        }
        function saveProjects() {
            ProjectService.SaveProjects(vm.projects, vm.filter, function (response) {
                    FlashService.Success('Saved successfully.', false);

            });
        }

        function addProject() {

            var project = { "ID": 0, "ProjectName": "", "Priority": 0 };

            vm.projects.push(project);
        }

        function removeProject(index) {
            vm.projects.splice(index, 1);
        }

        vm.getProjectsByFilter('A,B,C,D');
    }


})();