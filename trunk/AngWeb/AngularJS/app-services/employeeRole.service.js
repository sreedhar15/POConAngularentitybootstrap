(function () {
    'use strict';

    angular
        .module('app')
        .factory('EmployeeRoleService', EmployeeRoleService);

    EmployeeRoleService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function EmployeeRoleService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetEmployeeRoles = GetEmployeeRoles;
        service.GetEmployeeRolesByFilter = GetEmployeeRolesByFilter;
        service.SaveEmployeeRoles = SaveEmployeeRoles;

        return service;

        function GetEmployeeRoles(callback) {
            $http.get(appConfig.apiRoot + '/api/EmployeeRole').then(function (response) {
                callback(response);
            });
        }

        function GetEmployeeRolesByTypeID(employeeRoleTypeID, callback) {
            $http.get(appConfig.apiRoot + '/api/EmployeeRole').then(function (response) {
                callback(response);
            });
        }

        function GetEmployeeRolesByFilter(filter, callback) {

            $http.get(appConfig.apiRoot + '/api/EmployeeRole?filter=' + filter).then(function (response) {
                callback(response);
            });
        }

        function SaveEmployeeRoles(EmployeeRoles, filter, callback) {

            $http.post(appConfig.apiRoot + '/api/EmployeeRole?filter=' + filter, EmployeeRoles).then(function (response) {

                callback(response);
            });
        }

    }


})();