(function () {
    'use strict';

    angular
        .module('app')
        .factory('PlanGroupService', PlanGroupService);

    PlanGroupService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function PlanGroupService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetPlanGroups = GetPlanGroups;
        service.SavePlanGroups = SavePlanGroups;

        return service;

        function GetPlanGroups(callback) {
            $http.get(appConfig.apiRoot + '/api/PlanGroup').then(function (response) {
                callback(response);
            });
        }

        function SavePlanGroups(planGroups, callback) {
            $http.post(appConfig.apiRoot + '/api/PlanGroup', planGroups).then(function (response) {

                callback(response);
            });
        }

    }


})();