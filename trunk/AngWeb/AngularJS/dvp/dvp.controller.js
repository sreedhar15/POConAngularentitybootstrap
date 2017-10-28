(function () {
    'use strict';

    angular
        .module('app')
        .controller('DVPController', DVPController);

    DVPController.$inject = ['$rootScope', 'FlashService', 'DVPService'];

    function DVPController($rootScope, FlashService, DVPService) {

        var vm = this;

        vm.DVPs = [];

        vm.getDVPs = getDVPs;

        vm.saveDVPs = saveDVPs;

        vm.addDVP = addDVP;
        vm.removeDVP = removeDVP;

        function getDVPs() {
            DVPService.GetDVPs(function (response) {
                vm.DVPs = response.data.DVPs;
            });
        }

        function saveDVPs() {
            DVPService.SaveDVPs(vm.DVPs, function (response) {
                FlashService.Success('Saved successfully.', false);

            });
        }

        function addDVP() {

            var DVP = { "ID": 0, "Name": "" };

            vm.DVPs.push(DVP);
        }

        function removeDVP(index) {
            vm.DVPs.splice(index, 1);
        }

        vm.getDVPs();
    }


})();