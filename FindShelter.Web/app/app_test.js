///<reference path="~/Scripts/jasmine/jasmine.js"/>
///<reference path="~/Scripts/angular.js"/>
///<reference path="~/Scripts/angular-route.js"/>
///<reference path="~/Scripts/angular-sanitize.js"/>
///<reference path="~/Scripts/angular-mocks.js"/>
///<reference path="~/App/app.js"/>

describe("facilityListControllerSpec", function () {

    beforeEach(module("findShelterApp"));

    beforeEach(inject(function (_$httpBackend_, $rootScope, $sce, $controller) {
        $httpBackend = _$httpBackend_;
        $httpBackend.expectGET('api/facilities?latitudeSW=55&longitudeSW=12&latitudeNE=56&longitudeNE=13').
            respond([{ id: 1, name: 'Shelter1' }, { id: 2, name: 'Shelter2'}]);
        scope = $rootScope.$new();
        ctrl = $controller('facilityListController', { $scope: scope });
    }));

    it('should create "facilities" model with 2 facilities', inject(function ($controller) {
        
        expect(scope.facilities).toBeUndefined();

        $httpBackend.flush();

        expect(scope.facilities.length).toBe(2);

    }));

    it('should create the "facilities" with name and id', inject(function ($controller) {
        expect(scope.facilities).toBeUndefined();
        $httpBackend.flush();
        expect(scope.facilities[0].id).toBe(1);
        expect(scope.facilities[0].name).toBe('Shelter1');
        expect(scope.facilities[1].id).toBe(2);
        expect(scope.facilities[1].name).toBe('Shelter2');

    }));

});

describe("facilityDetailControllerSpec", function () {

    beforeEach(module("findShelterApp"));

    beforeEach(inject(function (_$httpBackend_, $rootScope, $routeParams, $sce, $controller) {
        $httpBackend = _$httpBackend_;
        $httpBackend.expectGET('api/facilities/1').
            respond({ id: 1, name: 'Shelter1', longDescription: 'description' });
        $routeParams.facilityId = 1;
        scope = $rootScope.$new();
        ctrl = $controller('facilityDetailController', { $scope: scope });
    }));
    
    it('should create one "facility" with the specified id and name', inject(function ($rootScope, $routeParams, $sce, $controller) {
        expect(scope.facility).toBeUndefined();
        $httpBackend.flush();
        expect(scope.facilityId).toBe(1);
        expect(scope.facility.id).toBe(1);
        expect(scope.facility.name).toBe('Shelter1');

    }));

});

describe("findShelterAppRouteConfigSpec", function () {

    beforeEach(module("findShelterApp"));
    
    it('facility route matches facilityListController with FacilityList view',
    inject(function ($route) {

        expect($route.routes['/facilities'].controller).toBe('facilityListController');
        expect($route.routes['/facilities'].templateUrl).toEqual('/views/FacilityList.html');

    }));

    it('null route matches facilityListController with FacilityList view',
    inject(function ($route) {
        expect($route.routes[null].redirectTo).toEqual('/facilities')

    }));

    it('facility with id route param matches facilityDetailController with FacilityDetail view',
    inject(function ($route) {
        expect($route.routes['/facilities/:facilityId'].controller).toBe('facilityDetailController');
        expect($route.routes['/facilities/:facilityId'].templateUrl).toEqual('/views/FacilityDetail.html');

    }));
});