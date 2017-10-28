(function () {
    'use strict';

    angular
        .module('app')
        .factory('PlanService', PlanService);

    PlanService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function PlanService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetPlans = GetPlans;
        service.SavePlans = SavePlans;

        return service;

        function GetPlans(callback) {
            $http.get(appConfig.apiRoot + '/api/Plan').then(function (response) {
                callback(response);
            });
        }

        function SavePlans(plans, callback) {
            $http.post(appConfig.apiRoot + '/api/Plan', plans).then(function (response) {

                callback(response);
            });
        }

    }


})();