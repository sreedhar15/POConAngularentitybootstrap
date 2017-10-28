(function () {
	'use strict';

	angular
        .module('app')
        .factory('ProjectGroupUserService', ProjectGroupUserService);

	ProjectGroupUserService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

	function ProjectGroupUserService($http, $cookieStore, $rootScope, appConfig, $timeout) {

		var service = {};

		service.GetProjectGroups = GetProjectGroups;
		service.GetSecurityRoles = GetSecurityRoles;
		service.GetProjectGroupUsers = GetProjectGroupUsers;
		service.SaveProjectGroupUsers = SaveProjectGroupUsers;
		service.GetProjectGroupUsersByUserID = GetProjectGroupUsersByUserID;
		return service;

		function GetProjectGroups(callback) {
			$http.get(appConfig.apiRoot + '/api/ProjectGroup').then(function (response) {
				callback(response);
			});
		}

		function GetSecurityRoles(callback) {
			$http.get(appConfig.apiRoot + '/api/SecurityRole').then(function (response) {
				callback(response);
			});
		}

		function GetProjectGroupUsers(callback) {
			$http.get(appConfig.apiRoot + '/api/ProjectGroupUser').then(function (response) {
				callback(response);
			});
		}

		function GetProjectGroupUsersByUserID(userID, callback) {
			$http.get(appConfig.apiRoot + '/api/ProjectGroupUser?userID = ' + userID).then(function (response) {
				callback(response);
			});
		}

		function SaveProjectGroupUsers(ProjectGroupUsers, callback) {
			$http.post(appConfig.apiRoot + '/api/ProjectGroupUser', ProjectGroupUsers).then(function (response) {
				callback(response);
			});
		}
	}

})();