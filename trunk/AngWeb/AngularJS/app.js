
(function () {
    'use strict';



    var app = angular
        .module('app', ['ngRoute', 'ngCookies'])
        .config(config)
        .run(run);


    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {


        $routeProvider

            .when('/menu', {
                controller: 'MenuController',
                templateUrl: 'AngularJS/menu/menu.view.html',
                controllerAs: 'vm'
            })

            .when('/login', {
                controller: 'LoginController',
                templateUrl: 'AngularJS/login/login.view.html',
                controllerAs: 'vm'
            })

            .when('/register', {
                controller: 'RegisterController',
                templateUrl: 'AngularJS/register/register.view.html',
                controllerAs: 'vm'
            })

            .when('/project', {
                controller: 'ProjectController',
                templateUrl: 'AngularJS/project/project.view.html',
                controllerAs: 'vm'
            })

            .when('/projectGroup', {
                controller: 'ProjectGroupController',
                templateUrl: 'AngularJS/projectGroup/projectGroup.view.html',
                controllerAs: 'vm'
            })

             .when('/plan', {
                 controller: 'PlanController',
                 templateUrl: 'AngularJS/plan/plan.view.html',
                 controllerAs: 'vm'
             })

             .when('/planGroup', {
                 controller: 'PlanGroupController',
                 templateUrl: 'AngularJS/planGroup/planGroup.view.html',
                 controllerAs: 'vm'
             })

            .when('/employeeRoleType', {
                controller: 'EmployeeRoleTypeController',
                templateUrl: 'AngularJS/employeeRoleType/employeeRoleType.view.html',
                controllerAs: 'vm'
            })

            .when('/expenseType', {
                controller: 'ExpenseTypeController',
                templateUrl: 'AngularJS/expenseType/expenseType.view.html',
                controllerAs: 'vm'
            })

            .when('/employeeRole', {
                controller: 'EmployeeRoleController',
                templateUrl: 'AngularJS/employeeRole/employeeRole.view.html',
                controllerAs: 'vm'
            })

            .when('/dvp', {
                controller: 'DVPController',
                templateUrl: 'AngularJS/dvp/dvp.view.html',
                controllerAs: 'vm'
            })

          .when('/country', {
              controller: 'CountryController',
              templateUrl: 'AngularJS/country/country.view.html',
              controllerAs: 'vm'
          })

          .when('/costCenter', {
              controller: 'CostCenterController',
              templateUrl: 'AngularJS/costCenter/costCenter.view.html',
                 controllerAs: 'vm'
             })

          .when('/currency', {
              controller: 'CurrencyController',
              templateUrl: 'AngularJS/currency/currency.view.html',
              controllerAs: 'vm'
          })

         .when('/employeeRoleByType', {
             controller: 'EmployeeRoleByTypeController',
             templateUrl: 'AngularJS/employeeRoleByType/employeeRoleByType.view.html',
             controllerAs: 'vm'
         })

        .when('/expenseByType', {
            controller: 'ExpenseByTypeController',
            templateUrl: 'AngularJS/expenseByType/expenseByType.view.html',
            controllerAs: 'vm'
        })

        .when('/expense', {
            controller: 'ExpenseController',
            templateUrl: 'AngularJS/expense/expense.view.html',
            controllerAs: 'vm'
        })

        .when('/planDetailHeadCount', {
            controller: 'PlanDetailHeadCountController',
            templateUrl: 'AngularJS/planDetailHeadCount/planDetailHeadCount.view.html',
            controllerAs: 'vm'
        })

        .when('/planDetailExpense', {
            controller: 'PlanDetailExpenseController',
            templateUrl: 'AngularJS/planDetailExpense/planDetailExpense.view.html',
            controllerAs: 'vm'
        })
        .when('/user', {
            controller: 'UserController',
            templateUrl: 'AngularJS/user/user.view.html',
            controllerAs: 'vm'
        })
       .when('/franchise', {
             controller: 'FranchiseController',
             templateUrl: 'AngularJS/franchise/franchise.view.html',
             controllerAs: 'vm'
        })
       .when('/plline', {
                 controller: 'PLLineController',
                 templateUrl: 'AngularJS/plline/plline.view.html',
                 controllerAs: 'vm'
             })
        .when('/userSecurityRole', {
            controller: 'UserSecurityRoleController',
            templateUrl: 'AngularJS/userSecurityRole/userSecurityRole.view.html',
            controllerAs: 'vm'
        })
        .when('/projectGroupUser', {
            controller: 'ProjectGroupUserController',
            templateUrl: 'AngularJS/projectGroupUser/projectGroupUser.view.html',
            controllerAs: 'vm'
        })
        .when('/projectUser', {
            controller: 'ProjectUserController',
            templateUrl: 'AngularJS/projectUser/projectUser.view.html',
            controllerAs: 'vm'
        })
        .when('/customGroup', {
            controller: 'CustomGroupController',
            templateUrl: 'AngularJS/customGroup/customGroup.view.html',
            controllerAs: 'vm'
        })
        .when('/customProjectGroup', {
            controller: 'CustomProjectGroupController',
            templateUrl: 'AngularJS/customProjectGroup/customProjectGroup.view.html',
            controllerAs: 'vm'
        })
        .otherwise({ redirectTo: '/login/' });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];
    function run($rootScope, $location, $cookieStore, $http) {
        // keep user logged in after page refresh
        $rootScope.globals = $cookieStore.get('globals') || {};

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in and trying to access a restricted page
            var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
            var loggedIn = $rootScope.globals.currentUser;

            if (restrictedPage && !loggedIn) {
                $location.path('/login');
            }

        });
    }


    var appConfig = { apiRoot: $("#linkApiRoot").attr("href") };
    
    
    angular.module('app').config(["$provide", function ($provide) {
        $provide.value("appConfig", appConfig);
    }]);




})();


  
