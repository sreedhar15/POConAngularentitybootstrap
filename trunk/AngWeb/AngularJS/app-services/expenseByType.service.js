(function () {
    'use strict';

    angular
        .module('app')
        .factory('ExpenseByTypeService', ExpenseByTypeService);

    ExpenseByTypeService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function ExpenseByTypeService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetExpenseByTypes = GetExpenseByTypes;
        service.GetExpenseByTypesByTypeID = GetExpenseByTypesByTypeID;
        service.GetExpenseByTypesList = GetExpenseByTypesList;
        service.SaveExpenseByTypes = SaveExpenseByTypes;

        return service;

        function GetExpenseByTypes(callback) {
            $http.get(appConfig.apiRoot + '/api/ExpenseByType').then(function (response) {
                callback(response);
            });
        }

        function GetExpenseByTypesByTypeID(expenseTypeID, callback) {
            $http.get(appConfig.apiRoot + '/api/ExpenseByType?expenseTypeID=' + expenseTypeID).then(function (response) {
                callback(response);
            });
        }

        // list method
        function GetExpenseByTypesList(expenseTypeID, callback) {
            $http.get(appConfig.apiRoot + '/api/ExpenseByType?list=true&expenseTypeID=' + expenseTypeID).then(function (response) {
                callback(response);
            });
        }


        function SaveExpenseByTypes(ExpenseByTypes, expenseTypeID, callback) {
            $http.post(appConfig.apiRoot + '/api/ExpenseByType?expenseTypeID=' + expenseTypeID, ExpenseByTypes).then(function (response) {

                callback(response);
            });
        }

    }


})();