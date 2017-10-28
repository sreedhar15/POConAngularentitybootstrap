(function () {
    'use strict';
    angular
        .module('app')
        .factory('UserService', UserService);

    UserService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function UserService($http, $cookieStore, $rootScope, appConfig, $timeout) {
        var service = {};
        service.GetUsers = GetUsers;
        service.GetUsersByFilter = GetUsersByFilter;
        service.SaveUserDetails = SaveUserDetails;
        return service;

        function GetUsers(callback) {
            $http.get(appConfig.apiRoot + '/api/User').then(function (response) {
                callback(response);
             });
        }

        function GetUsersByFilter(filter, callback) {

            $http.get(appConfig.apiRoot + '/api/User?filter=' + filter).then(function (response) {
                callback(response);
            });
        }

        
        function SaveUserDetails(UserDetails, filter, callback) {
            $http.post(appConfig.apiRoot + '/api/User?filter=' + filter, UserDetails).then(function (response) {

                callback(response);
            });
        }

    }
})();