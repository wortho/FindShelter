var findShelterApp = angular.module('findShelterApp', ['ngRoute']);

findShelterApp.config(function ($routeProvider) {
    $routeProvider
        .when('/facilities',
            {
                controller: 'facilityListController',
                templateUrl: '/views/FacilityList.html'
            })
        .when('/facilities/:facilityId',
            {
                controller: 'facilityDetailController',
                templateUrl: '/views/FacilityDetail.html'
            })
        .otherwise({ redirectTo: '/facilities' });
});

findShelterApp.controller('facilityListController',
    ['$scope', '$http',
    function ($scope, $http) {
        $http.get('api/facilities').success(function (data) {
            $scope.facilities = data;
        });
    }]);

findShelterApp.controller('facilityDetailController', 
    ['$scope', '$routeParams', '$http',
    function ($scope, $routeParams, $http) {
        $scope.facilityId = $routeParams.facilityId;
        $http.get('api/facilities/'+$scope.facilityId).success(function (data) {
            $scope.facility = data;
        });
}]);