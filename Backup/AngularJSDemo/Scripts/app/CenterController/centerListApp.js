

var CenterListApp = angular.module('CenterListApp', [
    // Angular modules 

    // Custom modules 

    // 3rd Party Modules
    'restangular',
    'ngTable',
    'ngRoute'
]);
CenterListApp.config(restangularConfig);
angular.element(document).ready(function () {
    var myHtml = document.getElementById("Container");
    angular.bootstrap(myHtml, ["CenterListApp"]);

});

CenterListApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/ListCenter', {
        templateUrl: '/PartialViews/Center/ListCenters.html',
        controller: 'CentersListController'
    }).


        otherwise({
            redirectTo: '/ListCenter'
        });

}]);


restangularConfig.$inject = ['RestangularProvider'];

function restangularConfig(RestangularProvider) {
    RestangularProvider.setBaseUrl('/api');

    RestangularProvider.addResponseInterceptor(function (data, operation, what, url, response, deferred) {
        var extractedData = {};
        // .. to look for getList operations
        if (operation === "getList") {
            // .. and handle the data and meta data
            console.log(data);
            extractedData = data.Items;
            extractedData.paging =
            {
                pageCount: data.PageCount,
                pageNo: data.PageNo,
                pageSize: data.PageSize,
                totalRecordCount: data.TotalRecordCount
            };
            console.log(extractedData.paging);
        } else {
            extractedData = data;
        }
        return extractedData;
    });
};

