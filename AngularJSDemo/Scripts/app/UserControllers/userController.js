var UserControllers = angular.module("UserControllers", ['ui.bootstrap', 'ng-bs3-datepicker']);


// this controller call the api method and display the list of employees
// in list.html
UserControllers.controller("UserShowController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.isThingsCollapsed = false;
        $http.get('/api/User').success(function (data) {
            $scope.UserInfo = data;
            //$scope.$apply();
            $scope.ChildsList = data.Childs;
            //console.log(data.Childs);
            //console.log(data);

        });
        //  alert(1);
    }]
);

// this controller call the api method and display the record of selected employee
// in delete.html and provide an option for delete
UserControllers.controller("DeleteUserController", ['$scope', '$http', '$routeParams', '$location',
        function ($scope, $http, $routeParams, $location) {
            $scope.user = {};
           // $scope.id = $routeParams.id;
            $http.get('/api/User/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.Id = data.Id;
                $scope.UserName = data.UserName;
                $scope.FullName = data.FullName;
                $scope.FatherName = data.FatherName;
                $scope.MotherName = data.MotherName;
                $scope.PhoneNumber = data.PhoneNumber;
                $scope.MyDate1 = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                $scope.UserId = data.UserId;
            });
         
            $scope.ValidUser = function () {

                if ($scope.Id) { // your question said "more than one element"
                    $scope.title = "Delete User";
                    console.log($scope.Id);
                    return false;
                  
                }
                else {
                    $scope.title = "Invalid User";
                    console.log($scope.Id);
                    return true;
                }
            };

            $scope.delete = function () {

                $http.delete('/api/User/' + $scope.Id).success(function (data) {
                    var url = '@Url.Action("Index", "Home")';
                   // $location.url('/User/Index');
                    location.href = "/User/Index";
                }).error(function (data) {
                    $scope.error = "An error has occured while deleting employee! " + data;
                });
            };
        }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
UserControllers.controller("EditUserController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
        // $scope.parent = { BirthDate: '' };

        // $scope.BirthDate = new Date();
       // $scope.MyDate1 = "20/03/2012";
        //$scope.MyDate1 = new Date() | date:'yyyy-MM-dd';
        $scope.MyDate1 = $filter('date')(new Date(), 'yyyy/MM/dd');
        $scope.DatePublish = function () {
            console.log($scope.MyDate1);
        };

        //$scope.$watch('BirthDate', function (newValue, oldValue) {
        //    $scope.parent = { BirthDate: '' };
        //    console.log('BirthDate changed', oldValue, newValue);
        //}, true);

        // $scope.form.BirthDate = null;
       // $scope.child = {}

        $scope.isDate = false;
        

        $scope.ID = 0;


        $scope.save = function () {

           

            var obj = {
                //ID: $scope.ID,
                //UserId: $scope.UserId,
                //StartDay: $scope.startTime,
                //EndDay: $scope.endTime,
                //Description: $scope.description,
                BirthCertificateID: $scope.BirthCertificateID,
                //BirthDate: $scope.BirthDate,

                BirthDate: $scope.MyDate1,
                CurrentAddress: $scope.CurrentAddress,
                FatherName: $scope.FatherName,
                FullName: $scope.FullName,
                Id: $scope.ID,
                MotherName: $scope.MotherName,
                PermanentAddress: $scope.PermanentAddress,
                PhoneNumber: $scope.PhoneNumber,
                UserId: $scope.UserId,
                UserName: $scope.UserName,


            };
            console.log(obj.Id);

            if ($scope.ID == 0) {

                $http.post('/api/User/', obj).success(function (data) {
                    $location.path('/showuser');
                }).error(function (data) {
                    $scope.error = "An error has occured while adding employee! " + data.ExceptionMessage;
                });
            }
            else {
                //alert($scope.ID);
                $http.put('/api/User/', obj).success(function (data) {
                    $location.path('/showuser');
                }).error(function (data) {
                   // console.log(data);
                    $scope.error = "An Error has occured while Saving customer! " + data.ExceptionMessage;
                });
            }
        }

        if ($routeParams.id) {
            $scope.form = {};
            //$scope.User.BirthDate = [];
            //alert($routeParams.ID);
           // console.log($routeParams.id);
          //  $scope.ID = $routeParams.id;
            
            $scope.vaccinName = "Hellooo";
            $http.get('/api/User/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.ID = data.Id;
                $scope.UserName = data.UserName;
                $scope.FullName = data.FullName;
                $scope.FatherName = data.FatherName;
                $scope.MotherName = data.MotherName;
                $scope.PhoneNumber = data.PhoneNumber;
                $scope.MyDate1 = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                $scope.UserId = data.UserId;

                if (data.count > 0) {
                    $scope.title = "Edit User";
                } else {
                    $scope.title = "Create New User";
                    console.log($scope.ID);
                }

                //$scope.startTime = data.StartDay;
                //$scope.endTime = data.EndDay;
                //$scope.description = data.Description;


            });
        }
        else {
            $scope.title = "Create New User";
        }
    }
]);

//UserControllers.controller("MainCntl", [
//    '$scope', '$filter', '$route', '$routeParams', '$location',
//    function($scope, $routeParams, $location, $route) {
////function MainCntl($scope, $route, $routeParams, $location) {
//        $('.input-group.date').datepicker({
//            format: "dd/mm/yyyy",
//            startDate: "01-05-2014",
//            endDate: "31-12-2017",
//            todayBtn: "linked",
//            autoclose: true,
//            todayHighlight: true
//        });
//    }
//]);

