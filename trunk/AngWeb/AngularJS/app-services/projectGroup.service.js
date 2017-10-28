(function () {
    'use strict';

    angular.module('app').factory('ProjectGroupService', ProjectGroupService);

    ProjectGroupService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function ProjectGroupService($http, $cookieStore, $rootScope, appConfig, $timeout) {
        var service = {};

        service.GetProjectGroups = GetProjectGroups;
        service.SaveProjectGroups = SaveProjectGroups;

        return service;

        function GetProjectGroups(callback) {
            $http.get(appConfig.apiRoot + '/api/ProjectGroup').then(function (response) {
                callback(response);
            });
        }

        function SaveProjectGroups(projectGroups, callback) {
            $http.post(appConfig.apiRoot + '/api/ProjectGroup', projectGroups).then(function (response) {
                callback(response);
            });
        }
    }

})();   