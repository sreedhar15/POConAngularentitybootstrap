(function () {
    'use strict';

    angular
        .module('app')
        .controller('CustomProjectGroupController', CustomProjectGroupController);

    CustomProjectGroupController.$inject = ['$rootScope', 'FlashService', 'ProjectService', 'CustomProjectGroupService', 'ProjectGroupService', 'CustomGroupService'];

    function CustomProjectGroupController($rootScope, FlashService, ProjectService, CustomProjectGroupService, ProjectGroupService, CustomGroupService) {

        var vm = this;

       // vm.Projects = []
      //  vm.ProjectGroups = [];
       // vm.CustomGroups = [];
        vm.CustomProjectGroups = [];

        vm.customGroupSelectionChanged = customGroupSelectionChanged;
        vm.customGroupID = 0;

        vm.projectGroupId = 0;
        vm.projectGroupIdSelectionChanged = projectGroupIdSelectionChanged;

        vm.getProjects = getProjects;
        vm.getProjectGroups = getProjectGroups;
        vm.getCustomGroups = getCustomGroups;

        vm.getCustomProjectGroups = getCustomProjectGroups;
        vm.addCustomProjectGroups = addCustomProjectGroups;
        vm.saveCustomProjectGroups = saveCustomProjectGroups;
        vm.removeCustomProjectGroup = removeCustomProjectGroup;
        vm.getCustomProjectGroupbyCustomGroupID = getCustomProjectGroupbyCustomGroupID;

        //User Details
        function getProjects() {
            ProjectService.GetProjects(function (response) {
                vm.Projects = response.data.projects;
            });
            }

        function getProjectGroups() {
            ProjectGroupService.GetProjectGroups(function (response) {
                vm.ProjectGroups = response.data.ProjectGroups;
            });
        }

        //CustomGroups
        function getCustomGroups() {
            CustomGroupService.GetCustomGroups(function (response) {
                vm.CustomGroups = response.data.customGroups;
            });
            
        }

        //CustomProject Group  Details
        function getCustomProjectGroups() {
            CustomProjectGroupService.GetCustomProjectGroups(function (response) {
                vm.CustomProjectGroups = response.data.CustomProjectGroups;
            });
        }

        //Add Custom Project Group 
        function addCustomProjectGroups() {
            var customProjectGroup = { "CustomGroupID": "", "ProjectGroupID": "", "ProjectID": "" };
            vm.CustomProjectGroups.push(customProjectGroup);
        }

        //Save custom Project Group  Details
        function saveCustomProjectGroups() {
            CustomProjectGroupService.SaveCustomProjectGroups(vm.CustomProjectGroups, vm.customGroupID,vm.projectGroupId, function (response) {
                FlashService.Success('Saved successfully.', false);
            });
        }

        //Remove Custom Project Group 
        function removeCustomProjectGroup(index) {
            vm.CustomProjectGroups.splice(index, 1);
        }
        ///filter by CustomGroupID
        function getCustomProjectGroupbyCustomGroupID(customGroupID, projectGroupId) {
             CustomProjectGroupService.GetCustomProjectGroupsByCustomGroupID(customGroupID,projectGroupId, function (response) {
                 vm.CustomProjectGroups = response.data.CustomProjectGroups;
             
            });
        }
        ///filter by ProjectGroupID
        function getCustomProjectGroupbyProjectGroupID(projectGroupID) {
            CustomProjectGroupService.GetCustomProjectGroupbyProjectGroupID(projectGroupID, function (response) {
                vm.CustomProjectGroups = response.data.ExpenseByTypes;
            });
        }


        function customGroupSelectionChanged(customGroupID) {
            vm.customGroupID = customGroupID;
            vm.getCustomProjectGroupbyCustomGroupID(customGroupID,  vm.projectGroupId);
            
        }
        function projectGroupIdSelectionChanged(projectGroupId) {
            vm.projectGroupId = projectGroupId;
            vm.getCustomProjectGroupbyCustomGroupID(vm.customGroupID, projectGroupId);

        }
        vm.getProjects();
        vm.getProjectGroups();
        vm.getCustomGroups();
       // vm.getCustomProjectGroups();
        vm.getCustomProjectGroupbyCustomGroupID(vm.customGroupID, vm.projectGroupId);
    }

})();