(function () {
    'use strict';

    angular
        .module('app')
        .factory('CostCenterService', CostCenterService);

    CostCenterService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function CostCenterService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetCostCenters = GetCostCenters;
        service.SaveCostCenters = SaveCostCenters;

        return service;

        function GetCostCenters(callback) {
            $http.get(appConfig.apiRoot + '/api/CostCenter').then(function (response) {
                callback(response);
            });
        }

        function SaveCostCenters(CostCenters, callback) {
            $http.post(appConfig.apiRoot + '/api/CostCenter', CostCenters).then(function (response) {

                callback(response);
            });
        }

    }


})();