var TikaControllers = angular.module("TikaControllers", ['ui.bootstrap', 'ngResource']);

// this controller call the api method and display the list of vaccines
// in list.html

TikaControllers.controller('ListController', [
        '$scope', '$resource', 'filterFilter', function ($scope, $resource, filterFilter) {


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
            $scope.sortReverse = false; // set the default sort order

            $scope.paginate = function (value) {
                var begin, end, index;
                begin = ($scope.currentPage - 1) * $scope.numPerPage;
                end = begin + $scope.numPerPage;

                index = $scope.filtered.indexOf(value);
                console.log(index);
                return (begin <= index && index < end);
            };

            $scope.$watch('search', function (term) {
                if ($scope.objects.length > 0) {
                    if (term.length > 0) {
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

                    $scope.noOfPages = Math.ceil($scope.filtered.length / $scope.numPerPage);
                    $scope.currentPage = 1;
                    $scope.paginate();
                }
            });

        }
]
);

// this controller call the api method and display the record of selected employee
// in delete.html and provide an option for delete
TikaControllers.controller("DeleteController", ['$scope', '$resource', '$routeParams', '$location',
        function ($scope, $resource, $routeParams, $location) {

            $scope.id = $routeParams.id;

            var Issue = $resource('/api/Vaccination/' + $routeParams.id);
            $scope.objects = Issue.get();

            $scope.objects.$promise.then(function () {

                $scope.vaccinName = $scope.objects.Name;
                $scope.startTime = $scope.objects.StartDay;
                $scope.endTime = $scope.objects.EndDay;
                $scope.description = $scope.objects.Description;
            }, function (error) {
                $scope.error = "An error has occured while deleting employee! " + error;

            });



            $scope.delete = function () {


                $scope.objectsDelete = Issue.delete();
                $scope.objectsDelete.$promise.then(function () {
                    $location.path('/list');
                }, function (error) {
                    $scope.error = "An error has occured while deleting employee! " + error;
                });
            };
        }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
TikaControllers.controller("EditController", ['$scope', '$filter', '$resource', '$routeParams', '$location',
    function ($scope, $filter, $resource, $routeParams, $location) {

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


                var IssusSave = $resource('/api/Vaccination/');
                $scope.objectSave = IssusSave.save(obj);
                $scope.objectSave.$promise.then(function () {
                    $location.path('/list');
                }, function (error) {

                    $scope.error = "An error has occured while adding employee! ";
                });

            } else {

                var IssueUpdate = $resource('/api/Vaccination/', {}, {
                    update: {
                        method: 'PUT'
                    }
                });



                $scope.objectsUpdate = IssueUpdate.update(obj);
                $scope.objectsUpdate.$promise.then(function () {

                    $location.path('/list');
                }, function (error) {

                    $scope.error = "An error has occured while Saving employee! ";
                });

            }
        }

        if ($routeParams.id) {

            //alert($routeParams.ID);
            $scope.ID = $routeParams.id;
            $scope.title = "Edit Vaccin";



            var Issue = $resource('/api/Vaccination/' + $routeParams.id);
            $scope.objectsGetVaccin = Issue.get();

            $scope.objectsGetVaccin.$promise.then(function () {
                //$scope.totalItems = $scope.objects.length;
                //$scope.filtered = $scope.objects;
                //console.log($scope.objects);

                $scope.vaccinName = $scope.objectsGetVaccin.Name;
                $scope.startTime = $scope.objectsGetVaccin.StartDay;
                $scope.endTime = $scope.objectsGetVaccin.EndDay;
                $scope.description = $scope.objectsGetVaccin.Description;
            }, function (error) {

                $scope.error = "An error has occured while Acquiring employee Information! ";
            });
        }
        else {
            $scope.title = "Create New Vaccin";
        }
    }
]);