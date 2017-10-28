(function () {
    'use strict';

    angular
        .module('app')
        .factory('ExpenseService', ExpenseService);

    ExpenseService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function ExpenseService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetExpenses = GetExpenses;
        service.GetExpensesByFilter = GetExpensesByFilter;
        service.SaveExpenses = SaveExpenses;

        return service;

        function GetExpenses(callback) {
            $http.get(appConfig.apiRoot + '/api/Expense').then(function (response) {
                callback(response);
            });
        }

        function GetExpensesByFilter(filter, callback) {

            $http.get(appConfig.apiRoot + '/api/Expense?filter=' + filter).then(function (response) {
                callback(response);
            });
        }

        function SaveExpenses(Expenses, filter, callback) {

          
            $http.post(appConfig.apiRoot + '/api/Expense?filter=' + filter, Expenses).then(function (response) {

                callback(response);
            });
        }

    }


})();