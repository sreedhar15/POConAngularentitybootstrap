(function () {
    'use strict';

    angular
        .module('app')
        .factory('EmployeeRoleTypeService', EmployeeRoleTypeService);

    EmployeeRoleTypeService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function EmployeeRoleTypeService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetEmployeeRoleTypes = GetEmployeeRoleTypes;
        service.SaveEmployeeRoleTypes = SaveEmployeeRoleTypes;

        return service;

        function GetEmployeeRoleTypes(callback) {
            $http.get(appConfig.apiRoot + '/api/EmployeeRoleType').then(function (response) {
                callback(response);
            });
        }

        function SaveEmployeeRoleTypes(EmployeeRoleTypes, callback) {
            $http.post(appConfig.apiRoot + '/api/EmployeeRoleType', EmployeeRoleTypes).then(function (response) {

                callback(response);
            });
        }

    }


})();