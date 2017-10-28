(function () {
	'use strict';

	angular
        .module('app')
        .factory('CustomProjectGroupService', CustomProjectGroupService);

	CustomProjectGroupService.$inject = ['$http', '$cookieStore', '$rootScope', 'appConfig', '$timeout'];

	function CustomProjectGroupService($http, $cookieStore, $rootScope, appConfig, $timeout) {

		var service = {};

	//	service.GetProjectGroups = GetProjectGroups;
	
		service.GetCustomProjectGroups = GetCustomProjectGroups;
		service.SaveCustomProjectGroups = SaveCustomProjectGroups;
		service.GetCustomProjectGroupsByProjectID = GetCustomProjectGroupsByProjectID;
		service.GetCustomProjectGroupsByCustomGroupID = GetCustomProjectGroupsByCustomGroupID;
		return service;

		//function GetProjectGroups(callback) {
		//	$http.get(appConfig.apiRoot + '/api/ProjectGroup').then(function (response) {
		//		callback(response);
		//	});
		//}

		//function GetSecurityRoles(callback) {
		//	$http.get(appConfig.apiRoot + '/api/SecurityRole').then(function (response) {
		//		callback(response);
		//	});
		//}

		function GetCustomProjectGroups(callback) {
			$http.get(appConfig.apiRoot + '/api/CustomProjectGroup').then(function (response) {
				callback(response);
			});
		}

		function GetCustomProjectGroupsByProjectID(projectID, callback) {
			$http.get(appConfig.apiRoot + '/api/CustomProjectGroup?ProjectID = ' + projectID).then(function (response) {
				callback(response);
			});
		}
		function GetCustomProjectGroupsByCustomGroupID(customGroupId,projectGroupId, callback) {
		     
		    var url = appConfig.apiRoot + '/api/CustomProjectGroup?customGroupId=' + customGroupId + '&projectGroupId=' + projectGroupId;
		    $http.get(url).then(function (response) {
		        callback(response);
		    });
		    
		}
		function SaveCustomProjectGroups(CustomProjectGroups, customGroupId, projectGroupId, callback) {
		    var url = appConfig.apiRoot + '/api/CustomProjectGroup?customGroupId=' + customGroupId + '&projectGroupId=' + projectGroupId;
		    $http.post(url,CustomProjectGroups).then(function (response) {
				callback(response);
			});
		}
	}

})();