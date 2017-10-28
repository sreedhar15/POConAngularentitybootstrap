(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserSecurityRoleService', UserSecurityRoleService);

    UserSecurityRoleService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function UserSecurityRoleService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetSecurityRoles = GetSecurityRoles;
        service.GetUserSecurityRoles = GetUserSecurityRoles;
        service.SaveUserSecurityRoles = SaveUserSecurityRoles;
        service.GetUserSecurityRolesByUserID = GetUserSecurityRolesByUserID;
        return service;

        function GetSecurityRoles(callback) {
            $http.get(appConfig.apiRoot + '/api/SecurityRole').then(function (response) {
                callback(response);
            });
        }

        function GetUserSecurityRoles(callback) {
            $http.get(appConfig.apiRoot + '/api/UserSecurityRole').then(function (response) {
                callback(response);
            });
        }

        function GetUserSecurityRolesByUserID(userID, callback) {
            $http.get(appConfig.apiRoot + '/api/UserSecurityRole?userID = ' + userID).then(function (response) {
                callback(response);
            });
        }

        function SaveUserSecurityRoles(UserSecurityRoles, callback) {
            $http.post(appConfig.apiRoot + '/api/UserSecurityRole', UserSecurityRoles).then(function (response) {
                callback(response);
            });
        }
    }

})();