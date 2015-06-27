'use strict';

//angular
//    .module('UsersPagingApp')
//    .controller('UsersController', UsersController);

//UsersController.$inject = ['$scope', 'Restangular', 'ngTableParams'];

CenterListApp.controller("CentersListController", [
'$scope', 'Restangular', 'ngTableParams',
function ($scope, Restangular, ngTableParams) {

    /* jshint validthis:true */
    var vm = this;

    $scope.search = '';

    $scope.tableParams = new ngTableParams({
        page: 1,
        count: 2,
        //noCounts: true,
        //counts: [],
        sorting: {
            ID: 'asc'
        }
    },
    {
        getData: function ($defer, params) {
            // Load the data from the API
            Restangular.all('CenterPaging').getList({
                pageNo: params.page(),
                pageSize: params.count(),
                sort: params.orderBy(),
                search: $scope.search
            }).then(function (centers) {
                // console.log(customers);
                $scope.RowCount = centers.paging.totalRecordCount;
                // Tell ngTable how many records we have (so it can set up paging)
                params.total(centers.paging.totalRecordCount);

                // Return the customers to ngTable
                $defer.resolve(centers);
            }, function (response) {
                // Notify of error
            });
        }
    });

    // Watch for changes to the search text, so we can reload the table
    $scope.$watch(angular.bind(vm, function () {
        return $scope.search;
    }), function (value) {
       // $scope.tableParams.settings().$scope = $scope;
        $scope.tableParams.reload();
    });

}
]);
