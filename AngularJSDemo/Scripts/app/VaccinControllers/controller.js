var TikaControllers = angular.module("TikaControllers", ['ui.bootstrap', 'ngResource']);

// this controller call the api method and display the list of vaccines
// in list.html
//TikaControllers.controller("ListController", ['$scope', '$timeout', 'filterFilter', '$resource',
//function ($scope, $http, $timeout, filterFilter, $resource) {
TikaControllers.controller('ListController', ['$scope', '$resource', 'filterFilter', function ($scope, $resource, filterFilter) {


    $scope.filtered = [];
    //var Issue = $resource('https://api.github.com/repos/angular-ui/bootstrap/issues');
    var Issue = $resource('/api/Vaccination');
    $scope.objects = Issue.query();

    $scope.objects.$promise.then(function () {
        $scope.totalItems = $scope.objects.length;
        $scope.filtered = $scope.objects;
        console.log($scope.objects);
    });
    $scope.currentPage = 1;
    $scope.numPerPage = 3;
    $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);

    $scope.sortType = 'Name'; // set the default sort type
    $scope.sortReverse = false;  // set the default sort order

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;

        index = $scope.filtered.indexOf(value);
        console.log(index);
        return (begin <= index && index < end);
    };

    $scope.$watch('search', function (term) {
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        console.log(term);
        if ($scope.objects.length > 0) {
            if (term.length > 0) {
                //  $scope.filtered = filterFilter($scope.objects, term);
                // $scope.filtered.length = 0;
                $scope.tempArray = [];
                angular.forEach($scope.objects, function (row) {
                    var t1 = (angular.lowercase((row.Name).toString()).indexOf(angular.lowercase($scope.search) || '') !== -1 || angular.lowercase((row.Description)).toString().indexOf(angular.lowercase($scope.search) || '') !== -1);
                    if (t1) {

                        $scope.tempArray.push(row);
                        console.log($scope.filtered);
                    }
                });
                $scope.filtered = $scope.tempArray;


            } else {
                $scope.filtered = $scope.objects;
            }

            //console.log($scope.entryLimit);
            $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);
            //console.log($scope.noOfPages);
            $scope.currentPage = 1;
            $scope.paginate();
        }
    });



    //  alert(1);
}]
);

// this controller call the api method and display the record of selected employee
// in delete.html and provide an option for delete
TikaControllers.controller("DeleteController", ['$scope', '$http', '$routeParams', '$location',
        function ($scope, $http, $routeParams, $location) {

            $scope.id = $routeParams.id;
            $http.get('/api/Vaccination/' + $routeParams.id).success(function (data) {
                $scope.vaccinName = data.Name;
                $scope.startTime = data.StartDay;
                $scope.endTime = data.EndDay;
                $scope.description = data.Description;
            });

            $scope.delete = function () {

                $http.delete('/api/Vaccination/' + $scope.id).success(function (data) {
                    $location.path('/list');
                }).error(function (data) {
                    $scope.error = "An error has occured while deleting employee! " + data;
                });
            };
        }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
TikaControllers.controller("EditController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {

        $scope.ID = 0;

        $scope.save = function () {

            $scope.startTime = parseInt($scope.startTime, 10);
            $scope.endTime = parseInt($scope.endTime, 10);
            var obj = {
                ID: $scope.ID,
                Name: $scope.vaccinName,
                StartDay: $scope.startTime,
                EndDay: $scope.endTime,
                Description: $scope.description,

            };

            if ($scope.ID == 0) {

                $http.post('/api/Vaccination/', obj).success(function (data) {
                    $location.path('/list');
                }).error(function (data) {
                    $scope.error = "An error has occured while adding employee! " + data.ExceptionMessage;
                });
            }
            else {
              
                $http.put('/api/Vaccination/', obj).success(function (data) {
                    $location.path('/list');
                }).error(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving customer! " + data.ExceptionMessage;
                });
            }
        }

        if ($routeParams.id) {

            //alert($routeParams.ID);
            $scope.ID = $routeParams.id;
            $scope.title = "Edit Vaccin";
            $scope.vaccinName = "Hellooo";
            $http.get('/api/Vaccination/' + $routeParams.id).success(function (data) {
                $scope.vaccinName = data.Name;
                $scope.startTime = data.StartDay;
                $scope.endTime = data.EndDay;
                $scope.description = data.Description;


            });
        }
        else {
            $scope.title = "Create New Vaccin";
        }
    }
]);