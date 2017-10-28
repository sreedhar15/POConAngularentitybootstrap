(function () {
    'use strict';

    angular
        .module('app')
        .factory('DVPService', DVPService);

    DVPService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function DVPService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetDVPs = GetDVPs;
        service.SaveDVPs = SaveDVPs;

        return service;

        function GetDVPs(callback) {
            $http.get(appConfig.apiRoot + '/api/DVP').then(function (response) {
                callback(response);
            });
        }

        function SaveDVPs(DVPs, callback) {
            $http.post(appConfig.apiRoot + '/api/DVP', DVPs).then(function (response) {

                callback(response);
            });
        }

    }


})();