(function () {
    'use strict';

    angular
        .module('app')
        .factory('ProjectService', ProjectService);

    ProjectService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function ProjectService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetProjects = GetProjects;
        service.GetProjectsByFilter = GetProjectsByFilter;
        service.SaveProjects = SaveProjects;

        return service;

        function GetProjects(callback) {
            $http.get(appConfig.apiRoot + '/api/Project').then(function (response) {
                callback(response);
            });
        }

        function GetProjectsByFilter(filter, callback) {

            $http.get(appConfig.apiRoot + '/api/Project?filter=' + filter).then(function (response) {
                callback(response);
            });
        }

        function SaveProjects(projects, filter, callback) {
            $http.post(appConfig.apiRoot + '/api/Project?filter=' + filter, projects).then(function (response) {

           // $http.post(appConfig.apiRoot + '/api/Project', projects).then(function (response) {

            callback(response);
            });
        }

    }


})();