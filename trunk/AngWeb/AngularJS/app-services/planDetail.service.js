(function () {
    'use strict';

    angular
        .module('app')
        .factory('PlanDetailService', PlanDetailService);

    PlanDetailService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function PlanDetailService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetPlanDetails = GetPlanDetails;
        service.SavePlanDetails = SavePlanDetails;

        return service;

        function GetPlanDetails(planID, callback) {
            $http.get(appConfig.apiRoot + '/api/PlanDetail?planID=' + planID).then(function (response) {
                callback(response);
            });
        }

        function SavePlanDetails(PlanDetails, callback) {
            $http.post(appConfig.apiRoot + '/api/PlanDetail', PlanDetails).then(function (response) {

                callback(response);
            });
        }

    }


})();