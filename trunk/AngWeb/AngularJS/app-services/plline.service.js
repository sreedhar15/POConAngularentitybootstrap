(function () {
    'use strict';

    angular
        .module('app')
        .factory('PLLineService', PLLineService);

    PLLineService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function PLLineService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetPLLines = GetPLLines;
        service.SavePLLines = SavePLLines;

        return service;

        function GetPLLines(callback) {
            $http.get(appConfig.apiRoot + '/api/PLLine').then(function (response) {
                callback(response);
            });
        }

        function SavePLLines(pllines, callback) {
            $http.post(appConfig.apiRoot + '/api/PLLine', pllines).then(function (response) {

                callback(response);
            });
        }

    }


})();