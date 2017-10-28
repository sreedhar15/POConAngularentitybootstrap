(function () {
    'use strict';

    angular
        .module('app')
        .factory('ExpenseTypeService', ExpenseTypeService);

    ExpenseTypeService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function ExpenseTypeService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetExpenseTypes = GetExpenseTypes;
        service.SaveExpenseTypes = SaveExpenseTypes;

        return service;

        function GetExpenseTypes(callback) {
            $http.get(appConfig.apiRoot + '/api/ExpenseType').then(function (response) {
                callback(response);
            });
        }

        function SaveExpenseTypes(ExpenseTypes, callback) {
            $http.post(appConfig.apiRoot + '/api/ExpenseType', ExpenseTypes).then(function (response) {

                callback(response);
            });
        }

    }


})();