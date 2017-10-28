(function () {
    'use strict';

    angular.module('app').controller('ProjectGroupController', ProjectGroupController);

    ProjectGroupController.$inject = ['$rootScope', 'FlashService', 'ProjectGroupService'];

    function ProjectGroupController($rootScope, FlashService, ProjectGroupService) {

        var vm = this;

        vm.ProjectGroups = [];

        vm.getProjectGroups = getProjectGroups;
        vm.saveProjectGroups = saveProjectGroups;
        vm.addProjectGroup = addProjectGroup;
        vm.removeProjectGroup = removeProjectGroup;

        function getProjectGroups() {
            ProjectGroupService.GetProjectGroups(function (response) {
                vm.ProjectGroups = response.data.ProjectGroups;
            });
        }

        function saveProjectGroups() {
            ProjectGroupService.SaveProjectGroups(vm.ProjectGroups, function (response) {
                FlashService.Success('Saved successfully.', false);
            });
        }

        function addProjectGroup() {
            var projectGroup = { "ID": 0, "Name": "" };
            vm.ProjectGroups.push(projectGroup);
        }

        function removeProjectGroup(index) {
            vm.ProjectGroups.splice(index, 1);
        }

        vm.getProjectGroups();
    }
})();