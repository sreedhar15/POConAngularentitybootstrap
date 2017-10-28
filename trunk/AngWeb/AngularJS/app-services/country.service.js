(function () {
    'use strict';

    angular
        .module('app')
        .factory('CountryService', CountryService);

    CountryService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function CountryService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetCountrys = GetCountrys;
        service.SaveCountrys = SaveCountrys;

        return service;

        function GetCountrys(callback) {
            $http.get(appConfig.apiRoot + '/api/Country').then(function (response) {
                callback(response);
            });
        }

        function SaveCountrys(Countrys, callback) {
            $http.post(appConfig.apiRoot + '/api/Country', Countrys).then(function (response) {

                callback(response);
            });
        }

    }


})();