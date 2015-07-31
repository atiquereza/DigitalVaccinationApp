var childApp = angular.module('ChildApp', [
    'ngRoute',
    'ChildModule'
]);

angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["ChildApp"]);

});

childApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/addChild', {
        templateUrl: '/PartialViews/Child/EditChild.html',
        message: 'Add Child',
        controller: 'EditAddChildController'
    }).when('/dashboard', {
        templateUrl: '/PartialViews/Child/ChildInfo.html',
        message: 'Dashboard',
        controller: 'UserChildController'
    }).when('/ViewChild/:id', {
        templateUrl: '/PartialViews/Child/ViewChild.html',
        controller: 'ViewChildInfo',
        message: 'View Child'
    }).when('/UpdateTikaStatus/:tid/:cid', {
        templateUrl: '/PartialViews/Child/ViewChild.html',
        controller: 'UpdateTikaStatus',
        message: 'View Child'
    }).otherwise({
        redirectTo: function() {
            window.location.href = "/Child/Index#/dashboard";
        }
    });

}]);