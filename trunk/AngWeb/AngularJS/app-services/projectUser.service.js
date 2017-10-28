(function () {
    'use strict';

    angular
        .module('app')
        .factory('ProjectUserService', ProjectUserService);

    ProjectUserService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function ProjectUserService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetProjects = GetProjects;
        service.GetSecurityRoles = GetSecurityRoles;
        service.GetProjectUsers = GetProjectUsers;
        service.SaveProjectUsers = SaveProjectUsers;
        service.GetProjectUsersByUserID = GetProjectUsersByUserID;
        return service;

        function GetProjects(callback) {
            $http.get(appConfig.apiRoot + '/api/Project').then(function (response) {
                callback(response);
            });
        }

        function GetSecurityRoles(callback) {
            $http.get(appConfig.apiRoot + '/api/SecurityRole').then(function (response) {
                callback(response);
            });
        }

        function GetProjectUsers(callback) {
            $http.get(appConfig.apiRoot + '/api/ProjectUser').then(function (response) {
                callback(response);
            });
        }

        function GetProjectUsersByUserID(userID, callback) {
            $http.get(appConfig.apiRoot + '/api/ProjectUser?userID = ' + userID).then(function (response) {
                callback(response);
            });
        }

        function SaveProjectUsers(ProjectUsers, callback) {
            $http.post(appConfig.apiRoot + '/api/ProjectUser', ProjectUsers).then(function (response) {
                callback(response);
            });
        }
    }

})();