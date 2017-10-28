(function () {
    'use strict';
    angular
    .module('app')
    .controller('UserController', UserController);
    UserController.$inject = ['$rootScope', 'FlashService', 'UserService'];
    function UserController($rootScope, FlashService, UserService) {
        var vm = this;
        vm.user = [];
        //Get Users
        vm.getUsers = getUsers;
        vm.getUsersByFilter = getUsersByFilter;
        vm.saveUserDetails = saveUserDetails;
        vm.addUserDetails = addUserDetails;
        vm.removeUser = removeUser;

        function getUsers() {
            UserService.GetUsers(function (response) {
                vm.user = response.data.users;
                
            });
        }       

        function getUsersByFilter(filter) {

            vm.filter = filter;

            UserService.GetUsersByFilter(filter, function (response) {
                vm.user = response.data.users;

            });

        }

        //add additional Details
        function addUserDetails(){
            var userDetail = {
                "LoginName":"",
                "FirstName":"",
                "LastName":"",
                "EmailID":"",
                "Phone":""
            };
            vm.user.push(userDetail);
        }
        
        function saveUserDetails()
        {
            UserService.SaveUserDetails(vm.user, vm.filter, function (response) {
                FlashService.Success('Saved successfully.', false);
                
            });
        }
        function removeUser(index) {
            vm.user.splice(index, 1);
        }

        vm.getUsersByFilter('A,B,C,D');
    }
})();