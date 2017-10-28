    (function () {
    'use strict';

    angular
        .module('app')
        .controller('CustomGroupController', CustomGroupController);

    CustomGroupController.$inject = ['$rootScope', 'FlashService', 'CustomGroupService'];

    function CustomGroupController($rootScope, FlashService, CustomGroupService) {

        var vm = this;

        vm.customGroups = [];

        vm.getCustomGroups = getCustomGroups;

        vm.saveCustomGroups = saveCustomGroups;

        vm.addCustomGroup = addCustomGroup;
        vm.removeCustomGroup = removeCustomGroup;

        function getCustomGroups() {
            CustomGroupService.GetCustomGroups(function (response) {
                vm.customGroups = response.data.customGroups;
            });
        }

        function saveCustomGroups() {
            CustomGroupService.SaveCustomGroups(vm.customGroups, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addCustomGroup() {

            var customGroup = { "ID": 0, "CustomGroupName": "", "Priority": 0 };

            vm.customGroups.push(customGroup);
        }

        function removeCustomGroup(index) {
            vm.customGroups.splice(index, 1);
        }

        vm.getCustomGroups();
    }


})();