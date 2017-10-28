(function () {
    'use strict';

    angular
        .module('app')
        .factory('CurrencyService', CurrencyService);

    CurrencyService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

    function CurrencyService($http, $cookieStore, $rootScope, appConfig, $timeout) {

        var service = {};

        service.GetCurrencys = GetCurrencys;
        service.SaveCurrencys = SaveCurrencys;

        return service;

        function GetCurrencys(callback) {
            $http.get(appConfig.apiRoot + '/api/Currency').then(function (response) {
                callback(response);
            });
        }

        function SaveCurrencys(Currencys, callback) {
            $http.post(appConfig.apiRoot + '/api/Currency', Currencys).then(function (response) {

                callback(response);
            });
        }

    }


})();