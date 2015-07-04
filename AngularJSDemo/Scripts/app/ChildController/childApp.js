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
    }).otherwise({
            redirectTo: function () {
                window.location.href = "/Child/Index";
            }
        });

}]);