(function () {
    'use strict';

    angular
        .module('app')
        .controller('MenuController', MenuController);

    MenuController.$inject = ['$rootScope', 'FlashService', 'AuthenticationService'];

    function MenuController($rootScope, FlashService, AuthenticationService) 
    {
        var vm = this;

        vm.hasUserRole = hasUserRole;


        function hasUserRole(role) {
            var result = AuthenticationService.HasUserRole(role);
            return result;
        }

     
    }


})();
