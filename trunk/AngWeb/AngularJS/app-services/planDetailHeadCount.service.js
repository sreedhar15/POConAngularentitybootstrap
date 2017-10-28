(function () {
    'use strict';

    angular
        .module('app')
        .factory('PlanDetailHeadCountService', PlanDetailHeadCountService);

    PlanDetailHeadCountService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function PlanDetailHeadCountService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetPlanDetailHeadCounts = GetPlanDetailHeadCounts;
        service.SavePlanDetailHeadCounts = SavePlanDetailHeadCounts;

        return service;

        function GetPlanDetailHeadCounts(planDetailID, projectID, employeeRoleTypeID, callback) {
            var url = appConfig.apiRoot + '/api/PlanDetailHeadCount?planDetailID='+ planDetailID + '&projectID=' + projectID + '&employeeRoleTypeID=' + employeeRoleTypeID;
            $http.get(url).then(function (response) {
                callback(response);
            });
        }

        function SavePlanDetailHeadCounts(PlanDetailHeadCounts, planDetailID, projectID, employeeRoleTypeID, callback) {
            var url = appConfig.apiRoot + '/api/PlanDetailHeadCount?planDetailID=' + planDetailID + '&projectID=' + projectID + '&employeeRoleTypeID=' + employeeRoleTypeID;
            $http.post(url, PlanDetailHeadCounts).then(function (response) {
                callback(response);
            });
        }

    }


})();