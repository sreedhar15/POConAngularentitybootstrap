(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location', 'FlashService', 'AuthenticationService'];

    function LoginController($location, FlashService, AuthenticationService)
	{
        var vm = this;

        vm.login = login;

        vm.hasUserRole = hasUserRole;

       
        function hasUserRole(role) {
            return AuthenticationService.HasUserRole(role);
        }

        function login() {
            vm.dataLoading = true;

            AuthenticationService.Login(vm.username, vm.password, function (response) {

               vm.dataLoading = false;


               if (response.message == "ValidUser") {

                   AuthenticationService.SetCredentials(vm.username, vm.password, response.userRoles);

                    $location.path('/menu');
                }
                else {
                    AuthenticationService.ClearCredentials();
                    $location.path('/login');
                    FlashService.Error('Invalid user', false);
                }
               

            });
        };
    }

   
})();
