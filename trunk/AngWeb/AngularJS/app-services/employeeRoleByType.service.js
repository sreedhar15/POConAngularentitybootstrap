(function () {
    'use strict';

    angular
        .module('app')
        .factory('EmployeeRoleByTypeService', EmployeeRoleByTypeService);

    EmployeeRoleByTypeService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function EmployeeRoleByTypeService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetEmployeeRoleByTypes = GetEmployeeRoleByTypes;
        service.GetEmployeeRoleByTypesByTypeID = GetEmployeeRoleByTypesByTypeID;
        service.GetEmployeeRoleByTypesList = GetEmployeeRoleByTypesList;
        service.SaveEmployeeRoleByTypes = SaveEmployeeRoleByTypes;

        return service;

        function GetEmployeeRoleByTypes(callback) {
            $http.get(appConfig.apiRoot + '/api/EmployeeRoleByType').then(function (response) {
                callback(response);
            });
        }

        function GetEmployeeRoleByTypesByTypeID(employeeRoleTypeID, callback) {
            $http.get(appConfig.apiRoot + '/api/EmployeeRoleByType?employeeRoleTypeID=' + employeeRoleTypeID).then(function (response) {
                callback(response);
            });
        }

        // list method
        function GetEmployeeRoleByTypesList(employeeRoleTypeID, callback) {
            $http.get(appConfig.apiRoot + '/api/EmployeeRoleByType?list=true&employeeRoleTypeID=' + employeeRoleTypeID).then(function (response) {
                callback(response);
            });
        }

        function SaveEmployeeRoleByTypes(EmployeeRoleByTypes, employeeRoleTypeID, callback) {
            $http.post(appConfig.apiRoot + '/api/EmployeeRoleByType?employeeRoleTypeID=' + employeeRoleTypeID, EmployeeRoleByTypes).then(function (response) {

                callback(response);
            });
        }

    }


})();