(function () {
    'use strict';

    angular
        .module('app')
        .factory('FranchiseService', FranchiseService);

    FranchiseService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function FranchiseService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetFranchises = GetFranchises;
        service.SaveFranchises = SaveFranchises;

        return service;

        function GetFranchises(callback) {
            $http.get(appConfig.apiRoot + '/api/Franchise').then(function (response) {
                callback(response);
            });
        }

        function SaveFranchises(Franchises, callback) {
            $http.post(appConfig.apiRoot + '/api/Franchise', Franchises).then(function (response) {

                callback(response);
            });
        }

    }


})();