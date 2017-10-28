(function () {
    'use strict';

    angular
        .module('app')
        .controller('CurrencyController', CurrencyController);

    CurrencyController.$inject = ['$rootScope', 'FlashService', 'CurrencyService'];

    function CurrencyController($rootScope, FlashService, CurrencyService) {

        var vm = this;

        vm.Currencys = [];

        vm.getCurrencys = getCurrencys;

        vm.saveCurrencys = saveCurrencys;

        vm.addCurrency = addCurrency;
        vm.removeCurrency = removeCurrency;

        function getCurrencys() {
            CurrencyService.GetCurrencys(function (response) {
                vm.Currencys = response.data.Currencys;
            });
        }

        function saveCurrencys() {
            CurrencyService.SaveCurrencys(vm.Currencys, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addCurrency() {

            var Currency = { "ID": 0, "Name": "" };

            vm.Currencys.push(Currency);
        }

        function removeCurrency(index) {
            vm.Currencys.splice(index, 1);
        }

        vm.getCurrencys();
    }


})();