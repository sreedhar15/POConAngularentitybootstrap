(function () {
    'use strict';

    angular
        .module('app')
        .controller('PLLineController', PLLineController);

    PLLineController.$inject = ['$rootScope', 'FlashService', 'PLLineService'];

    function PLLineController($rootScope, FlashService, PLLineService) {

        var vm = this;

        vm.pllines = [];

        vm.getPLLines = getPLLines;

        vm.savePLLines = savePLLines;

        vm.addPLLine = addPLLine;
        vm.removePLLine = removePLLine;

        function getPLLines() {
            PLLineService.GetPLLines(function (response) {
                vm.pllines = response.data.pllines;
            });
        }

        function savePLLines() {
            PLLineService.SavePLLines(vm.pllines, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addPLLine() {

            var plline = { "ID": 0, "PLLineName": "", "Priority": 0 };

            vm.pllines.push(plline);
        }

        function removePLLine(index) {
            vm.pllines.splice(index, 1);
        }

        vm.getPLLines();
    }


})();