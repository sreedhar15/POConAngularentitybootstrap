(function () {
    'use strict';

    angular
        .module('app')
        .controller('CountryController', CountryController);

    CountryController.$inject = ['$rootScope', 'FlashService', 'CountryService'];

    function CountryController($rootScope, FlashService, CountryService) {

        var vm = this;

        vm.Countrys = [];

        vm.getCountrys = getCountrys;

        vm.saveCountrys = saveCountrys;

        vm.addCountry = addCountry;
        vm.removeCountry = removeCountry;

        function getCountrys() {
            CountryService.GetCountrys(function (response) {
                vm.Countrys = response.data.Countrys;
            });
        }

        function saveCountrys() {
            CountryService.SaveCountrys(vm.Countrys, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addCountry() {

            var Country = { "ID": 0, "Name": "" };

            vm.Countrys.push(Country);
        }

        function removeCountry(index) {
            vm.Countrys.splice(index, 1);
        }

        vm.getCountrys();
    }


})();