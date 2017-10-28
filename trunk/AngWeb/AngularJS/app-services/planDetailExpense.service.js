(function () {
    'use strict';

    angular
        .module('app')
        .factory('PlanDetailExpenseService', PlanDetailExpenseService);

    PlanDetailExpenseService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function PlanDetailExpenseService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetPlanDetailExpenses = GetPlanDetailExpenses;
        service.SavePlanDetailExpenses = SavePlanDetailExpenses;

        return service;

        function GetPlanDetailExpenses(planDetailID, projectID, expenseTypeID, callback) {
            var url = appConfig.apiRoot + '/api/PlanDetailExpense?planDetailID=' + planDetailID + '&projectID=' + projectID + '&expenseTypeID=' + expenseTypeID;
            $http.get(url).then(function (response) {
                callback(response);
            });
        }

        function SavePlanDetailExpenses(PlanDetailExpenses, planDetailID, projectID, expenseTypeID, callback) {
            var url = appConfig.apiRoot + '/api/PlanDetailExpense?planDetailID=' + planDetailID + '&projectID=' + projectID + '&expenseTypeID=' + expenseTypeID;
            $http.post(url, PlanDetailExpenses).then(function (response) {
                callback(response);
            });
        }

    }


})();