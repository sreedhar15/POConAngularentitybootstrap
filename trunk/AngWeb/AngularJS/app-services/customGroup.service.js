(function () {
    'use strict';

    angular
        .module('app')
        .factory('CustomGroupService', CustomGroupService);

    CustomGroupService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function CustomGroupService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetCustomGroups = GetCustomGroups;
        service.SaveCustomGroups = SaveCustomGroups;

        return service;

        function GetCustomGroups(callback) {
            $http.get(appConfig.apiRoot + '/api/CustomGroup').then(function (response) {
                callback(response);
            });
           
        }
       
        function SaveCustomGroups(customGroups, callback) {
            $http.post(appConfig.apiRoot + '/api/CustomGroup', customGroups).then(function (response) {

                callback(response);
            });
        }

    }


})();