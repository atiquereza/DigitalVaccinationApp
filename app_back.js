var EmpApp = angular.module('EmpApp', [
    'ngRoute',
    'EmpControllers'
]);

EmpApp.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.when('/list', {
            templateUrl: 'Employee/List.html',
            controller: 'ListController'
        }).
        when('/create', {
            templateUrl: 'Employee/edit.html',
            controller: 'EditController'
        }).
        when('/edit/:id', {
            templateUrl: 'Employee/edit.html',
            controller: 'EditController'
        }).
        when('/delete/:id', {
            templateUrl: 'Employee/delete.html',
            controller: 'DeleteController'
        }).
        otherwise({
        redirectTo: '/list'
    });

}]);